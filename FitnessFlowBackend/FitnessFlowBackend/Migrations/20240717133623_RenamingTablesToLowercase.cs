using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessFlowBackend.Migrations
{
    /// <inheritdoc />
    public partial class RenamingTablesToLowercase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Users_UserId1",
                table: "Trainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainings",
                table: "Trainings");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Trainings",
                newName: "trainings");

            migrationBuilder.RenameIndex(
                name: "IX_Trainings_UserId1",
                table: "trainings",
                newName: "IX_trainings_UserId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainings",
                table: "trainings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_trainings_users_UserId1",
                table: "trainings",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trainings_users_UserId1",
                table: "trainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainings",
                table: "trainings");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "trainings",
                newName: "Trainings");

            migrationBuilder.RenameIndex(
                name: "IX_trainings_UserId1",
                table: "Trainings",
                newName: "IX_Trainings_UserId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainings",
                table: "Trainings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Users_UserId1",
                table: "Trainings",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
