using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteManagement.Infrastructure.Persistence.Migrations
{
    public partial class CrossTableCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterInformations_Users_UserRegisterId",
                table: "RegisterInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisterTransactions_RegisterInformations_AccountCode",
                table: "RegisterTransactions");

            migrationBuilder.DropIndex(
                name: "IX_RegisterTransactions_AccountCode",
                table: "RegisterTransactions");

            migrationBuilder.DropIndex(
                name: "IX_RegisterInformations_UserRegisterId",
                table: "RegisterInformations");

            migrationBuilder.DropColumn(
                name: "UserRegisterId",
                table: "RegisterInformations");

            migrationBuilder.AddColumn<int>(
                name: "RegisterInformationId",
                table: "RegisterTransactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserRegister",
                columns: table => new
                {
                    RegisterInformationsId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegister", x => new { x.RegisterInformationsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserRegister_RegisterInformations_RegisterInformationsId",
                        column: x => x.RegisterInformationsId,
                        principalTable: "RegisterInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRegister_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisterTransactions_RegisterInformationId",
                table: "RegisterTransactions",
                column: "RegisterInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRegister_UsersId",
                table: "UserRegister",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTransactions_RegisterInformations_RegisterInformationId",
                table: "RegisterTransactions",
                column: "RegisterInformationId",
                principalTable: "RegisterInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterTransactions_RegisterInformations_RegisterInformationId",
                table: "RegisterTransactions");

            migrationBuilder.DropTable(
                name: "UserRegister");

            migrationBuilder.DropIndex(
                name: "IX_RegisterTransactions_RegisterInformationId",
                table: "RegisterTransactions");

            migrationBuilder.DropColumn(
                name: "RegisterInformationId",
                table: "RegisterTransactions");

            migrationBuilder.AddColumn<int>(
                name: "UserRegisterId",
                table: "RegisterInformations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RegisterTransactions_AccountCode",
                table: "RegisterTransactions",
                column: "AccountCode");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterInformations_UserRegisterId",
                table: "RegisterInformations",
                column: "UserRegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterInformations_Users_UserRegisterId",
                table: "RegisterInformations",
                column: "UserRegisterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTransactions_RegisterInformations_AccountCode",
                table: "RegisterTransactions",
                column: "AccountCode",
                principalTable: "RegisterInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
