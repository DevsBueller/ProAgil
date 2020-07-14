using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using ProAgil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProAgil.Repository
{
	public class ProAgilRepository : IProAgilRepository
	{
		private readonly ProAgilContext _context;

		public ProAgilRepository(ProAgilContext context)
		{
			_context = context;
			_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}
		//Gerais
		public void Add<T>(T entity) where T : class
		{
			_context.Add(entity);
		}
		public void Update<T>(T entity) where T : class
		{
			_context.Update(entity);
		}

		public void Delete<T>(T entity) where T : class
		{
			_context.Remove(entity);
		}
		public async Task<bool> SaveChangesAsync()
		{
			return (await _context.SaveChangesAsync()) > 0;
		}
		//EVENTO
		public async Task<Event[]> GetAllEventAsync(bool includeSpeakers = false)
		{
			IQueryable<Event> query = _context.Events
				.Include(c => c.Lots)
				.Include(c=>c.SocialNetworks);
			if (includeSpeakers)
			{
				query = query
					.Include(p => p.SpeakersEvents)
					.ThenInclude(p => p.Speaker);
			}
			query = query.OrderBy(c => c.Id);
			return await query.ToArrayAsync();
		}

		public async Task<Event[]> GetAllEventAsyncByTema(string theme, bool includeSpeakers)
		{
			IQueryable<Event> query = _context.Events
				.Include(c => c.Lots)
				.Include(c => c.SocialNetworks);
			if (string.IsNullOrEmpty(theme))
			{
				query.Where(c => c.Theme.ToLower().Contains(theme.ToLower()));

			}
			if (includeSpeakers)
			{
				query = query
					.Include(p => p.SpeakersEvents)
					.ThenInclude(p => p.Speaker);
			}
			query = query.OrderByDescending(c => c.EventDate);
			return await query.ToArrayAsync();
		}
		public async Task<Event> GetEventAsyncById(int EventId, bool includeSpeakers)
		{
			IQueryable<Event> query = _context.Events
				.Include(c => c.Lots)
				.Include(c => c.SocialNetworks);



			if (includeSpeakers)
			{
				query = query
					.Include(p => p.SpeakersEvents)
					.ThenInclude(p => p.Speaker);
			}
		
			query = query.AsNoTracking().OrderByDescending(c => c.EventDate).Where(e => e.Id == EventId);
			return await query.FirstOrDefaultAsync();
		}
		//PALESTRANTE
		public async Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvent)
		{
			IQueryable<Speaker> query = _context.Speakers
				.Include(c => c.SocialNetwork);
			if (includeEvent)
			{
				query = query
					.Include(p => p.SpeakersEvents)
					.ThenInclude(e => e.Event);
			}
			query = query.OrderBy(c => c.Name).Where(c => c.Name.ToLower().Contains(name.ToLower()));
			return await query.ToArrayAsync();
		}

	

		public async Task<Speaker> GetSpeakerAsync(int SpeakerId, bool IncludeEvents = false)
		{
			IQueryable<Speaker> query = _context.Speakers
				.Include(c => c.SocialNetwork);
			if (IncludeEvents)
			{
				query = query
					.Include(se => se.SpeakersEvents)
					.ThenInclude(e => e.Event);
					
			}
			query = query.OrderBy(c => c.Name).Where(c=> c.Id == SpeakerId);
			return await query.FirstOrDefaultAsync();
		}


	}
}
