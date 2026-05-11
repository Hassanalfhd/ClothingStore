using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 514, DateTimeKind.Utc).AddTicks(853),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 808, DateTimeKind.Utc).AddTicks(303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 532, DateTimeKind.Utc).AddTicks(7045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 817, DateTimeKind.Utc).AddTicks(526));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 529, DateTimeKind.Utc).AddTicks(1258),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 814, DateTimeKind.Utc).AddTicks(9458));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 808, DateTimeKind.Utc).AddTicks(303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 514, DateTimeKind.Utc).AddTicks(853));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 817, DateTimeKind.Utc).AddTicks(526),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 532, DateTimeKind.Utc).AddTicks(7045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 2, 22, 42, 51, 814, DateTimeKind.Utc).AddTicks(9458),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 5, 7, 21, 20, 45, 529, DateTimeKind.Utc).AddTicks(1258));
        }
    }
}
