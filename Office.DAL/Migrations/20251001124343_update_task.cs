using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Office.DAL.Migrations
{
    /// <inheritdoc />
    public partial class update_task : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_BaseEmployees_EmployeeId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_EmployeeId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Tasks");

            migrationBuilder.CreateTable(
                name: "EmployeeTaskEntity",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    TasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTaskEntity", x => new { x.EmployeesId, x.TasksId });
                    table.ForeignKey(
                        name: "FK_EmployeeTaskEntity_BaseEmployees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "BaseEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTaskEntity_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTaskEntity_TasksId",
                table: "EmployeeTaskEntity",
                column: "TasksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTaskEntity");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EmployeeId",
                table: "Tasks",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_BaseEmployees_EmployeeId",
                table: "Tasks",
                column: "EmployeeId",
                principalTable: "BaseEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
