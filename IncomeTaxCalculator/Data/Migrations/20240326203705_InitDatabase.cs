using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations;

public partial class InitDatabase : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TaxBands",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LowerLimit = table.Column<int>(type: "int", nullable: false),
                UpperLimit = table.Column<int>(type: "int", nullable: true),
                TaxRate = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TaxBands", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "TaxBands",
            columns: new[] { "Id", "LowerLimit", "Name", "TaxRate", "UpperLimit" },
            values: new object[] { new Guid("7b3e6b6a-9a58-4b41-a815-3828a9f23f28"), 0, "Tax Band A", 0, 5000 });

        migrationBuilder.InsertData(
            table: "TaxBands",
            columns: new[] { "Id", "LowerLimit", "Name", "TaxRate", "UpperLimit" },
            values: new object[] { new Guid("e7751ebc-1b20-48e1-9d67-d9fc58d06c4d"), 5000, "Tax Band B", 20, 20000 });

        migrationBuilder.InsertData(
            table: "TaxBands",
            columns: new[] { "Id", "LowerLimit", "Name", "TaxRate", "UpperLimit" },
            values: new object[] { new Guid("f824c88c-6ff7-4dc5-b376-32c0c3fb2ba8"), 20000, "Tax Band C", 40, null });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TaxBands");
    }
}
