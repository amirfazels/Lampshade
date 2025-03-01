using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class FixName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictrueTitle",
                table: "Articles",
                newName: "PictureTitle");

            migrationBuilder.RenameColumn(
                name: "PictrueAlt",
                table: "Articles",
                newName: "PictureAlt");

            migrationBuilder.RenameColumn(
                name: "Pictrue",
                table: "Articles",
                newName: "Picture");

            migrationBuilder.RenameColumn(
                name: "MetaDiscription",
                table: "ArticleCategories",
                newName: "MetaDescription");

            migrationBuilder.RenameColumn(
                name: "Keyword",
                table: "ArticleCategories",
                newName: "Keywords");

            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "ArticleCategories",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureTitle",
                table: "Articles",
                newName: "PictrueTitle");

            migrationBuilder.RenameColumn(
                name: "PictureAlt",
                table: "Articles",
                newName: "PictrueAlt");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Articles",
                newName: "Pictrue");

            migrationBuilder.RenameColumn(
                name: "MetaDescription",
                table: "ArticleCategories",
                newName: "MetaDiscription");

            migrationBuilder.RenameColumn(
                name: "Keywords",
                table: "ArticleCategories",
                newName: "Keyword");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ArticleCategories",
                newName: "Discription");
        }
    }
}
