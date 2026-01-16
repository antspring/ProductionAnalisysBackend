using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_LessThanPerHourView_view : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                 CREATE OR REPLACE VIEW "LessThanPerHourView" AS
                                 SELECT hbt."Id",
                                 
                                        hbt."DepartmentId",
                                        hbt."PerformerId",
                                        hbt."ShiftId",
                                 
                                        hbt."WorkHourId",
                                        wh."Value"              AS "WorkHour",
                                 
                                        hbt."OperationNameId",
                                        hbt."Date",
                                        hbt."Fact",
                                        hbt."Plan",
                                 
                                        hbt."StartTimePlan",
                                        hbt."StartTimeFact",
                                        hbt."EndTimePlan",
                                        hbt."EndTimeFact",
                                 
                                        hbt."ProductionDocumentId",
                                 
                                        -- план накопительный
                                        SUM(hbt."Plan") OVER (
                                            PARTITION BY hbt."OperationNameId", hbt."DepartmentId", hbt."ShiftId", hbt."Date"
                                            ORDER BY wh."Id"
                                            )                   AS "PlanCumulative",
                                 
                                        -- факт накопительный
                                        SUM(hbt."Fact") OVER (
                                            PARTITION BY hbt."OperationNameId", hbt."DepartmentId", hbt."ShiftId", hbt."Date"
                                            ORDER BY wh."Id"
                                            )                   AS "FactCumulative",
                                 
                                        -- отклонение
                                        hbt."Fact" - hbt."Plan" AS "Deviation",
                                 
                                        -- отклонение накопительное
                                        SUM(hbt."Fact" - hbt."Plan") OVER (
                                            PARTITION BY hbt."OperationNameId", hbt."DepartmentId", hbt."ShiftId", hbt."Date"
                                            ORDER BY wh."Id"
                                            )                   AS "DeviationCumulative"
                                 
                                 FROM "LessThanPerHour" hbt
                                          JOIN "CatalogValue" wh ON wh."Id" = hbt."WorkHourId";
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                 DROP VIEW IF EXISTS "LessThanPerHourView";
                                 """);
        }
    }
}
