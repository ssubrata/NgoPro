using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Lib.Migrations
{
    public partial class addLoandCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanCollection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoanCollectAmount = table.Column<decimal>(nullable: false),
                    LoanCollectDate = table.Column<DateTime>(nullable: false),
                    LoanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanCollection_Loan_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanCollection_LoanId",
                table: "LoanCollection",
                column: "LoanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanCollection");
        }
    }
}
