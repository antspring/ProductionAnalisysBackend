using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_HourlyByPowerView_view : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                 CREATE OR REPLACE VIEW "HourlyByPowerView" AS
                                 SELECT hbt."Id",
                                 
                                        hbt."NameOfProductId",
                                        hbt."DepartmentId",
                                        hbt."PerformerId",
                                        hbt."ShiftId",
                                 
                                        hbt."WorkHourId",
                                        wh."Value"                              AS "WorkHour",
                                 
                                        hbt."Power",
                                        hbt."DailyRate",
                                        hbt."Fact",
                                        hbt."Date",
                                        hbt."ProductionDocumentId",
                                 
                                        -- план за час
                                        (hbt."DailyRate" / 8.0)::NUMERIC(10, 2) AS "Plan",
                                 
                                        -- план накопительный
                                        SUM(hbt."DailyRate" / 8.0) OVER (
                                            PARTITION BY hbt."NameOfProductId", hbt."DepartmentId", hbt."ShiftId", hbt."Date"
                                            ORDER BY wh."Id"
                                            )                                   AS "PlanCumulative",
                                 
                                        -- факт накопительный
                                        SUM(hbt."Fact") OVER (
                                            PARTITION BY hbt."NameOfProductId", hbt."DepartmentId", hbt."ShiftId", hbt."Date"
                                            ORDER BY wh."Id"
                                            )                                   AS "FactCumulative",
                                 
                                        -- отклонение
                                        hbt."Fact" - (hbt."DailyRate" / 8.0)    AS "Deviation",
                                 
                                        -- отклонение накопительное
                                        SUM(hbt."Fact" - (hbt."DailyRate" / 8.0)) OVER (
                                            PARTITION BY hbt."NameOfProductId", hbt."DepartmentId", hbt."ShiftId", hbt."Date"
                                            ORDER BY wh."Id"
                                            )                                   AS "DeviationCumulative",
                                 
                                        -- итоги за смену
                                        SUM(hbt."Fact") OVER (
                                            PARTITION BY hbt."NameOfProductId", hbt."DepartmentId", hbt."ShiftId", hbt."Date"
                                            )                                   AS "TotalFact",
                                 
                                        SUM(hbt."DailyRate" / 8.0) OVER (
                                            PARTITION BY hbt."NameOfProductId", hbt."DepartmentId", hbt."ShiftId", hbt."Date"
                                            )                                   AS "TotalPlan"
                                 
                                 FROM "HourlyByPower" hbt
                                          JOIN "CatalogValue" wh ON wh."Id" = hbt."WorkHourId";
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                 DROP VIEW IF EXISTS "HourlyByPowerView";
                                 """);
        }
    }
}
