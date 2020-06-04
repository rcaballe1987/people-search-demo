using Microsoft.EntityFrameworkCore;

namespace PeopleSearchAppDemo.Core.Domain
{
    public class PeopleSearchAppDemoDbContext : DbContext
    {
        public PeopleSearchAppDemoDbContext(DbContextOptions<PeopleSearchAppDemoDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Interest> Interests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonInterest>()
                .HasKey(x => new { x.PersonId, x.InterestId });
        }
    }
}
