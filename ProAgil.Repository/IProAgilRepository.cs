using ProAgil.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProAgil.Repository
{
	public interface IProAgilRepository
	{
		void Add<T>(T entity) where T : class;
		void Update<T>(T entity) where T : class;
		void Delete<T>(T entity) where T : class;
		void DeleteRange<T>(T[] entity) where T : class;
		Task<bool> SaveChangesAsync();

		//EVENTOS
		Task<Event[]> GetAllEventAsyncByTema(string theme, bool includeSpeakers);
		Task<Event[]> GetAllEventAsync(bool includeSpeakers);
		Task<Event> GetEventAsyncById(int EventId, bool includeSpeakers);


		//Speakers
		Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvent);
		Task<Speaker> GetSpeakerAsync(int SpeakerId, bool includeEvent);
	}
}
