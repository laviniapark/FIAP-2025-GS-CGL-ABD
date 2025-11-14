using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AiLogs",
                table: "AiLogs");

            migrationBuilder.RenameTable(
                name: "AiLogs",
                newName: "TBL_AI_LOGS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBL_AI_LOGS",
                table: "TBL_AI_LOGS",
                column: "log_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TBL_AI_LOGS",
                table: "TBL_AI_LOGS");

            migrationBuilder.RenameTable(
                name: "TBL_AI_LOGS",
                newName: "AiLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AiLogs",
                table: "AiLogs",
                column: "log_id");
        }
    }
}
