using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageTheme> MessageThemes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTheme>().HasData(
                new MessageTheme { Id = 1, Name = "Техподдержка" },
                new MessageTheme { Id = 2, Name = "Продажи" },
                new MessageTheme { Id = 3, Name = "Другое" },
                new MessageTheme { Id = 4, Name = "Ещё один пункт" }
            );
        }

    }
}
