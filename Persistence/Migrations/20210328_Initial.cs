using FluentMigrator;

namespace Persistence.Migrations
{
    [Migration(20210328)]
    public class Initial : Migration
    {
        public override void Up()
        {
            Create.Table("Category")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("Country")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("Product")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("SubCategoryId").AsInt64().ForeignKey().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("Brand").AsString().NotNullable()
                .WithColumn("ImagePath").AsString().NotNullable()
                .WithColumn("Origin").AsString().NotNullable()
                .WithColumn("Stock").AsInt64().NotNullable()
                .WithColumn("Price").AsFloat().NotNullable()
                .WithColumn("Created").AsDate().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("LastModified").AsDate().Nullable()
                .WithColumn("LastModifiedBy").AsString().NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable();

            Create.Table("SubCategory")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("CategoryId").AsInt64().NotNullable()
                .WithColumn("Name").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Category");
            Delete.Table("Country");
            Delete.Table("Product");
            Delete.Table("SubCategory");
        }
    }
}