using FluentMigrator;

namespace Louvryy.Core.Migrations.Scripts;

[Migration(20240115174339)]
public class AddAssetVersionTable : Migration
{
    public const string TableName = "AssetVersions";

    public override void Up()
    {
        Create.Table(TableName)
            .WithColumn("OriginalAssetId")
                .AsGuid()
                .PrimaryKey()
                .ForeignKey(AddAssetTable.TableName, "Id")
            .WithColumn("VersionAssetId")
                .AsGuid()
                .PrimaryKey()
                .ForeignKey(AddAssetTable.TableName, "Id")
            .WithColumn("Type")
                .AsString(50)
                .NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable();


    }

    public override void Down()
    {
        Delete.Table(TableName);
    }
}
