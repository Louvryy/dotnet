using FluentMigrator;

namespace Louvryy.Core.Migrations.Scripts;

[Migration(20240115140223)]
public class AddAssetTable : Migration
{
    public const string TableName = "Assets";

    public override void Up()
    {
        Create.Table(TableName)
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Title").AsString(100).Nullable()
            .WithColumn("Caption").AsString(1024).Nullable()
            .WithColumn("Alt").AsString(1024).Nullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("UpdatedAt").AsDateTime().Nullable();
    }

    public override void Down()
    {
        Delete.Table(TableName);
    }
}
