using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ICS_project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    ImageURL = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectAmounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectAmounts_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectAmounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagAmounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ActivityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagAmounts_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagAmounts_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("03ac647a-3807-4079-9b27-78942ea34fb9"), "IFJ" },
                    { new Guid("7baee999-46ab-447e-9107-0efab35044bf"), "ICS" },
                    { new Guid("e1aa63e9-d011-46c6-a502-374806e86fd5"), "IZU" },
                    { new Guid("fe63da7b-d028-4cea-b7ac-e597a75e4d40"), "IPP" }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("110f6520-9cb3-401d-8abc-6950f8d4cbff"), "Work", new Guid("f3a9d51d-4c23-4f89-89c1-b59f3a0b8e9e") },
                    { new Guid("15a17df4-fce6-4f69-a8c9-9e026b0ad2e4"), "School", new Guid("854c99f8-30a1-48e9-9975-5c5ba0d7e703") },
                    { new Guid("d0ebd9b5-05aa-4d53-b5a5-8afd0231bc3f"), "Mischief", new Guid("e82ed712-0d16-49b2-8a27-ec99742cf9a4") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ImageURL", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("79267a2c-bc56-46f5-8e7c-1020512f9f38"), "https://as2.ftcdn.net/v2/jpg/00/60/59/01/1000_F_60590191_YKoAK1ZcOkyR4PDQcmVfXszbqOk1uSbJ.jpg", "Petr", "Novak" },
                    { new Guid("854c99f8-30a1-48e9-9975-5c5ba0d7e703"), "https://static.gigwise.com/artists/Editors_7_quesada_750.jpg", "Tom", "Smith" },
                    { new Guid("e82ed712-0d16-49b2-8a27-ec99742cf9a4"), "https://images.squarespace-cdn.com/content/v1/5ceffd427a27670001628fb2/1561716917340-U5L6LYWZQL3D5JC7T3RR/download.jpeg", "Darina", "Novotna" },
                    { new Guid("f3a9d51d-4c23-4f89-89c1-b59f3a0b8e9e"), "https://babewings.cz/wp-content/uploads/2020/03/Super-Woman-obe-oci.jpg", "Klara", "Konecna" }
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Description", "End", "Name", "ProjectId", "Start", "UserId" },
                values: new object[,]
                {
                    { new Guid("110f6520-9cb3-401d-8abc-6950f8d4cbff"), null, new DateTime(2021, 1, 25, 6, 30, 50, 0, DateTimeKind.Unspecified), "Skating", new Guid("7baee999-46ab-447e-9107-0efab35044bf"), new DateTime(2021, 1, 25, 4, 50, 50, 0, DateTimeKind.Unspecified), new Guid("e82ed712-0d16-49b2-8a27-ec99742cf9a4") },
                    { new Guid("5cfd2fbd-59b1-481c-94fe-8cdf0fed3854"), null, new DateTime(2023, 5, 23, 7, 30, 50, 0, DateTimeKind.Unspecified), "Skiing", new Guid("03ac647a-3807-4079-9b27-78942ea34fb9"), new DateTime(2023, 5, 23, 4, 34, 50, 0, DateTimeKind.Unspecified), new Guid("854c99f8-30a1-48e9-9975-5c5ba0d7e703") },
                    { new Guid("d60a30d9-7b9c-4635-a7b3-3d89833d672c"), null, new DateTime(2023, 5, 30, 5, 30, 50, 0, DateTimeKind.Unspecified), "Programming", new Guid("03ac647a-3807-4079-9b27-78942ea34fb9"), new DateTime(2023, 5, 30, 3, 34, 50, 0, DateTimeKind.Unspecified), new Guid("854c99f8-30a1-48e9-9975-5c5ba0d7e703") }
                });

            migrationBuilder.InsertData(
                table: "ProjectAmounts",
                columns: new[] { "Id", "ProjectId", "UserId" },
                values: new object[,]
                {
                    { new Guid("08ca6f49-6d8e-4a92-bbfe-35e15adfb4a4"), new Guid("03ac647a-3807-4079-9b27-78942ea34fb9"), new Guid("854c99f8-30a1-48e9-9975-5c5ba0d7e703") },
                    { new Guid("a4946474-12fd-4a11-a2ae-dd4aa79a1b4e"), new Guid("03ac647a-3807-4079-9b27-78942ea34fb9"), new Guid("f3a9d51d-4c23-4f89-89c1-b59f3a0b8e9e") },
                    { new Guid("a7be96a2-3ab5-4617-b42c-856fea8ea86c"), new Guid("7baee999-46ab-447e-9107-0efab35044bf"), new Guid("e82ed712-0d16-49b2-8a27-ec99742cf9a4") }
                });

            migrationBuilder.InsertData(
                table: "TagAmounts",
                columns: new[] { "Id", "ActivityId", "TagId" },
                values: new object[,]
                {
                    { new Guid("4c6b11fb-07a3-4d2c-a9e6-1c6547c94721"), new Guid("d60a30d9-7b9c-4635-a7b3-3d89833d672c"), new Guid("15a17df4-fce6-4f69-a8c9-9e026b0ad2e4") },
                    { new Guid("5c8a9383-b2e4-42dc-a012-3f47bf45db6d"), new Guid("5cfd2fbd-59b1-481c-94fe-8cdf0fed3854"), new Guid("110f6520-9cb3-401d-8abc-6950f8d4cbff") },
                    { new Guid("a7d02af9-8709-4662-a8eb-945a14a67b02"), new Guid("110f6520-9cb3-401d-8abc-6950f8d4cbff"), new Guid("d0ebd9b5-05aa-4d53-b5a5-8afd0231bc3f") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ProjectId",
                table: "Activities",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserId",
                table: "Activities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAmounts_ProjectId",
                table: "ProjectAmounts",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAmounts_UserId",
                table: "ProjectAmounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TagAmounts_ActivityId",
                table: "TagAmounts",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_TagAmounts_TagId",
                table: "TagAmounts",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectAmounts");

            migrationBuilder.DropTable(
                name: "TagAmounts");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
