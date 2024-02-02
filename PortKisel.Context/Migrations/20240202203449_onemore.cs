using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortKisel.Context.Migrations
{
    /// <inheritdoc />
    public partial class onemore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cargo_Weight",
                table: "Vessel");

            migrationBuilder.RenameIndex(
                name: "IX_Cargo_Name",
                table: "Vessel",
                newName: "IX_Vessel_Name");

            migrationBuilder.CreateIndex(
                name: "IX_Vessel_LoadCapacity",
                table: "Vessel",
                column: "LoadCapacity",
                filter: "DeletedAt is null");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vessel_LoadCapacity",
                table: "Vessel");

            migrationBuilder.RenameIndex(
                name: "IX_Vessel_Name",
                table: "Vessel",
                newName: "IX_Cargo_Name");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_Weight",
                table: "Vessel",
                column: "LoadCapacity",
                unique: true,
                filter: "DeletedAt is null");
        }
    }
}
