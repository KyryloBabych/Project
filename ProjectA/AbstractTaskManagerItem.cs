using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class AbstractTaskManagerItem
{
    public abstract string Title { get; set; }
    public abstract string Description { get; set; }

    protected AbstractTaskManagerItem(string title, string description)
    {
        Title = title;
        Description = description;
    }
}

