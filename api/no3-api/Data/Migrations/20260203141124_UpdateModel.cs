using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace no3_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RespnseReason",
                table: "Requests",
                newName: "ResponseReason");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResponseReason",
                table: "Requests",
                newName: "RespnseReason");
        }
    }
}
