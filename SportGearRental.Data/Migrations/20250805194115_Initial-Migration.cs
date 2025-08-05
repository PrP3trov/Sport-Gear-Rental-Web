using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportGearRental.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Username of the user"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is the account deleted or not"),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GearConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportGears",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The name of the gear"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The description of the gear"),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "The price per day of the gear"),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true, comment: "The image URL of the gear"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is the entity deleted (soft delete)?"),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportGears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportGears_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SportGears_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SportGears_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SportGears_GearConditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "GearConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SportGearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is the entity deleted (soft delete)?")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_SportGears_SportGearId",
                        column: x => x.SportGearId,
                        principalTable: "SportGears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SportGearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RentalStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentalEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is the entity deleted (soft delete)?"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_SportGears_SportGearId",
                        column: x => x.SportGearId,
                        principalTable: "SportGears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is the entity deleted (soft delete)?"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SportGearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_SportGears_SportGearId",
                        column: x => x.SportGearId,
                        principalTable: "SportGears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a1f2e3d4-c5b6-47f8-9876-123456789abc", null, "Admin", "ADMIN" },
                    { "b2f3e4d5-a6b7-48c9-8765-abcdef123456", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "c3d4e5f6-7890-4abc-def1-234567890abc", 0, "c6e1728e-7f9b-4a00-bcff-0cbad12398f1", "admin@gear.bg", true, false, false, null, "ADMIN@GEAR.BG", "ADMIN@GEAR.BG", "AQAAAAIAAYagAAAAEAXfE1MIsuOaZlGFTSazmiuatT/+9RSfC5uRw+cvuV1hToNzDceW2tCgQ2/50s+1vQ==", null, false, "d33cff64-636e-4fa7-95a8-f3b837b5ddc4", false, "admin@gear.bg" },
                    { "d4e5f6a7-8901-4bcd-efa2-34567890bcde", 0, "d0b0500b-d4ef-4835-9aa7-6e6f025eeb39", "user@gear.bg", true, false, false, null, "USER@GEAR.BG", "USER@GEAR.BG", "AQAAAAIAAYagAAAAEKEQvbUYBdBSXfqBtUVsqaeiP18ZwoB6otJ3oKf17JKMJTXAunqAE79ZluYNjU+eBw==", null, false, "2b8bb44c-4702-46e4-8702-21b5ecdf1806", false, "user@gear.bg" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0d9a5b76-48b1-4eea-8c1d-cdff4a56b57a"), "Саломон" },
                    { new Guid("1c2d0d89-62e6-4e3b-8fcd-125c5bb8f2a1"), "Колумбия" },
                    { new Guid("3d6f0a88-9d64-4a38-9f2c-52deff0a92d2"), "Пума" },
                    { new Guid("c9bf9e57-1685-4c89-bafb-ff5af830be8a"), "Найк" },
                    { new Guid("e358efa4-1e22-4ac1-8f98-cd78e9a6ccf3"), "Адидас" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "Фитнес оборудване" },
                    { new Guid("6fa459ea-ee8a-3ca4-894e-db77e160355e"), "Велосипеди" },
                    { new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"), "Аксесоари" },
                    { new Guid("9c858901-8a57-4791-81fe-4c455b099bc9"), "Дрехи" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), "Обувки" }
                });

            migrationBuilder.InsertData(
                table: "GearConditions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("22a963d9-2a3b-4baf-943d-9e6a51b8db78"), "С видими следи от употреба", "Средно" },
                    { new Guid("5f0e3e1f-4a76-4c4e-9d34-bdeff33f76a9"), "Със сериозни дефекти", "Лошо" },
                    { new Guid("73c2f799-3e94-47bc-8c29-1d157f243bbc"), "Използвано, но в добро състояние", "Добро" },
                    { new Guid("862f2c20-cb09-4e6a-b4d2-92d0d4e3d5f6"), "Много леко използвано", "Като ново" },
                    { new Guid("f1a52b5b-4c3d-44d0-80f8-5ad836eeb09c"), "Изцяло ново, неизползвано", "Ново" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "a1f2e3d4-c5b6-47f8-9876-123456789abc", "c3d4e5f6-7890-4abc-def1-234567890abc" },
                    { "b2f3e4d5-a6b7-48c9-8765-abcdef123456", "d4e5f6a7-8901-4bcd-efa2-34567890bcde" }
                });

            migrationBuilder.InsertData(
                table: "SportGears",
                columns: new[] { "Id", "BrandId", "CategoryId", "ConditionId", "Description", "ImageUrl", "IsDeleted", "Name", "OwnerId", "PricePerDay" },
                values: new object[,]
                {
                    { new Guid("1679091c-5a88-4e3e-96a4-7f3b3e7d9d3f"), new Guid("0d9a5b76-48b1-4eea-8c1d-cdff4a56b57a"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Guid("862f2c20-cb09-4e6a-b4d2-92d0d4e3d5f6"), "Дебела постелка за йога с антислип покритие.", "https://example.com/images/yoga-mat-deluxe.jpg", false, "Yoga Mat Deluxe", "c3d4e5f6-7890-4abc-def1-234567890abc", 9.00m },
                    { new Guid("1dcca233-c2a1-4f1e-9d9f-c2147b0ccf8a"), new Guid("0d9a5b76-48b1-4eea-8c1d-cdff4a56b57a"), new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"), new Guid("f1a52b5b-4c3d-44d0-80f8-5ad836eeb09c"), "Очила за ски и сноуборд с високо качество.", "https://example.com/images/salomon-goggles.jpg", false, "Salomon Ski Goggles", "c3d4e5f6-7890-4abc-def1-234567890abc", 10.00m },
                    { new Guid("37b51d19-59a7-4ed4-8996-0b1d0c428a92"), new Guid("3d6f0a88-9d64-4a38-9f2c-52deff0a92d2"), new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"), new Guid("73c2f799-3e94-47bc-8c29-1d157f243bbc"), "Удобни ръкавици за фитнес и тежести.", "https://example.com/images/puma-gloves.jpg", false, "Puma Fitness Gloves", "d4e5f6a7-8901-4bcd-efa2-34567890bcde", 5.00m },
                    { new Guid("73feffa4-7f1b-4e14-90c6-b42b041bf63f"), new Guid("1c2d0d89-62e6-4e3b-8fcd-125c5bb8f2a1"), new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"), new Guid("73c2f799-3e94-47bc-8c29-1d157f243bbc"), "Топли ръкавици за зимни спортове.", "https://example.com/images/columbia-gloves.jpg", false, "Columbia Winter Gloves", "c3d4e5f6-7890-4abc-def1-234567890abc", 7.50m },
                    { new Guid("8e296a06-2b87-4f7d-bb57-1a7b1c5ca6e9"), new Guid("3d6f0a88-9d64-4a38-9f2c-52deff0a92d2"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new Guid("73c2f799-3e94-47bc-8c29-1d157f243bbc"), "Обувки за бягане в планината, устойчиви на кал и вода.", "https://example.com/images/trail-shoes.jpg", false, "Trail Running Shoes", "c3d4e5f6-7890-4abc-def1-234567890abc", 13.50m },
                    { new Guid("8f14e45f-ceea-4bfc-9274-b7987d4a59d9"), new Guid("c9bf9e57-1685-4c89-bafb-ff5af830be8a"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new Guid("f1a52b5b-4c3d-44d0-80f8-5ad836eeb09c"), "Леки маратонки за бягане с въздушна възглавница.", "https://example.com/images/nike-pegasus.jpg", false, "Nike Air Zoom Pegasus", "d4e5f6a7-8901-4bcd-efa2-34567890bcde", 12.50m },
                    { new Guid("a87ff679-a2f3-4f54-8e8f-0fa6d8b7cd55"), new Guid("e358efa4-1e22-4ac1-8f98-cd78e9a6ccf3"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Guid("f1a52b5b-4c3d-44d0-80f8-5ad836eeb09c"), "Удобен постел за фитнес и йога.", "https://example.com/images/adidas-mat.jpg", false, "Adidas Fitness Mat", "c3d4e5f6-7890-4abc-def1-234567890abc", 8.00m },
                    { new Guid("acbd18db-4cc2-43e2-a05d-dcbbd298db96"), new Guid("e358efa4-1e22-4ac1-8f98-cd78e9a6ccf3"), new Guid("9c858901-8a57-4791-81fe-4c455b099bc9"), new Guid("862f2c20-cb09-4e6a-b4d2-92d0d4e3d5f6"), "Водоустойчиво яке за планина.", "https://example.com/images/adidas-terrex.jpg", false, "Adidas Terrex Jacket", "d4e5f6a7-8901-4bcd-efa2-34567890bcde", 15.00m },
                    { new Guid("b6d81b36-1b9e-4f1d-9e0f-5b3a9d3b21d6"), new Guid("c9bf9e57-1685-4c89-bafb-ff5af830be8a"), new Guid("9c858901-8a57-4791-81fe-4c455b099bc9"), new Guid("862f2c20-cb09-4e6a-b4d2-92d0d4e3d5f6"), "Дишащи шорти за бягане и спорт.", "https://example.com/images/nike-shorts.jpg", false, "Nike Running Shorts", "c3d4e5f6-7890-4abc-def1-234567890abc", 6.00m },
                    { new Guid("e4da3b7f-bbce-4a1b-b0f4-35e1d9f2b35a"), new Guid("1c2d0d89-62e6-4e3b-8fcd-125c5bb8f2a1"), new Guid("6fa459ea-ee8a-3ca4-894e-db77e160355e"), new Guid("73c2f799-3e94-47bc-8c29-1d157f243bbc"), "Велосипед с 21 скорости и амортисьори.", "https://example.com/images/bike-x200.jpg", false, "Mountain Bike X200", "c3d4e5f6-7890-4abc-def1-234567890abc", 25.00m }
                });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "IsDeleted", "RentalEndDate", "RentalStartDate", "SportGearId", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"), false, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8f14e45f-ceea-4bfc-9274-b7987d4a59d9"), 50.00m, "d4e5f6a7-8901-4bcd-efa2-34567890bcde" },
                    { new Guid("b2c3d4e5-f6a7-48b9-9c0d-1e2f3a4b5c6d"), false, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("acbd18db-4cc2-43e2-a05d-dcbbd298db96"), 75.50m, "d4e5f6a7-8901-4bcd-efa2-34567890bcde" },
                    { new Guid("c3d4e5f6-a7b8-49c0-ad1e-2f3a4b5c6d7e"), false, new DateTime(2025, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b6d81b36-1b9e-4f1d-9e0f-5b3a9d3b21d6"), 30.00m, "c3d4e5f6-7890-4abc-def1-234567890abc" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "IsDeleted", "Rating", "SportGearId", "UserId" },
                values: new object[,]
                {
                    { new Guid("6d7f2c91-ea12-4c88-9b3a-8f7d5a0e4b9d"), "Good value for money.", false, 4, new Guid("acbd18db-4cc2-43e2-a05d-dcbbd298db96"), "d4e5f6a7-8901-4bcd-efa2-34567890bcde" },
                    { new Guid("c9f1d8b2-3a47-4d89-8e7c-12a9d1e8f3b2"), "Works perfectly for my needs.", false, 5, new Guid("37b51d19-59a7-4ed4-8996-0b1d0c428a92"), "d4e5f6a7-8901-4bcd-efa2-34567890bcde" },
                    { new Guid("d4b6c1a3-7f56-4a2e-9bc7-efa7a1234567"), "Highly recommend this gear!", false, 5, new Guid("b6d81b36-1b9e-4f1d-9e0f-5b3a9d3b21d6"), "c3d4e5f6-7890-4abc-def1-234567890abc" },
                    { new Guid("e3f4d2b1-1234-4c5d-a789-6b7c8d9e0f1a"), "Good quality and fast delivery.", false, 4, new Guid("a87ff679-a2f3-4f54-8e8f-0fa6d8b7cd55"), "c3d4e5f6-7890-4abc-def1-234567890abc" },
                    { new Guid("f2e3a1b4-7f9c-4d26-bf6e-0a4eae9d0bfb"), "Great equipment, very durable!", false, 5, new Guid("8f14e45f-ceea-4bfc-9274-b7987d4a59d9"), "d4e5f6a7-8901-4bcd-efa2-34567890bcde" },
                    { new Guid("f7e8d9c0-4567-4b8d-b12f-34a5b6c7d8e9"), "Perfect for winter sports.", false, 5, new Guid("73feffa4-7f1b-4e14-90c6-b42b041bf63f"), "c3d4e5f6-7890-4abc-def1-234567890abc" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_SportGearId",
                table: "Favorites",
                column: "SportGearId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_SportGearId",
                table: "Rentals",
                column: "SportGearId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_UserId",
                table: "Rentals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SportGearId",
                table: "Reviews",
                column: "SportGearId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SportGears_BrandId",
                table: "SportGears",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_SportGears_CategoryId",
                table: "SportGears",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SportGears_ConditionId",
                table: "SportGears",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_SportGears_OwnerId",
                table: "SportGears",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "SportGears");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "GearConditions");
        }
    }
}
