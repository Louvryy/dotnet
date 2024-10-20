namespace Louvryy.Core.Data.Utils;

public class Pagination<T>
{
    public int Page
    {
        get { return _Page; }
    }

    public int PageSize
    {
        get { return _PageSize; }
    }

    public int Total
    {
        get { return _Total; }
    }

    public IEnumerable<T> Items
    {
        get { return _Items; }
    }

    public int LastPage
    {
        get
        {
            if (Total < PageSize)
                return 1;

            if (Total % PageSize == 0)
                return Convert.ToInt32(Total / PageSize);

            return Convert.ToInt32(Math.Truncate(Convert.ToDouble(Total / PageSize))) + 1;
        }
    }

    private readonly IQueryable<T> _Query;
    private readonly IEnumerable<T> _Items;
    private readonly int _Page;
    private readonly int _PageSize;
    private readonly int _Total;

    public Pagination(IQueryable<T> query, int page, int pageSize)
    {
        _Query = query;
        _Page = page < 1 ? 1 : page;
        _PageSize = pageSize;
        _Total = _Query.Count();
        _Items = _Query
            .Skip((_Page - 1) * _PageSize)
            .Take(_PageSize)
            .ToArray();
    }

    public Pagination(int page, int pageSize, int total, IEnumerable<T> items)
    {
        _Items = items;
        _Page = page;
        _PageSize = pageSize;
        _Total = total;
    }
}