﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHub.EF.Migrations
{
    /// <inheritdoc />
    public partial class returnDiscriminatorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "Discriminator",
               table: "AspNetUsers",
               type: "nvarchar(13)",
               maxLength: 13,
               nullable: false,
               defaultValue: "");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                         name: "Discriminator",
                         table: "AspNetUsers");

        }
    }
}
