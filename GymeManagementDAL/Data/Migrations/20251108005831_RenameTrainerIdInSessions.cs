using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymeManagementDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTrainerIdInSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Trainers_tarainerID",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "tarainerID",
                table: "Sessions",
                newName: "TrainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_tarainerID",
                table: "Sessions",
                newName: "IX_Sessions_TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Trainers_TrainerId",
                table: "Sessions",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Trainers_TrainerId",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "TrainerId",
                table: "Sessions",
                newName: "tarainerID");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_TrainerId",
                table: "Sessions",
                newName: "IX_Sessions_tarainerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Trainers_tarainerID",
                table: "Sessions",
                column: "tarainerID",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
