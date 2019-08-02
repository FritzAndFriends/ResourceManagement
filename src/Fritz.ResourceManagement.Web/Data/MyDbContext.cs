using Fritz.ResourceManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Fritz.ResourceManagement.Web.Models
{

	public class MyDbContext : DbContext
	{

		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<PersonPersonType>()
				.HasKey(pt => new { pt.PersonId, pt.PersonTypeId });

			modelBuilder.Entity<PersonPersonType>()
				.HasOne<Person>(pt => pt.Person)
				.WithMany(p => p.PersonPersonType)
				.HasForeignKey(pt => pt.PersonId);

			modelBuilder.Entity<PersonPersonType>()
				.HasOne<PersonType>(pt => pt.PersonType)
				.WithMany(t => t.PersonPersonTypes)
				.HasForeignKey(pt => pt.PersonTypeId);

			modelBuilder.Entity<Person>()
				.HasOne<Schedule>("Schedule");

		}

		public DbSet<Person> Persons { get; set; }

		public DbSet<PersonType> PersonTypes { get; set; }

		public DbSet<Schedule> Schedules { get; set; }

	}

}
