using System;
using AnagramGenerator.EF.DatabaseFirst.Configurations;
using AnagramGenerator.EF.DatabaseFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AnagramGenerator.EF.DatabaseFirst
{
    public partial class WordsDBContext : DbContext
    {

        public WordsDBContext(DbContextOptions<WordsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnagramEntity> Anagrams { get; set; }
        public virtual DbSet<CachedWordEntity> CachedWords { get; set; }
        public virtual DbSet<PhraseEntity> Phrases { get; set; }
        public virtual DbSet<UserLogEntity> UserLogs { get; set; }
        public virtual DbSet<UserWordEntity> UserWords { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<WordEntity> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=WordsDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.ApplyConfiguration(new AnagramsConfiguration());
            modelBuilder.ApplyConfiguration(new CachedWordsConfiguration());
            modelBuilder.ApplyConfiguration(new PhrasesConfiguration());
            modelBuilder.ApplyConfiguration(new UserLogConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new UserWordsConfiguration());
            modelBuilder.ApplyConfiguration(new WordsConfiguration());

        }
    }
}
