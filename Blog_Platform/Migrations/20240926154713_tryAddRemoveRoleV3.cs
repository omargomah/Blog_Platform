using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_Platform.Migrations
{
    /// <inheritdoc />
    public partial class tryAddRemoveRoleV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "token",
                table: "Tokens",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "token",
                table: "Tokens",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER");
        }
    }
}
