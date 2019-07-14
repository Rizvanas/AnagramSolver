using AnagramGenerator.EF.DatabaseFirst.Configurations;
using AnagramGenerator.EF.DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramGenerator.EF.DatabaseFirst.Persistence
{
    public partial class WordsDBContext : DbContext
    {
        public WordsDBContext(DbContextOptions<WordsDBContext> options)
            : base(options)
        { }

        public virtual DbSet<Anagrams> Anagrams { get; set; }
        public virtual DbSet<CachedWords> CachedWords { get; set; }
        public virtual DbSet<Phrases> Phrases { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }
        public virtual DbSet<Words> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=WordsDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AnagramsConfiguration());
            modelBuilder.ApplyConfiguration(new CachedWordsConfiguration());
            modelBuilder.ApplyConfiguration(new PhrasesConfiguration());
            modelBuilder.ApplyConfiguration(new UserLogConfiguration());
            modelBuilder.ApplyConfiguration(new WordsConfiguration());
        }
    }
}