using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItemCustomerData.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerMasters",
                columns: table => new
                {
                    Customer_FName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Customer_LName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Phone = table.Column<int>(type: "int", nullable: false),
                    Customer_Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMasters", x => x.Customer_FName);
                });

            migrationBuilder.CreateTable(
                name: "ItemMasters",
                columns: table => new
                {
                    Item_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Item_Rate = table.Column<int>(type: "int", nullable: false),
                    Item_Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMasters", x => x.Item_Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerMasters");

            migrationBuilder.DropTable(
                name: "ItemMasters");
        }
    }
}
