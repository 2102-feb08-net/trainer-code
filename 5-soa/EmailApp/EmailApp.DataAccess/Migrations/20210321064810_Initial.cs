using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailApp.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrigDate = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: false),
                    FromId = table.Column<int>(type: "int", nullable: false),
                    ToId = table.Column<int>(type: "int", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Accounts_FromId",
                        column: x => x.FromId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Accounts_ToId",
                        column: x => x.ToId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address" },
                values: new object[] { 1, "nick.escalona@revature.com" });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Body", "FromId", "Guid", "OrigDate", "Subject", "ToId" },
                values: new object[] { 1, "this is a message to say hello", 1, new Guid("57d462ca-a9ce-4417-b8a4-d9b59907c7a6"), new DateTimeOffset(new DateTime(2021, 3, 20, 22, 37, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "hello", 1 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Body", "FromId", "Guid", "OrigDate", "Subject", "ToId" },
                values: new object[] { 2, "this is a reply to hello", 1, new Guid("bd682c41-68db-4c00-9dd2-814b8013e563"), new DateTimeOffset(new DateTime(2021, 3, 20, 22, 40, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Re: hello", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Address",
                table: "Accounts",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromId",
                table: "Messages",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Guid",
                table: "Messages",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToId",
                table: "Messages",
                column: "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
