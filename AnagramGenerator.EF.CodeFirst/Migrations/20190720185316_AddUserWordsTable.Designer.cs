﻿// <auto-generated />
using AnagramGenerator.EF.CodeFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnagramGenerator.EF.CodeFirst.Migrations
{
    [DbContext(typeof(WordsDB_CFContext))]
    [Migration("20190720185316_AddUserWordsTable")]
    partial class AddUserWordsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Entities.AnagramEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Anagram")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Anagram")
                        .IsUnique();

                    b.ToTable("Anagrams");
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Entities.CachedWordEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnagramId");

                    b.Property<int>("PhraseId");

                    b.HasKey("Id");

                    b.HasIndex("AnagramId");

                    b.HasIndex("PhraseId");

                    b.ToTable("CachedWords");
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Entities.PhraseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Phrase")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Phrase")
                        .IsUnique();

                    b.ToTable("Phrases");
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Entities.UserLogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SearchPhraseId");

                    b.Property<int>("SearchTime");

                    b.Property<string>("UserIp")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("SearchPhraseId");

                    b.ToTable("UserLog");
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Entities.WordEntity", b =>
                {
                    b.Property<int>("WordId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("WordId");

                    b.HasIndex("Word")
                        .IsUnique();

                    b.ToTable("Words");
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Entities.CachedWordEntity", b =>
                {
                    b.HasOne("AnagramGenerator.EF.CodeFirst.Entities.AnagramEntity", "Anagram")
                        .WithMany("CachedWords")
                        .HasForeignKey("AnagramId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnagramGenerator.EF.CodeFirst.Entities.PhraseEntity", "Phrase")
                        .WithMany("CachedWords")
                        .HasForeignKey("PhraseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Entities.UserLogEntity", b =>
                {
                    b.HasOne("AnagramGenerator.EF.CodeFirst.Entities.PhraseEntity", "SearchPhrase")
                        .WithMany("UserLogs")
                        .HasForeignKey("SearchPhraseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
