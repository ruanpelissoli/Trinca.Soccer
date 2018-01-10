using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=TrincaSoccer")
        {
        }

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Employee>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Employees");
            });
        }

        public interface IDbContextFactory
        {
            DatabaseContext GetDbContext();
        }

        public class DatabaseContextFactory : IDbContextFactory
        {
            private readonly DatabaseContext _context;

            public DatabaseContextFactory()
            {
                _context = new DatabaseContext();
            }

            public DatabaseContext GetDbContext()
            {
                return _context;
            }
        }
    }
}
