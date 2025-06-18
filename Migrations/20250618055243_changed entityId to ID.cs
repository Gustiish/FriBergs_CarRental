using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FriBergs_CarRental.Migrations
{
    /// <inheritdoc />
    public partial class changedentityIdtoID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrder_Car_CarEntityId",
                table: "CustomerOrder");

            migrationBuilder.RenameColumn(
                name: "CarEntityId",
                table: "CustomerOrder",
                newName: "CarId");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "CustomerOrder",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerOrder_CarEntityId",
                table: "CustomerOrder",
                newName: "IX_CustomerOrder_CarId");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "Car",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrder_Car_CarId",
                table: "CustomerOrder",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrder_Car_CarId",
                table: "CustomerOrder");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "CustomerOrder",
                newName: "CarEntityId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CustomerOrder",
                newName: "EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerOrder_CarId",
                table: "CustomerOrder",
                newName: "IX_CustomerOrder_CarEntityId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Car",
                newName: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrder_Car_CarEntityId",
                table: "CustomerOrder",
                column: "CarEntityId",
                principalTable: "Car",
                principalColumn: "EntityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
