using FluentMigrator;

namespace CostReportMaker.Database.Migrations
{
    [Migration(20170502215000)]
    public class AddUserTable : Migration
    {
        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("Username").AsString(50).Unique().NotNullable()
                .WithColumn("Password").AsString(50).NotNullable()
                .WithColumn("Email").AsString(50).Nullable()
                .WithColumn("CreatedAt").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("UpdatedAt").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("User");
        }
    }
}