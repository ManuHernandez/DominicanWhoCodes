using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DominicanWhoCodes.Profiles.API.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UsersProfile");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "UsersProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 80, nullable: false),
                    LastName = table.Column<string>(maxLength: 250, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                schema: "UsersProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    ImageSource = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(maxLength: 250, nullable: false),
                    UserAssignedId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_UserAssignedId",
                        column: x => x.UserAssignedId,
                        principalSchema: "UsersProfile",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialNetworks",
                schema: "UsersProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Network = table.Column<int>(nullable: false),
                    Url = table.Column<string>(maxLength: 250, nullable: false),
                    UserAssignedId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialNetworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialNetworks_Users_UserAssignedId",
                        column: x => x.UserAssignedId,
                        principalSchema: "UsersProfile",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserAssignedId",
                schema: "UsersProfile",
                table: "Photos",
                column: "UserAssignedId",
                unique: true,
                filter: "[UserAssignedId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SocialNetworks_UserAssignedId",
                schema: "UsersProfile",
                table: "SocialNetworks",
                column: "UserAssignedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos",
                schema: "UsersProfile");

            migrationBuilder.DropTable(
                name: "SocialNetworks",
                schema: "UsersProfile");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "UsersProfile");
        }
    }
}
