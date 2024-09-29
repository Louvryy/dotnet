using Microsoft.EntityFrameworkCore;
using Louvryy.Core.Migrations;

namespace Louvryy.Core.Tests.Fixtures.DbContextFixture;

public class DbContextFixture : IDisposable
{
    public readonly TestDbContext DbContext;

    public DbContextFixture(TestDbContext testDbContext)
    {
        DbContext = testDbContext;
    }

    public async Task TruncateTable<TEntity>() where TEntity : class
    {
        var tableName = DbContext.Set<TEntity>().EntityType.GetTableName();

        await DbContext.Database.ExecuteSqlRawAsync($"DELETE FROM {tableName}", tableName);
        await DbContext.SaveChangesAsync();
    }

    public async Task RefreshDatabaseAsync()
    {
        var connection = DbContext.Database.GetDbConnection();

        // drop database if exists
        await DbContext.Database.EnsureDeletedAsync();
        // create database if not exists and creates tables based on models
        await DbContext.Database.EnsureCreatedAsync();

        await dropAllTables(DbContext);

        // run migrations
        await Task.Run(() =>
        {
            Migrator.RunUpdate(connection.ConnectionString);
        });
    }

    private async Task dropAllTables(DbContext dbContext)
    {
        await DbContext.Database.ExecuteSqlRawAsync(@"
                    DECLARE @DropConstraints NVARCHAR(max) = '';
                    SELECT @DropConstraints += 'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.'
                                            +  QUOTENAME(OBJECT_NAME(parent_object_id)) + ' ' + 'DROP CONSTRAINT' + QUOTENAME(name) + ';'
                    FROM sys.foreign_keys;
                    EXECUTE sp_executesql @DropConstraints;

                    DECLARE @DropTables NVARCHAR(max) = '';
                    SELECT @DropTables += 'DROP TABLE ' + QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) + ';'
                    FROM INFORMATION_SCHEMA.TABLES;
                    EXECUTE sp_executesql @DropTables;");
    }


    public void Dispose()
    {
    }
}