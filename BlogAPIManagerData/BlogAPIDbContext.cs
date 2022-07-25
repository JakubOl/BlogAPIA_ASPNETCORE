using BlogAPIModels;
using BlogAPIModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogAPIData
{
    public class BlogAPIDbContext : DbContext
    {
        public static IConfigurationRoot _configuration;

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }

        public BlogAPIDbContext()
        {

        }
        public BlogAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                _configuration = builder.Build();
                var connectionString = _configuration.GetConnectionString("BlogAPI");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired();
            modelBuilder.Entity<Post>().Property(p => p.Title).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Post>().Property(p => p.Text).IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.Text).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();

            modelBuilder.Entity<Role>(x =>
            {
                x.HasData(
                    new Role()
                    {
                        Id = 1,
                        Name = "User"
                    },
                    new Role()
                    {
                        Id = 2,
                        Name = "Admin"
                    });
            });
        }
    }
}