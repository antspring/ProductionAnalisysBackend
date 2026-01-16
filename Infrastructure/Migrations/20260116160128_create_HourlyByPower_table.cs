using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_HourlyByPower_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HourlyByPower",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameOfProductId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    PerformerId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: false),
                    Power = table.Column<double>(type: "double precision", nullable: false),
                    DailyRate = table.Column<double>(type: "double precision", nullable: false),
                    WorkHourId = table.Column<int>(type: "integer", nullable: false),
                    Fact = table.Column<double>(type: "double precision", nullable: false),
                    ProductionDocumentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourlyByPower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HourlyByPower_CatalogValue_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlyByPower_CatalogValue_NameOfProductId",
                        column: x => x.NameOfProductId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlyByPower_CatalogValue_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlyByPower_CatalogValue_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlyByPower_CatalogValue_WorkHourId",
                        column: x => x.WorkHourId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlyByPower_ProductionDocuments_ProductionDocumentId",
                        column: x => x.ProductionDocumentId,
                        principalTable: "ProductionDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByPower_DepartmentId",
                table: "HourlyByPower",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByPower_NameOfProductId_DepartmentId_PerformerId_Shif~",
                table: "HourlyByPower",
                columns: new[] { "NameOfProductId", "DepartmentId", "PerformerId", "ShiftId", "WorkHourId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByPower_PerformerId",
                table: "HourlyByPower",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByPower_ProductionDocumentId",
                table: "HourlyByPower",
                column: "ProductionDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByPower_ShiftId",
                table: "HourlyByPower",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByPower_WorkHourId",
                table: "HourlyByPower",
                column: "WorkHourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HourlyByPower");
        }
    }
}
