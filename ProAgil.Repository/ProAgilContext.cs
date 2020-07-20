using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAgil.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProAgil.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace ProAgil.Repository
{
	public class ProAgilContext: IdentityDbContext<User, Role, int, 
		IdentityUserClaim<int>,
		UserRole, IdentityUserLogin<int>, 
		IdentityRoleClaim<int>, 
		IdentityUserToken<int>>
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
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<UserRole>(
				userRole => {
					userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

					userRole.HasOne(ur => ur.Role)
					.WithMany(r => r.UserRoles)
					.HasForeignKey(ur => ur.RoleId)
					.IsRequired();

					userRole.HasOne(ur => ur.User)
					.WithMany(r => r.UserRoles)
					.HasForeignKey(ur => ur.UserId)
					.IsRequired();
				}
			);
			modelBuilder.Entity<SpeakerEvent>()
				.HasKey(PE => new { PE.EventId, PE.SpeakerId });

		}
	}
}
