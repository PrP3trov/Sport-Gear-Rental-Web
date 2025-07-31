using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportGearRental.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithCorrectOwners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "SportGears",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3d4e5f6-7890-4abc-def1-234567890abc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc35b38d-21c4-4cc0-ae6e-a804e0d91dd9", "AQAAAAIAAYagAAAAEId8QT3dULt5TRbghc2uSnnKQKINd93ayX42ku5Z6Ee7aHEJczku+cNz1X7U4/VJWw==", "cd85155f-0102-4401-8d46-82f5b3364e5e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4e5f6a7-8901-4bcd-efa2-34567890bcde",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "047da4a6-6064-4ba8-a44e-87f62c2dee5e", "AQAAAAIAAYagAAAAEMJ+fF+DkXVc74s4pUCsP4JqY/8OfBZDbmRha1rHB+1yQKCbEYWveFwkk2biZNb97A==", "b10c04a9-3b67-4b2c-9955-edff42bcfc56" });

            migrationBuilder.UpdateData(
                table: "SportGears",
                keyColumn: "Id",
                keyValue: new Guid("1679091c-5a88-4e3e-96a4-7f3b3e7d9d3f"),
                column: "OwnerId",
                value: "c3d4e5f6-7890-4abc-def1-234567890abc");

            migrationBuilder.UpdateData(
                table: "SportGears",
                keyColumn: "Id",
                keyValue: new Guid("1dcca233-c2a1-4f1e-9d9f-c2147b0ccf8a"),
                column: "OwnerId",
                value: "c3d4e5f6-7890-4abc-def1-234567890abc");

            migrationBuilder.UpdateData(
                table: "SportGears",
                keyColumn: "Id",
                keyValue: new Guid("37b51d19-59a7-4ed4-8996-0b1d0c428a92"),
                column: "OwnerId",
                value: "d4e5f6a7-8901-4bcd-efa2-34567890bcde");

            migrationBuilder.UpdateData(
                table: "SportGears",
                keyColumn: "Id",
                keyValue: new Guid("73feffa4-7f1b-4e14-90c6-b42b041bf63f"),
                column: "OwnerId",
                value: "c3d4e5f6-7890-4abc-def1-234567890abc");

            migrationBuilder.UpdateData(
                table: "SportGears",
                keyColumn: "Id",
                keyValue: new Guid("8e296a06-2b87-4f7d-bb57-1a7b1c5ca6e9"),
                column: "OwnerId",
                value: "c3d4e5f6-7890-4abc-def1-234567890abc");

            migrationBuilder.UpdateData(
                table: "SportGears",
                keyColumn: "Id",
                keyValue: new Guid("8f14e45f-ceea-4bfc-9274-b7987d4a59d9"),
                column: "OwnerId",
                value: "d4e5f6a7-8901-4bcd-efa2-34567890bcde");

            migrationBuilder.UpdateData(
                table: "SportGears",
                keyColumn: "Id",
                keyValue: new Guid("a87ff679-a2f3-4f54-8e8f-0fa6d8b7cd55"),
                column: "OwnerId",
                value: "c3d4e5f6-7890-4abc-def1-234567890abc");

            migrationBuilder.UpdateData(
                table: "SportGears",
                keyColumn: "Id",
                keyValue: new Guid("acbd18db-4cc2-43e2-a05d-dcbbd298db96"),
                column: "OwnerId",
                value: "d4e5f6a7-8901-4bcd-efa2-34567890bcde");

            migrationBuilder.UpdateData(
                table: "SportGears",
                keyColumn: "Id",
                keyValue: new Guid("b6d81b36-1b9e-4f1d-9e0f-5b3a9d3b21d6"),
                column: "OwnerId",
                value: "c3d4e5f6-7890-4abc-def1-234567890abc");

            migrationBuilder.UpdateData(
                table: "SportGears",
                keyColumn: "Id",
                keyValue: new Guid("e4da3b7f-bbce-4a1b-b0f4-35e1d9f2b35a"),
                column: "OwnerId",
                value: "c3d4e5f6-7890-4abc-def1-234567890abc");

            migrationBuilder.CreateIndex(
                name: "IX_SportGears_OwnerId",
                table: "SportGears",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SportGears_AspNetUsers_OwnerId",
                table: "SportGears",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportGears_AspNetUsers_OwnerId",
                table: "SportGears");

            migrationBuilder.DropIndex(
                name: "IX_SportGears_OwnerId",
                table: "SportGears");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "SportGears");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3d4e5f6-7890-4abc-def1-234567890abc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13b3ec7d-5972-42e7-9ef3-0376af86026b", "AQAAAAIAAYagAAAAEH5Z6eAxisbHSUeASSuEppFmGQ4K0Y7VQxKRnfcT+QnrDD/jX/zY+pmsaU0TJ2E7PA==", "effb48d5-153c-4989-82ec-fc4c720d833c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4e5f6a7-8901-4bcd-efa2-34567890bcde",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80412610-b9ec-4bf3-ac25-991020437e42", "AQAAAAIAAYagAAAAEMiwT+mgQeB7CnUIkLunMg0mvjPHovvHYguUov5uLssHSXNR71bvO1roESoJIura8g==", "a206dcbe-3943-4ffd-aca3-cffbfac6b05f" });
        }
    }
}
