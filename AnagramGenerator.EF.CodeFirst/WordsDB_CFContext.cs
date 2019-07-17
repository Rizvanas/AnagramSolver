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
            modelBuilder.ApplyConfiguration();
            modelBuilder.ApplyConfiguration();
            modelBuilder.ApplyConfiguration();
            modelBuilder.ApplyConfiguration();
            modelBuilder.ApplyConfiguration();
        }
    }
}
