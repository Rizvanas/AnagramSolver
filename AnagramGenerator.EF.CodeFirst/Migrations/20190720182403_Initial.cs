using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramGenerator.EF.CodeFirst.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anagrams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Anagram = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anagrams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phrases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Phrase = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phrases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Word = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordId);
                });

            migrationBuilder.CreateTable(
                name: "CachedWords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhraseId = table.Column<int>(nullable: false),
                    AnagramId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CachedWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CachedWords_Anagrams_AnagramId",
                        column: x => x.AnagramId,
                        principalTable: "Anagrams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CachedWords_Phrases_PhraseId",
                        column: x => x.PhraseId,
                        principalTable: "Phrases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserIp = table.Column<string>(unicode: false, maxLength: 15, nullable: false),
                    SearchPhraseId = table.Column<int>(nullable: false),
                    SearchTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLog_Phrases_SearchPhraseId",
                        column: x => x.SearchPhraseId,
                        principalTable: "Phrases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anagrams_Anagram",
                table: "Anagrams",
                column: "Anagram",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CachedWords_AnagramId",
                table: "CachedWords",
                column: "AnagramId");

            migrationBuilder.CreateIndex(
                name: "IX_CachedWords_PhraseId",
                table: "CachedWords",
                column: "PhraseId");

            migrationBuilder.CreateIndex(
                name: "IX_Phrases_Phrase",
                table: "Phrases",
                column: "Phrase",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLog_SearchPhraseId",
                table: "UserLog",
                column: "SearchPhraseId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_Word",
                table: "Words",
                column: "Word",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CachedWords");

            migrationBuilder.DropTable(
                name: "UserLog");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Anagrams");

            migrationBuilder.DropTable(
                name: "Phrases");
        }
    }
}
