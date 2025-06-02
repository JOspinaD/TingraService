using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TingraService.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldsEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dirreccion",
                table: "Empresa",
                newName: "Direccion");

            migrationBuilder.AddColumn<string>(
                name: "AspectosMejorar",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CapacitacionRecibida",
                table: "Empresa",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Capacitadores",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Disponibilidad",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaCreacion",
                table: "Empresa",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaLlamada",
                table: "Empresa",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PerteneceRedEmprendedores",
                table: "Empresa",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Propietario",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RedEmprendedoresConfirmada",
                table: "Empresa",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RedesSociales",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Servicios",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspectosMejorar",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "CapacitacionRecibida",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Capacitadores",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Disponibilidad",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "FechaLlamada",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "PerteneceRedEmprendedores",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Propietario",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "RedEmprendedoresConfirmada",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "RedesSociales",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Servicios",
                table: "Empresa");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "Empresa",
                newName: "Dirreccion");
        }
    }
}
