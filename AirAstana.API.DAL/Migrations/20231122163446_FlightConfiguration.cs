using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirAstana.API.DAL.Migrations
{
    public partial class FlightConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fc463d53-f8c6-4056-8ddb-2919a8f22dbe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d121d0bf-898c-4bc2-b277-e23f59681458");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d978289f-66f6-4547-a07a-bd80356dd144", "AQAAAAEAACcQAAAAEMorw4ddQhLnOGM3cQbvDokzYv1d7dd5B2CZi6KdovaQUWeIGZucGLO8XH6AuoXq2g==", "edd0ae41-19f1-4f82-b6cd-bd4fe848d090" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ebecd235-871f-48f7-beb9-6b23a61ce3f7", "AQAAAAEAACcQAAAAEIy7eOSm3w+4/pVQj8+qgTqPS2s/JEQ3ev5qJbwDOH9XWYKVxCJeeuyIBxE6ZbZ9pA==", "2d5e0677-afdf-4912-a7d5-509a39b72996" });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "Arrival", "Departure", "Destination", "Origin", "Status" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2021, 10, 10, 12, 10, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 10, 10, 10, 10, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Nur-Sultan", "Almaty", 1 },
                    { 2, new DateTimeOffset(new DateTime(2021, 10, 10, 12, 10, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 10, 10, 10, 10, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Nur-Sultan", "Almaty", 1 },
                    { 3, new DateTimeOffset(new DateTime(2021, 10, 10, 12, 10, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 10, 10, 10, 10, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Nur-Sultan", "Almaty", 1 },
                    { 4, new DateTimeOffset(new DateTime(2021, 10, 10, 12, 10, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 10, 10, 10, 10, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Nur-Sultan", "Almaty", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ee71543c-e4de-4380-a0c1-9fca62b583b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "fdf86af1-e820-46d4-8107-5aaef6fe4b08");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fcfb2597-de57-43a2-8453-61d3083304da", "AQAAAAEAACcQAAAAELsYDcKqlbvc4MpvVrF5mdKjmy6GvazKtloBqz6U/T3phyFP3F6d5UIzPVzP0cK4JA==", "1f678460-26bd-4267-b89f-4dff03753dcf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48cc59ec-f44c-496f-914e-d517de89b0b7", "AQAAAAEAACcQAAAAEBoFkRyy18syJebO03VOtCqmYyRufrfHnpGgjnf4RarNNE2QqMACXbi4aj/dHmg+4Q==", "396128e2-2417-45ae-a7d8-4c5fd8511300" });
        }
    }
}
