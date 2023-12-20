using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SVSTTest02.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GAS_VALUES",
                columns: table => new
                {
                    GAS_VAL_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GAS_VAL_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    H2_VAL = table.Column<double>(type: "double precision", nullable: false),
                    O2_VAL = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GAS_VALUES", x => x.GAS_VAL_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GAS_VALUES");
        }
    }
}
