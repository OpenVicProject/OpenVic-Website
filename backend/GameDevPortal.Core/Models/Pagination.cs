namespace GameDevPortal.Core.Models;

public record Pagination
{
    private const int _maxPageSize = 20;
    private const int _defaultSize = 10;

    public int Size
    {
        get
        {
            return _size;
        }
        set
        {
            _size = Math.Max(Math.Min(value, _maxPageSize), 1);
        }
    }

    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = Math.Max(value, 0);
        }
    }

    private int _size = _defaultSize;
    private int _index = 0;

    public Pagination(int size, int index)
    {
        Size = size;
        Index = index;
    }

    public Pagination() { }
}