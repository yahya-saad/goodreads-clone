using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goodreads.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddShelvesTablev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookShelf_Books_BookId",
                table: "BookShelf");

            migrationBuilder.DropForeignKey(
                name: "FK_BookShelf_Shelf_ShelfId",
                table: "BookShelf");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shelf",
                table: "Shelf");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookShelf",
                table: "BookShelf");

            migrationBuilder.RenameTable(
                name: "Shelf",
                newName: "Shelves");

            migrationBuilder.RenameTable(
                name: "BookShelf",
                newName: "BookShelves");

            migrationBuilder.RenameIndex(
                name: "IX_BookShelf_ShelfId",
                table: "BookShelves",
                newName: "IX_BookShelves_ShelfId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shelves",
                table: "Shelves",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookShelves",
                table: "BookShelves",
                columns: new[] { "BookId", "ShelfId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookShelves_Books_BookId",
                table: "BookShelves",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookShelves_Shelves_ShelfId",
                table: "BookShelves",
                column: "ShelfId",
                principalTable: "Shelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookShelves_Books_BookId",
                table: "BookShelves");

            migrationBuilder.DropForeignKey(
                name: "FK_BookShelves_Shelves_ShelfId",
                table: "BookShelves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shelves",
                table: "Shelves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookShelves",
                table: "BookShelves");

            migrationBuilder.RenameTable(
                name: "Shelves",
                newName: "Shelf");

            migrationBuilder.RenameTable(
                name: "BookShelves",
                newName: "BookShelf");

            migrationBuilder.RenameIndex(
                name: "IX_BookShelves_ShelfId",
                table: "BookShelf",
                newName: "IX_BookShelf_ShelfId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shelf",
                table: "Shelf",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookShelf",
                table: "BookShelf",
                columns: new[] { "BookId", "ShelfId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookShelf_Books_BookId",
                table: "BookShelf",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookShelf_Shelf_ShelfId",
                table: "BookShelf",
                column: "ShelfId",
                principalTable: "Shelf",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
