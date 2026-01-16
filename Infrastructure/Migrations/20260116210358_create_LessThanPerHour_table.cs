using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_LessThanPerHour_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessThanPerHour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    PerformerId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: false),
                    WorkHourId = table.Column<int>(type: "integer", nullable: false),
                    OperationNameId = table.Column<int>(type: "integer", nullable: false),
                    StartTimePlan = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    StartTimeFact = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTimePlan = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTimeFact = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Plan = table.Column<double>(type: "double precision", nullable: false),
                    Fact = table.Column<double>(type: "double precision", nullable: false),
                    ProductionDocumentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessThanPerHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessThanPerHour_CatalogValue_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessThanPerHour_CatalogValue_OperationNameId",
                        column: x => x.OperationNameId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessThanPerHour_CatalogValue_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessThanPerHour_CatalogValue_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessThanPerHour_CatalogValue_WorkHourId",
                        column: x => x.WorkHourId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessThanPerHour_ProductionDocuments_ProductionDocumentId",
                        column: x => x.ProductionDocumentId,
                        principalTable: "ProductionDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessThanPerHour_DepartmentId_PerformerId_ShiftId_WorkHourId~",
                table: "LessThanPerHour",
                columns: new[] { "DepartmentId", "PerformerId", "ShiftId", "WorkHourId", "OperationNameId", "Date", "StartTimePlan", "EndTimePlan" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessThanPerHour_OperationNameId",
                table: "LessThanPerHour",
                column: "OperationNameId");

            migrationBuilder.CreateIndex(
                name: "IX_LessThanPerHour_PerformerId",
                table: "LessThanPerHour",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_LessThanPerHour_ProductionDocumentId",
                table: "LessThanPerHour",
                column: "ProductionDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessThanPerHour_ShiftId",
                table: "LessThanPerHour",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_LessThanPerHour_WorkHourId",
                table: "LessThanPerHour",
                column: "WorkHourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessThanPerHour");
        }
    }
}
