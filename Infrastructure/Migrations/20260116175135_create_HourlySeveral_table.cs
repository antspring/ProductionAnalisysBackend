using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_HourlySeveral_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HourlySeveral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Product1Id = table.Column<int>(type: "integer", nullable: false),
                    Product2Id = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    PerformerId = table.Column<int>(type: "integer", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: false),
                    WorkHourId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CycleTime1 = table.Column<double>(type: "double precision", nullable: false),
                    CycleTime2 = table.Column<double>(type: "double precision", nullable: false),
                    DailyRate1 = table.Column<double>(type: "double precision", nullable: false),
                    DailyRate2 = table.Column<double>(type: "double precision", nullable: false),
                    Fact = table.Column<double>(type: "double precision", nullable: false),
                    Changeover = table.Column<double>(type: "double precision", nullable: false),
                    ProductionDocumentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourlySeveral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HourlySeveral_CatalogValue_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlySeveral_CatalogValue_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlySeveral_CatalogValue_Product1Id",
                        column: x => x.Product1Id,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlySeveral_CatalogValue_Product2Id",
                        column: x => x.Product2Id,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlySeveral_CatalogValue_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlySeveral_CatalogValue_WorkHourId",
                        column: x => x.WorkHourId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourlySeveral_ProductionDocuments_ProductionDocumentId",
                        column: x => x.ProductionDocumentId,
                        principalTable: "ProductionDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HourlySeveral_DepartmentId_PerformerId_ShiftId_WorkHourId_D~",
                table: "HourlySeveral",
                columns: new[] { "DepartmentId", "PerformerId", "ShiftId", "WorkHourId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HourlySeveral_PerformerId",
                table: "HourlySeveral",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlySeveral_Product1Id",
                table: "HourlySeveral",
                column: "Product1Id");

            migrationBuilder.CreateIndex(
                name: "IX_HourlySeveral_Product2Id",
                table: "HourlySeveral",
                column: "Product2Id");

            migrationBuilder.CreateIndex(
                name: "IX_HourlySeveral_ProductionDocumentId",
                table: "HourlySeveral",
                column: "ProductionDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlySeveral_ShiftId",
                table: "HourlySeveral",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlySeveral_WorkHourId",
                table: "HourlySeveral",
                column: "WorkHourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HourlySeveral");
        }
    }
}
