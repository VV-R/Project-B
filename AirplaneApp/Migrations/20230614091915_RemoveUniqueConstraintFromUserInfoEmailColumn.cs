using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirplaneApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueConstraintFromUserInfoEmailColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserInfo_Email",
                table: "UserInfo");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_Email",
                table: "UserInfo",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserInfo_Email",
                table: "UserInfo");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_Email",
                table: "UserInfo",
                column: "Email",
                unique: true);
        }
    }
}
