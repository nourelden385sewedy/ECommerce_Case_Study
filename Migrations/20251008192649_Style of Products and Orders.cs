using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce_Case_Study.Migrations
{
    /// <inheritdoc />
    public partial class StyleofProductsandOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    DateOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.OrdersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Devices and gadgets", "Electronics" },
                    { 2, "Reading and education", "Books" },
                    { 3, "Apparel and accessories", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "ConfirmPassword", "Contact", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "Password123!", "555-1111", "alice@example.com", "Alice Johnson", "Password123!" },
                    { 2, "Password123!", "555-2222", "bob@example.com", "Bob Smith", "Password123!" },
                    { 3, "Password123!", "555-3333", "charlie@example.com", "Charlie Brown", "Password123!" },
                    { 4, "Password123!", "555-4444", "diana@example.com", "Diana Prince", "Password123!" }
                });

            migrationBuilder.InsertData(
                table: "CustomerProfiles",
                columns: new[] { "Id", "Address", "CustomerId", "DateOfBirth" },
                values: new object[,]
                {
                    { 1, "123 Main St", 1, new DateTime(1990, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "456 Elm St", 2, new DateTime(1985, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "789 Oak St", 3, new DateTime(1992, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "DateOfOrder", "OrderId", "TotalPrice" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 9, 30, 10, 0, 0, 0, DateTimeKind.Unspecified), 1001, 948m },
                    { 2, 2, new DateTime(2025, 10, 2, 14, 30, 0, 0, DateTimeKind.Unspecified), 1002, 59m },
                    { 3, 3, new DateTime(2025, 10, 6, 9, 15, 0, 0, DateTimeKind.Unspecified), 1003, 198m },
                    { 4, 1, new DateTime(2025, 10, 4, 16, 45, 0, 0, DateTimeKind.Unspecified), 1004, 1299m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, "Latest smartphone", "Smartphone", 899m, 50 },
                    { 2, 1, "High performance laptop", "Laptop", 1299m, 30 },
                    { 3, 1, "Noise cancelling", "Headphones", 199m, 80 },
                    { 4, 2, "Programming guide", "C# in Depth", 49m, 100 },
                    { 5, 2, "Best coding practices", "Clean Code", 59m, 75 },
                    { 6, 2, "Software architecture", "Design Patterns", 79m, 60 },
                    { 7, 3, "Cotton T-shirt", "T-Shirt", 19m, 200 },
                    { 8, 3, "Blue denim jeans", "Jeans", 49m, 150 },
                    { 9, 3, "Leather jacket", "Jacket", 149m, 40 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CustomerId",
                table: "CustomerProfiles",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductsId",
                table: "OrderProducts",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerProfiles");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
