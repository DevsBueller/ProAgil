using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAgil.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProAgil.Repository
{
	public class ProAgilContext: DbContext
	{
		public ProAgilContext(DbContextOptions<ProAgilContext> options): base(options)
		{

		}
		public DbSet<Event> Events { get; set; }
		public DbSet<Speaker> Speakers { get; set; }
		public DbSet<SpeakerEvent> SpeakerEvents { get; set; }
		public DbSet<Lot> Lots { get; set; }
		public DbSet<SocialNetwork> SocialNetworks { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SpeakerEvent>().HasKey(PE => new { PE.EventId, PE.SpeakerId });

		}
	}
}
