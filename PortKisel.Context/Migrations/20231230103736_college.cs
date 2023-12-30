using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortKisel.Context.Migrations
{
    /// <inheritdoc />
    public partial class college : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responsible_cargo",
                table: "Documenti");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Responsible_cargo",
                table: "Documenti",
                type: "int",
                nullable: true);
        }
    }
}
