using AnagramGenerator.EF.DatabaseFirst.Configurations;
using AnagramGenerator.EF.DatabaseFirst;
using Microsoft.EntityFrameworkCore;
using Contracts.Entities;

namespace AnagramGenerator.EF.DatabaseFirst
{
    public partial class WordsDBContext : DbContext
    {
        public WordsDBContext(DbContextOptions<WordsDBContext> options)
            : base(options)
        { }

        public virtual DbSet<AnagramEntity> Anagrams { get; set; }
        public virtual DbSet<CachedWordEntity> CachedWords { get; set; }
        public virtual DbSet<PhraseEntity> Phrases { get; set; }
        public virtual DbSet<UserLogEntity> UserLog { get; set; }
        public virtual DbSet<WordEntity> Words { get; set; }

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