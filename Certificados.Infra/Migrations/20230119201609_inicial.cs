using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Certificados.Infra.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Email", "Nome", "Salt", "Senha" },
                values: new object[] { 1, "super@super.com", "super", "754ab257a6ca7b2b29236cf2f899dffd8719efa9a4b81eb3e0fe2f2fe079512dbc2fa979e8866ea9a340ee2c3c971ee51b45b751846406a28741f4b5110523b6", "639f80627cbccf696bc1c0f959c82c4f6bc5b441ad047f8c4450b6346c20cf1f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
