using System;
using FluentMigrator;

namespace OTUS.work_with_database.Migrations
{
    [Migration(202106291211)]
    public class AddAdvertisementTable : Migration {
        public override void Up()
        {
            Create.Table("advertisements")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("name").AsFixedLengthString(50).NotNullable()
                .WithColumn("description").AsString().NotNullable()
                .WithColumn("price").AsDouble().NotNullable()
                .WithColumn("category_id").AsInt64().NotNullable()
                .WithColumn("user_id").AsInt64().NotNullable();

            Create.ForeignKey("fk_advertisements_users_user_id")
                .FromTable("advertisements").ForeignColumn("user_id")
                .ToTable("users").PrimaryColumn("id");

            Create.ForeignKey("fk_advertisements_categories_categoryId")
                .FromTable("advertisements").ForeignColumn("category_id")
                .ToTable("categories").PrimaryColumn("id");
        }

        public override void Down()
        {
            Delete.Table("advertisements");
        }
    }
}