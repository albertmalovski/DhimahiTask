using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
  public  class AppDbContext: IdentityDbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<StoreRoles> StoreRoles { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Currency> Currencies { get; set; }
		public DbSet<Continent> Continents { get; set; }
		public DbSet<Language> Languages { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			modelBuilder.Entity<CountryLanguage>()
				.HasKey(bc => new { bc.CountryId, bc.LanguageId });
			modelBuilder.Entity<CountryLanguage>()
				.HasOne(bc => bc.Country)
				.WithMany(b => b.CountryLanguage)
				.HasForeignKey(bc => bc.CountryId);
			modelBuilder.Entity<CountryLanguage>()
				.HasOne(bc => bc.Language)
				.WithMany(c => c.CountryLanguage)
				.HasForeignKey(bc => bc.LanguageId);

			if (Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer")
			{

				modelBuilder.Entity<AppUser>(b =>
				{
					b.ToTable("Users");
				});

				modelBuilder.Entity<IdentityUserClaim<string>>(b =>
				{
					b.ToTable("UserClaims");
				});

				modelBuilder.Entity<IdentityUserLogin<string>>(b =>
				{
					b.ToTable("UserLogins");
				});

				modelBuilder.Entity<IdentityUserToken<string>>(b =>
				{
					b.ToTable("UserTokens");
				});

				modelBuilder.Entity<StoreRoles>(b =>
				{
					b.ToTable("Roles");
				});

				modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
				{
					b.ToTable("RoleClaims");
				});

				modelBuilder.Entity<IdentityUserRole<string>>(b =>
				{
					b.ToTable("UserRoles");
				});


				foreach (var entityType in modelBuilder.Model.GetEntityTypes())
				{
					var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

					
					var dateTimeProperties = entityType.ClrType.GetProperties()
						.Where(p => p.PropertyType == typeof(DateTimeOffset));

					foreach (var property in properties)
					{
						modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
					}
					

					foreach (var property in dateTimeProperties)
					{
						modelBuilder.Entity(entityType.Name).Property(property.Name)
							.HasConversion(new DateTimeOffsetToBinaryConverter());
					}
				}
			}
		}
	}
}
