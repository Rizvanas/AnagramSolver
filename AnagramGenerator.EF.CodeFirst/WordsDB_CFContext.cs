using AnagramGenerator.EF.CodeFirst.Configurations;
using AnagramGenerator.EF.CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnagramGenerator.EF.CodeFirst
{
    public class WordsDB_CFContext : DbContext
    {
        public WordsDB_CFContext(DbContextOptions<WordsDB_CFContext> options)
            : base(options)
        { }

        public virtual DbSet<AnagramEntity> Anagrams { get; set; }
        public virtual DbSet<CachedWordEntity> CachedWords { get; set; }
        public virtual DbSet<PhraseEntity> Phrases { get; set; }
        public virtual DbSet<UserLogEntity> UserLog { get; set; }
        public virtual DbSet<WordEntity> Words { get; set; }

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
