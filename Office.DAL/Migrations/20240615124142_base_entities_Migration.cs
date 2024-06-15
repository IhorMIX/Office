using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Office.DAL.Migrations
{
    /// <inheritdoc />
    public partial class base_entities_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeoplePartnerID = table.Column<int>(type: "int", nullable: true),
                    OutOfOfficeBalance = table.Column<int>(type: "int", nullable: true),
                    ManagedEmployeesCount = table.Column<int>(type: "int", nullable: true),
                    CurrentProjectsCount = table.Column<int>(type: "int", nullable: true),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
