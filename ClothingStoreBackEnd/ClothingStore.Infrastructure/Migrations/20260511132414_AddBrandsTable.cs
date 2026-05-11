using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 11, 13, 24, 13, 975, DateTimeKind.Utc).AddTicks(3057),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 514, DateTimeKind.Utc).AddTicks(853));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 11, 13, 24, 14, 2, DateTimeKind.Utc).AddTicks(9189),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 532, DateTimeKind.Utc).AddTicks(7045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 11, 13, 24, 13, 997, DateTimeKind.Utc).AddTicks(9518),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 529, DateTimeKind.Utc).AddTicks(1258));

            migrationBuilder.AddColumn<long>(
                name: "BrandId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_PublicId",
                table: "Brands",
                column: "PublicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 514, DateTimeKind.Utc).AddTicks(853),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 11, 13, 24, 13, 975, DateTimeKind.Utc).AddTicks(3057));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 532, DateTimeKind.Utc).AddTicks(7045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 11, 13, 24, 14, 2, DateTimeKind.Utc).AddTicks(9189));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 529, DateTimeKind.Utc).AddTicks(1258),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 11, 13, 24, 13, 997, DateTimeKind.Utc).AddTicks(9518));
        }
    }
}
