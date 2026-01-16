using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_ProductionDownTime_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductionDownTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DocumentId = table.Column<int>(type: "integer", nullable: false),
                    ResponsibleId = table.Column<int>(type: "integer", nullable: false),
                    ReasonGroupId = table.Column<int>(type: "integer", nullable: false),
                    ReasonId = table.Column<int>(type: "integer", nullable: false),
                    ActionTake = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionDownTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionDownTime_CatalogValue_ReasonGroupId",
                        column: x => x.ReasonGroupId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionDownTime_CatalogValue_ReasonId",
                        column: x => x.ReasonId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionDownTime_CatalogValue_ResponsibleId",
                        column: x => x.ResponsibleId,
                        principalTable: "CatalogValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionDownTime_ProductionDocuments_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "ProductionDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionDownTime_DocumentId",
                table: "ProductionDownTime",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionDownTime_ReasonGroupId",
                table: "ProductionDownTime",
                column: "ReasonGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionDownTime_ReasonId",
                table: "ProductionDownTime",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionDownTime_ResponsibleId",
                table: "ProductionDownTime",
                column: "ResponsibleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductionDownTime");
        }
    }
}
