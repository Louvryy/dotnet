using Bogus;
using Louvryy.Core.Data.Models;

namespace Louvryy.Core.Tests.Fixtures.EntityBuilders;

public class AssetBuilder
{
    private readonly Faker _faker;
    private bool _materialize = false;
    private DbContextFixture.DbContextFixture? _dbContextFixture;
    public static AssetBuilder Setup()
    {
        return new AssetBuilder();
    }

    private AssetBuilder()
    {
        _faker = new Faker("en");
    }

    public AssetBuilder Materialize(DbContextFixture.DbContextFixture dbContextFixture)
    {
        _dbContextFixture = dbContextFixture;
        _materialize = true;

        return this;
    }

    public Asset Build()
    {
        var asset = MakeRaw();

        if (_materialize)
        {
            _dbContextFixture.DbContext.Assets.Add(asset);
            _dbContextFixture.DbContext.SaveChanges();
            _dbContextFixture.DbContext.ChangeTracker.Clear();
        }

        return asset;
    }

    public IEnumerable<Asset> BuildMany(int quantity)
    {
        var assets = Enumerable.Range(0, quantity).Select(_ => MakeRaw()).ToList();

        if (_materialize)
        {
            if (_dbContextFixture is null)
                throw new ArgumentNullException("DbContextFixture");

            _dbContextFixture.DbContext.Assets.AddRange(assets);
            _dbContextFixture.DbContext.SaveChanges();
            _dbContextFixture.DbContext.ChangeTracker.Clear();
        }

        return assets;
    }

    private Asset MakeRaw()
    {
        return new Asset
        {
            Id = _faker.Random.Guid(),
            Title = _faker.Lorem.Word(),
            CreatedAt = _faker.Date.Past(),
            Name = _faker.System.FileName(".webp")
        };
    }
}
