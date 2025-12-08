using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasman.Migrations
{
    /// <inheritdoc />
    public partial class AddGalleryImageUrlsToTravel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ✨ ADD THE NEW COLUMNS
            migrationBuilder.AddColumn<string>(
                name: "GalleryImage1Url", // New column name
                table: "TravelDestinations", // Table to add to
                type: "TEXT", // Data type
                nullable: true); // Allow nulls (it's often safer)

            migrationBuilder.AddColumn<string>(
                name: "GalleryImage2Url", // New column name
                table: "TravelDestinations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryImage3Url", // New column name
                table: "TravelDestinations",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ✨ REMOVE THE COLUMNS IF ROLLING BACK
            migrationBuilder.DropColumn(
                name: "GalleryImage1Url",
                table: "TravelDestinations");

            migrationBuilder.DropColumn(
                name: "GalleryImage2Url",
                table: "TravelDestinations");

            migrationBuilder.DropColumn(
                name: "GalleryImage3Url",
                table: "TravelDestinations");
        }
    }
}