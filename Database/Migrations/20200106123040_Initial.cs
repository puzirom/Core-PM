using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    SourceSystemId = table.Column<int>(nullable: false),
                    RecNo = table.Column<int>(nullable: false),
                    OrgNoClient = table.Column<string>(maxLength: 20, nullable: true),
                    Kid = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                "IX_Documents_RecNo_SourceSystemId_Type",
                "Documents",
                new[] { "RecNo", "SourceSystemId", "Type" });

            migrationBuilder.CreateIndex(
                "IX_Documents_Kid_SourceSystemId",
                "Documents",
                new[] { "Kid", "SourceSystemId" });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 100, nullable: false),
                    DocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                    table.ForeignKey(
                        name: "FK_References_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_References_Value_Type",
                "References",
                new[] { "Value", "Type" });

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Undefined" },
                    { 1, "Invoice" },
                    { 2, "Reminder" }
                });

            migrationBuilder.InsertData(
                table: "ReferenceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "InvoiceNumber" },
                    { 2, "ReminderNumber" },
                    { 3, "CreditInvoiceNumber" },
                    { 4, "OrderNumber" },
                    { 5, "CustomerNumber" },
                    { 6, "InvoiceRecNo" },
                    { 7, "ConsolidatedInvoiceRecNo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_References_DocumentId",
                table: "References",
                column: "DocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "ReferenceTypes");

            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
