﻿using System;
using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore.Migrations;
using RAZORWEB.Model;

#nullable disable

namespace RAZORWEB.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.Id);
                });

            // Sau khi cài bảng xong thì chèn dữ liệu mẫu vào
            // Insert Data
            // migrationBuilder.InsertData(
            //     table: "articles",
            //     columns: new[] {"Title", "Created", "Content"},
            //     values: new object[] {
            //         "Bài viết 1",
            //         new DateTime(2023, 10, 17),
            //         "Nội dung 1"
            //     }
            // );

            Randomizer.Seed = new Random(8675309);
            var fakerArticle = new Faker<Article>();
            fakerArticle.RuleFor(a => a.Title, f => f.Lorem.Sentence(5, 5));
            fakerArticle.RuleFor(a => a.Created, f => f.Date.Between(new DateTime(2023, 2, 17), new DateTime(2024, 9, 17)));
            fakerArticle.RuleFor(a => a.Content, f => f.Lorem.Paragraphs(1,5));

            for(int i=0; i<50; i++) {
                Article article = fakerArticle.Generate();
                migrationBuilder.InsertData(
                    table: "articles",
                    columns: new[] {"Title", "Created", "Content"},
                    values: new object[] {
                        article.Title,
                        article.Created,
                        article.Content,
                    }
                );
            }

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");
        }
    }
}
