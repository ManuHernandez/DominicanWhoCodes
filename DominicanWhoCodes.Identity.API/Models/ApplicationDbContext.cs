using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DominicanWhoCodes.Identity.API.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            :base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(model =>
            {
                model.Property(e => e.FirstName).IsRequired().HasMaxLength(80);
                model.Property(e => e.LastName).IsRequired().HasMaxLength(250);
                model.Property(e => e.Email).IsRequired();
            });
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
