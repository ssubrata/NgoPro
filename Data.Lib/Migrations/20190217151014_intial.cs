using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Data.Lib.Migrations
{
    public partial class intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberNo = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    ContactNo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NationalNo = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: true),
                    PresentAddress = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_MemberNo",
                table: "Members",
                column: "MemberNo",
                unique: true,
                filter: "[MemberNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Members_NationalNo",
                table: "Members",
                column: "NationalNo",
                unique: true,
                filter: "[NationalNo] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
