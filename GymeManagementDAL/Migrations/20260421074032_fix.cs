using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymeManagementDAL.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSessions_Members_MemberID",
                table: "MemberSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSessions_Sessions_SessionID",
                table: "MemberSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSessions",
                table: "MemberSessions");

            migrationBuilder.RenameColumn(
                name: "SessionID",
                table: "MemberSessions",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "MemberID",
                table: "MemberSessions",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSessions_SessionID",
                table: "MemberSessions",
                newName: "IX_MemberSessions_SessionId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MemberSessions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSessions",
                table: "MemberSessions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSessions_MemberId",
                table: "MemberSessions",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSessions_Members_MemberId",
                table: "MemberSessions",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSessions_Sessions_SessionId",
                table: "MemberSessions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSessions_Members_MemberId",
                table: "MemberSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSessions_Sessions_SessionId",
                table: "MemberSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSessions",
                table: "MemberSessions");

            migrationBuilder.DropIndex(
                name: "IX_MemberSessions_MemberId",
                table: "MemberSessions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MemberSessions");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "MemberSessions",
                newName: "SessionID");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "MemberSessions",
                newName: "MemberID");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSessions_SessionId",
                table: "MemberSessions",
                newName: "IX_MemberSessions_SessionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSessions",
                table: "MemberSessions",
                columns: new[] { "MemberID", "SessionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSessions_Members_MemberID",
                table: "MemberSessions",
                column: "MemberID",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSessions_Sessions_SessionID",
                table: "MemberSessions",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
