using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddedKeywordFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Records");

            migrationBuilder.AddColumn<bool>(
                name: "Positive",
                table: "Keywords",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Keywords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Positive",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Keywords");

            migrationBuilder.AddColumn<Guid>(
                name: "PatientId",
                table: "Records",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
