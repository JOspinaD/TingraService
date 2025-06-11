﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TingraService.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokensMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Usuario",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Usuario");
        }
    }
}
