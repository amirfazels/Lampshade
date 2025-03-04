﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommentManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Comments",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
            //        Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
            //        IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        IsCanceled = table.Column<bool>(type: "bit", nullable: false),
            //        OwnerRecordId = table.Column<long>(type: "bigint", nullable: false),
            //        Type = table.Column<int>(type: "int", nullable: false),
            //        ParentId = table.Column<long>(type: "bigint", nullable: false),
            //        CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Comments", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Comments_Comments_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "Comments",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comments_ParentId",
            //    table: "Comments",
            //    column: "ParentId");
            migrationBuilder.AddColumn<string>(
        name: "Website",
        table: "Comments",
        type: "nvarchar(max)",
        nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerRecordId",
                table: "Comments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Comments",
                type: "bigint",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments",
                column: "ParentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Comments");
            migrationBuilder.DropForeignKey(
        name: "FK_Comments_Comments_ParentId",
        table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ParentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "OwnerRecordId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Comments");
        }
    }
}
