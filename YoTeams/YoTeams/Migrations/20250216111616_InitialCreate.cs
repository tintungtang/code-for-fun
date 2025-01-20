using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YoTeams.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Name", "Role" },
                values: new object[,]
                {
                    { 1, "Member 1", "Designer" },
                    { 2, "Member 2", "Developer" },
                    { 3, "Member 3", "Designer" },
                    { 4, "Member 4", "Developer" },
                    { 5, "Member 5", "Designer" },
                    { 6, "Member 6", "Developer" },
                    { 7, "Member 7", "Designer" },
                    { 8, "Member 8", "Developer" },
                    { 9, "Member 9", "Designer" },
                    { 10, "Member 10", "Developer" },
                    { 11, "Member 11", "Designer" },
                    { 12, "Member 12", "Developer" },
                    { 13, "Member 13", "Designer" },
                    { 14, "Member 14", "Developer" },
                    { 15, "Member 15", "Designer" },
                    { 16, "Member 16", "Developer" },
                    { 17, "Member 17", "Designer" },
                    { 18, "Member 18", "Developer" },
                    { 19, "Member 19", "Designer" },
                    { 20, "Member 20", "Developer" },
                    { 21, "Member 21", "Designer" },
                    { 22, "Member 22", "Developer" },
                    { 23, "Member 23", "Designer" },
                    { 24, "Member 24", "Developer" },
                    { 25, "Member 25", "Designer" },
                    { 26, "Member 26", "Developer" },
                    { 27, "Member 27", "Designer" },
                    { 28, "Member 28", "Developer" },
                    { 29, "Member 29", "Designer" },
                    { 30, "Member 30", "Developer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "SocialItems");
        }
    }
}
