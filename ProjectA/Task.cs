using System;
using System.Buffers;

public class Task : IPrintable
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskType Type { get; set; }

    public Task(string title, string description, TaskType type)
    {
        Title = title;
        Description = description;
        Type = type;
    }

    public void Print()
    {
        throw new NotImplementedException();
        //Console.WriteLine($"{Title}, {Description}, {Type}");
    }
}
