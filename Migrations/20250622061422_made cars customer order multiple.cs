using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FriBergs_CarRental.Migrations
{
    /// <inheritdoc />
    public partial class madecarscustomerordermultiple : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerOrder_CarId",
                table: "CustomerOrder");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_CarId",
                table: "CustomerOrder",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerOrder_CarId",
                table: "CustomerOrder");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_CarId",
                table: "CustomerOrder",
                column: "CarId",
                unique: true);
        }
    }
}
