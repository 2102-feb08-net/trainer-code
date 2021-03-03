using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailApp.DataAccess.Migrations
{
    public partial class NotNullUniqueAndData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address" },
                values: new object[] { 1, "fred@fred.com" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address" },
                values: new object[] { 2, "kevin@kevin.com" });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Body", "Date", "FromId", "Subject" },
                values: new object[] { 1, "Aenean elit massa, eleifend id feugiat a, semper in massa. Praesent ex lectus, vehicula eget mi ut, dictum commodo tortor. Sed congue leo ac mollis hendrerit. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Pellentesque ut magna non sapien efficitur ullamcorper. Donec elementum, purus aliquet facilisis auctor, massa justo finibus leo, a feugiat purus tortor vitae ante. Suspendisse ipsum nibh, tincidunt congue mattis ut, tristique in felis.", new DateTimeOffset(new DateTime(2021, 3, 1, 12, 58, 58, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), 1, "qc" });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Body", "Date", "FromId", "Subject" },
                values: new object[] { 2, "Donec egestas lorem viverra augue placerat interdum. Nulla id mollis purus. Quisque eget libero ultricies est tincidunt tempor. Integer lobortis sapien et pellentesque tincidunt. Nulla euismod pulvinar lorem sed pellentesque. Ut sit amet quam non elit pharetra cursus. In hac habitasse platea dictumst. Proin accumsan a justo ac molestie. Phasellus eu metus neque. Donec vel sollicitudin libero. Donec sed leo leo.", new DateTimeOffset(new DateTime(2021, 3, 1, 13, 0, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), 2, "RE: qc" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Address",
                table: "Accounts",
                column: "Address",
                unique: true,
                filter: "[Address] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_Address",
                table: "Accounts");

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
