using Microsoft.EntityFrameworkCore;
using Restful.Core.Connection;
using Restful.Entity.Entities;

namespace Restful.Core.Context
{
    /// <summary>
    /// Application DB Context - Restful 
    /// </summary>
    public class RestfulDbContext : DbContext
    {
        public DbSet<CollectionEntity> Collections { get; set; }
        public DbSet<HeaderEntity> Headers { get; set; }
        public DbSet<ParameterEntity> Parameters { get; set; }
        public DbSet<PasswordEntity> Passwords { get; set; }
        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<ResponseEntity> Responses { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public RestfulDbContext() { }
        public RestfulDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseSqlite(Hidden.GetConnectionString());
        }

        // Configure Fluent API //
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
