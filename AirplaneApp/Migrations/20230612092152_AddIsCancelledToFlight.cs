using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirplaneApp.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCancelledToFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Flights",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Flights");
        }
    }
}
