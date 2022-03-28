using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.API.Migrations
{
    public partial class SeededDefaultEntityUserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "108aeca5-61b7-4058-9564-54cde5ff5d78", "220f907f-056a-460e-9b4d-4b7687295475", "Administrator", "ADMINISTRATOR" },
                    { "c87afbef-b901-482f-a669-9d241d85dc06", "5cba233d-a07a-4844-8fd8-06f435a9ffc7", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2af25b2c-aea7-41eb-ba87-97fd6dcfc24f", 0, "9e6e11c9-d1af-4513-84e8-4445d686508e", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEMTMqpVlWSZ5tqiuRgL61GQkAW0qUDD0NqrB80QHTb+vbryUNUh+MDdQT988pbmxPA==", null, false, "828208e4-beb7-4739-81f1-dddefebfd564", false, "user@bookstore.com" },
                    { "e19bcb87-a979-49f7-8c6f-60f5802b3bec", 0, "dc59cb13-690c-47b7-a4b8-fec054cf62d0", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEDPkHyS+95DbN4FHa+OrIudJBOBkvxal8LgiNYkDd1yHGlQZY/oYlliVpQPwh1qdGA==", null, false, "56643fa5-8126-4760-bd06-140ce5f1e5bc", false, "admin@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c87afbef-b901-482f-a669-9d241d85dc06", "2af25b2c-aea7-41eb-ba87-97fd6dcfc24f" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "108aeca5-61b7-4058-9564-54cde5ff5d78", "e19bcb87-a979-49f7-8c6f-60f5802b3bec" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c87afbef-b901-482f-a669-9d241d85dc06", "2af25b2c-aea7-41eb-ba87-97fd6dcfc24f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "108aeca5-61b7-4058-9564-54cde5ff5d78", "e19bcb87-a979-49f7-8c6f-60f5802b3bec" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "108aeca5-61b7-4058-9564-54cde5ff5d78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c87afbef-b901-482f-a669-9d241d85dc06");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2af25b2c-aea7-41eb-ba87-97fd6dcfc24f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e19bcb87-a979-49f7-8c6f-60f5802b3bec");
        }
    }
}
