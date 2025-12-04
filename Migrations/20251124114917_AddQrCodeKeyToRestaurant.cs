using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KundenUmfrageTool.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddQrCodeKeyToRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QrCodeKey",
                table: "Restaurants",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QrCodeKey",
                table: "Restaurants");
        }
    }
}
