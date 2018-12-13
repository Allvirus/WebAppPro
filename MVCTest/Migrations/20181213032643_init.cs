using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCTest.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisaForm",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    app_type = table.Column<string>(nullable: true),
                    centre = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true),
                    phone_code = table.Column<string>(nullable: true),
                    phone = table.Column<string>(maxLength: 11, nullable: false),
                    email = table.Column<string>(nullable: false),
                    member = table.Column<string>(nullable: false),
                    app_date = table.Column<DateTime>(nullable: false),
                    app_date_hidden = table.Column<DateTime>(nullable: false),
                    app_time = table.Column<string>(nullable: false),
                    captcha = table.Column<string>(nullable: false),
                    countryID = table.Column<string>(nullable: false),
                    dateOfBirth = table.Column<DateTime>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    loc_final = table.Column<string>(nullable: false),
                    loc_selected = table.Column<string>(nullable: false),
                    mission_selected = table.Column<string>(nullable: false),
                    missionId = table.Column<string>(nullable: false),
                    nationalityId = table.Column<string>(nullable: false),
                    passport_no = table.Column<string>(nullable: false),
                    passportType = table.Column<string>(nullable: false),
                    pptExpiryDate = table.Column<DateTime>(nullable: false),
                    pptIssueDate = table.Column<DateTime>(nullable: false),
                    pptIssuePalace = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaForm", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisaForm");
        }
    }
}
