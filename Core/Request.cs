using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core;

public class Request
{
    private int _page = 1;
    private int _pageSize = 10;

    public int PageSize
    {
        get { return _pageSize; }
        set
        {
            if (value > 0 && value <= 100)
                _pageSize = value;
        }
    }

    public int Page
    {
        get { return _page; }
        set
        {
            if (value >= 1)
                _page = value;
        }
    }

    public string? OrderBy { get; set; }
    public string? OrderByDirection { get; set; }

    public int Skip()
    {

        return (Page - 1) * PageSize;

    }

}
