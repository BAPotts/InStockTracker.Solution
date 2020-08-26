using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InStockTracker.Migrations
{
    public partial class ImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImgTitle",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Img = table.Column<byte[]>(nullable: true),
                    ImgTitle = table.Column<string>(nullable: true),
                    ParentProductProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Products_ParentProductProductId",
                        column: x => x.ParentProductProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ParentProductProductId",
                table: "Images",
                column: "ParentProductProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.AddColumn<byte[]>(
                name: "Img",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgTitle",
                table: "Products",
                nullable: true);
        }
    }
}
