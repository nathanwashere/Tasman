using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasman.Migrations
{
    /// <inheritdoc />
    public partial class AdminEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookableDaysBeforeStart",
                table: "TravelDestinations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "CancellableDaysBeforeStart",
                table: "TravelDestinations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "DiscountPrice",
                table: "TravelDestinations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DiscountEndsAt",
                table: "TravelDestinations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "TravelDestinations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecommended",
                table: "TravelDestinations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "TravelDestinations",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookableDaysBeforeStart",
                table: "TravelDestinations");

            migrationBuilder.DropColumn(
                name: "CancellableDaysBeforeStart",
                table: "TravelDestinations");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "TravelDestinations");

            migrationBuilder.DropColumn(
                name: "DiscountEndsAt",
                table: "TravelDestinations");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "TravelDestinations");

            migrationBuilder.DropColumn(
                name: "IsRecommended",
                table: "TravelDestinations");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "TravelDestinations");
        }
    }
}
