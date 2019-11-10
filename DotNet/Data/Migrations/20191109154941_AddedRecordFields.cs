using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddedRecordFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Patient",
                table: "Records",
                newName: "TesutConjunctiv");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Records",
                newName: "CreationDate");

            migrationBuilder.AddColumn<string>(
                name: "Anamneza",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AparatCardiovascular",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AparatDigestiv",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AparatRespirator",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AparatUroGeneral",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Constienta",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facies",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fanere",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FicatSplina",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Greutate",
                table: "Records",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IstoriculBolii",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotiveleInternarii",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mucoase",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nutritie",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PatientId",
                table: "Records",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SistemEndocrin",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SistemGanglionar",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SistemMuscular",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SistemOsteoArticular",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StareaGenerala",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Talie",
                table: "Records",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tegumente",
                table: "Records",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anamneza",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "AparatCardiovascular",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "AparatDigestiv",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "AparatRespirator",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "AparatUroGeneral",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Constienta",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Facies",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Fanere",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "FicatSplina",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Greutate",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "IstoriculBolii",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "MotiveleInternarii",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Mucoase",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Nutritie",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "SistemEndocrin",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "SistemGanglionar",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "SistemMuscular",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "SistemOsteoArticular",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "StareaGenerala",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Talie",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Tegumente",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "TesutConjunctiv",
                table: "Records",
                newName: "Patient");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Records",
                newName: "Date");
        }
    }
}
