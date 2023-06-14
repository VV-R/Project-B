using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirplaneApp.Migrations
{
    /// <inheritdoc />
    public partial class addIsDelayedFieldToFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelayed",
                table: "Flights",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelayed",
                table: "Flights");
        }
    }
}
