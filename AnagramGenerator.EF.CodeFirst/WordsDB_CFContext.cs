using AnagramGenerator.EF.CodeFirst.Configurations;
using AnagramGenerator.EF.CodeFirst.Entities;
using Contracts;
using Implementation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AnagramGenerator.EF.CodeFirst
{
    public class WordsDB_CFContext : DbContext
    {
        private readonly IWordLoader _wordLoader;
        private readonly IAppConfig _appConfig;
        public WordsDB_CFContext(DbContextOptions<WordsDB_CFContext> options, IWordLoader wordLoader, IAppConfig appConfig)
            : base(options)
        {
            _wordLoader = wordLoader;
            _appConfig = appConfig;
        }

        public virtual DbSet<AnagramEntity> Anagrams { get; set; }
        public virtual DbSet<CachedWordEntity> CachedWords { get; set; }
        public virtual DbSet<PhraseEntity> Phrases { get; set; }
        public virtual DbSet<UserLogEntity> UserLog { get; set; }
        public virtual DbSet<WordEntity> Words { get; set; }
        public virtual DbSet<WordEntity> UserWords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new WordsConfiguration());
            modelBuilder.ApplyConfiguration(new AnagramsConfiguration());
            modelBuilder.ApplyConfiguration(new PhrasesConfiguration());
            modelBuilder.ApplyConfiguration(new CachedWordsConfiguration());
            modelBuilder.ApplyConfiguration(new UserLogConfiguration());
        }
    }
}
