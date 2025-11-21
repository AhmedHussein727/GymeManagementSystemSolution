using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymeManagementDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeHelthRecordHightToHeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hight",
                table: "Members",
                newName: "Height");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Members",
                newName: "Hight");
        }
    }
}
