using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerRequestApp.Server.Core.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyPairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    СurrencyFrom = table.Column<string>(type: "TEXT", nullable: true),
                    СurrencyTo = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyPairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<double>(type: "REAL", nullable: true),
                    CurrencyPairId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_CurrencyPairs_CurrencyPairId",
                        column: x => x.CurrencyPairId,
                        principalTable: "CurrencyPairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Details = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OpenDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CloseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    NeedSum = table.Column<double>(type: "REAL", nullable: true),
                    CurrentSum = table.Column<double>(type: "REAL", nullable: true),
                    TargetMilitary = table.Column<string>(type: "TEXT", nullable: true),
                    IsFavourite = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestId = table.Column<int>(type: "INTEGER", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Sum = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsMain = table.Column<bool>(type: "INTEGER", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestTag",
                columns: table => new
                {
                    RequestsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsTitle = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTag", x => new { x.RequestsId, x.TagsTitle });
                    table.ForeignKey(
                        name: "FK_RequestTag_Requests_RequestsId",
                        column: x => x.RequestsId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestTag_Tags_TagsTitle",
                        column: x => x.TagsTitle,
                        principalTable: "Tags",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CurrencyPairs",
                columns: new[] { "Id", "IsActive", "СurrencyFrom", "СurrencyTo" },
                values: new object[] { 1, true, "UAH", "USD" });

            migrationBuilder.InsertData(
                table: "CurrencyPairs",
                columns: new[] { "Id", "IsActive", "СurrencyFrom", "СurrencyTo" },
                values: new object[] { 2, true, "UAH", "EUR" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "Чорновик" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "Активний" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Title" },
                values: new object[] { 3, "Завершений" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Title" },
                values: new object[] { 4, "Відмінений" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Title" },
                values: new object[] { 5, "Архівний" });

            migrationBuilder.InsertData(
                table: "ExchangeRates",
                columns: new[] { "Id", "CreatedOn", "CurrencyPairId", "Value" },
                values: new object[] { 1, new DateTime(2022, 11, 18, 10, 34, 46, 333, DateTimeKind.Utc).AddTicks(8722), 1, 0.027076188000000001 });

            migrationBuilder.InsertData(
                table: "ExchangeRates",
                columns: new[] { "Id", "CreatedOn", "CurrencyPairId", "Value" },
                values: new object[] { 2, new DateTime(2022, 11, 18, 10, 34, 46, 333, DateTimeKind.Utc).AddTicks(8725), 2, 0.026223587 });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_RequestId",
                table: "Donations",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyPairId",
                table: "ExchangeRates",
                column: "CurrencyPairId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_RequestId",
                table: "Photos",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_StatusId",
                table: "Requests",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTag_TagsTitle",
                table: "RequestTag",
                column: "TagsTitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "RequestTag");

            migrationBuilder.DropTable(
                name: "CurrencyPairs");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
