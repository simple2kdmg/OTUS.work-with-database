using System;
using FluentMigrator;

namespace OTUS.work_with_database.Migrations
{
    [Migration(202106291208)]
    public class AddCategoryTable : Migration {
        public override void Up()
        {
            Create.Table("categories")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("name").AsFixedLengthString(30).NotNullable().Unique("ix_categories_name")
                .WithColumn("description").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("categories");
        }
    }
}