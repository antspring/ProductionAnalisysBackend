using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_HourlyByTactTime_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HourlyByTactTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameOfProductId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    PerformerId = table.Column<int>(type: "integer", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: false),
                    WorkHourId = table.Column<int>(type: "integer", nullable: false),
                    TactTime = table.Column<double>(type: "double precision", nullable: false),
                    DailyRate = table.Column<double>(type: "double precision", nullable: false),
                    Fact = table.Column<double>(type: "double precision", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ProductionDocumentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourlyByTactTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HourlyByTactTime_CatalogValue_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlyByTactTime_CatalogValue_NameOfProductId",
                        column: x => x.NameOfProductId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlyByTactTime_CatalogValue_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlyByTactTime_CatalogValue_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlyByTactTime_CatalogValue_WorkHourId",
                        column: x => x.WorkHourId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlyByTactTime_ProductionDocuments_ProductionDocumentId",
                        column: x => x.ProductionDocumentId,
                        principalTable: "ProductionDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByTactTime_DepartmentId",
                table: "HourlyByTactTime",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByTactTime_NameOfProductId_DepartmentId_PerformerId_S~",
                table: "HourlyByTactTime",
                columns: new[] { "NameOfProductId", "DepartmentId", "PerformerId", "ShiftId", "WorkHourId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByTactTime_PerformerId",
                table: "HourlyByTactTime",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByTactTime_ProductionDocumentId",
                table: "HourlyByTactTime",
                column: "ProductionDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByTactTime_ShiftId",
                table: "HourlyByTactTime",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyByTactTime_WorkHourId",
                table: "HourlyByTactTime",
                column: "WorkHourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HourlyByTactTime");
        }
    }
}
