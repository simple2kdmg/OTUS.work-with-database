using System;
using FluentMigrator;

namespace OTUS.work_with_database.Migrations
{
    [Migration(202106291205)]
    public class AddUserTable : Migration {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("name").AsFixedLengthString(30).NotNullable()
                .WithColumn("surname").AsFixedLengthString(30).NotNullable()
                .WithColumn("age").AsInt16().Nullable()
                .WithColumn("email").AsFixedLengthString(60).Unique("ix_users_email");
        }

        public override void Down()
        {
            Delete.Table("users");
        }
    }
}