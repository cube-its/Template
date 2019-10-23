using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyName.ProjectName.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 78, nullable: false),
                    Phone = table.Column<string>(maxLength: 15, nullable: false),
                    SmsCode = table.Column<string>(maxLength: 20, nullable: true),
                    SmsCodeExpiredOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    SmsCodePassedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsCubeUser = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
