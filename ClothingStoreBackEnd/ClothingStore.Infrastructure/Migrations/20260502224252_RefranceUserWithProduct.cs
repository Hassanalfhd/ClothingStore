using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefranceUserWithProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_UserProfiles_UserProfileId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserProfileId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 808, DateTimeKind.Utc).AddTicks(303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 1, 21, 2, 31, 633, DateTimeKind.Utc).AddTicks(3243));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 817, DateTimeKind.Utc).AddTicks(526),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 1, 21, 2, 31, 641, DateTimeKind.Utc).AddTicks(8390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 814, DateTimeKind.Utc).AddTicks(9458),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 1, 21, 2, 31, 639, DateTimeKind.Utc).AddTicks(8919));

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_CreatedBy",
                table: "ProductVariants",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedBy",
                table: "Products",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_UserProfiles_CreatedBy",
                table: "Products",
                column: "CreatedBy",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_UserProfiles_CreatedBy",
                table: "ProductVariants",
                column: "CreatedBy",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_UserProfiles_CreatedBy",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_UserProfiles_CreatedBy",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_CreatedBy",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedBy",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 1, 21, 2, 31, 633, DateTimeKind.Utc).AddTicks(3243),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 808, DateTimeKind.Utc).AddTicks(303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 1, 21, 2, 31, 641, DateTimeKind.Utc).AddTicks(8390),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 817, DateTimeKind.Utc).AddTicks(526));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 1, 21, 2, 31, 639, DateTimeKind.Utc).AddTicks(8919),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 814, DateTimeKind.Utc).AddTicks(9458));

            migrationBuilder.AddColumn<long>(
                name: "UserProfileId",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserProfileId",
                table: "Products",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_UserProfiles_UserProfileId",
                table: "Products",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
