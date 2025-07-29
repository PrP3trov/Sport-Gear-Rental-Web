using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportGearRental.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3d4e5f6-7890-4abc-def1-234567890abc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2aaea8e5-413e-42ad-8583-596325acef28", "AQAAAAIAAYagAAAAEK+SxF5jcdqlIboWN+FJqBRQl9OtHrMYNnAcaRDy/SmXFqwxJ0f9IQdnro4dXie5jQ==", "335729ab-3555-47ec-b5df-ab90a98c6c9a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4e5f6a7-8901-4bcd-efa2-34567890bcde",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "daa34453-b3f2-4c7b-ab10-a598812a4c32", "AQAAAAIAAYagAAAAEIJ6GDOBeJI6As3qDs7dWZ93SO6E+ltyZXa22JfynKMh55X+ZcYH7EVRIpDIGa7Uhw==", "398bbe4d-835e-481b-a9bf-25e0d23cd3f9" });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "IsDeleted", "RentalEndDate", "RentalStartDate", "SportGearId", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"), false, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8f14e45f-ceea-4bfc-9274-b7987d4a59d9"), 50.00m, "d4e5f6a7-8901-4bcd-efa2-34567890bcde" },
                    { new Guid("b2c3d4e5-f6a7-48b9-9c0d-1e2f3a4b5c6d"), false, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("acbd18db-4cc2-43e2-a05d-dcbbd298db96"), 75.50m, "d4e5f6a7-8901-4bcd-efa2-34567890bcde" },
                    { new Guid("c3d4e5f6-a7b8-49c0-ad1e-2f3a4b5c6d7e"), false, new DateTime(2025, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b6d81b36-1b9e-4f1d-9e0f-5b3a9d3b21d6"), 30.00m, "c3d4e5f6-7890-4abc-def1-234567890abc" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"));

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-48b9-9c0d-1e2f3a4b5c6d"));

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-49c0-ad1e-2f3a4b5c6d7e"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3d4e5f6-7890-4abc-def1-234567890abc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c40e5f27-a744-4069-9dea-377f2ec2d9c8", "AQAAAAIAAYagAAAAEKRyeZe/Ac1sdpBfspj792SoXmC+JrwT3RuhVDDv2X1WjXklm2phepACbg0L34ydzA==", "c4f46b2b-a334-480a-95ca-596c83184d1c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4e5f6a7-8901-4bcd-efa2-34567890bcde",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0df06fb6-2df9-4c62-9fdc-8452f6d4abe8", "AQAAAAIAAYagAAAAENblSsvmzdRGYPTmoEqonauX4yNjXvkzmRSgFrp6Id2r66wtaRFf2l4sHB7ydKBLiw==", "3325435e-1fce-4db4-92b5-46533efc0337" });
        }
    }
}
