using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailApp.DataAccess.Migrations
{
    public partial class AddMeInSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address" },
                values: new object[] { 3, "me@me.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
