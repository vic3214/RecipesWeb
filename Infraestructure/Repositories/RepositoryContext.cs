using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain.Repositories
{
    public class RepositoryContext : DbContext
    {
        private readonly IConfiguration _configuration;
    
        public RepositoryContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            string defaultSchema = _configuration["SQL:Schema"] ?? 
                                   Environment.GetEnvironmentVariable("SQL_Schema") ?? 
                                   "dbo";

            modelBuilder.HasDefaultSchema(defaultSchema);

            // Update cascade delete behavior
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(t => t.GetForeignKeys())
                         .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Recipe> Recipes { get; set; } = null!;
    }
}
