using System;
using System.Buffers;

public class Task : AbstractTaskManagerItem, IPrintable
{
    // Властивості для Type
    public TaskType Type { get; set; }

    // Реалізація абстрактних властивостей
    public override string Title { get; set; }
    public override string Description { get; set; }

    // Конструктор для Task
    public Task(string title, string description, TaskType type = TaskType.Upcoming) : base(title, description)
    {
        Type = type;
    }

    // Реалізація методу з інтерфейсу
    public void Print()
    {
        Console.WriteLine($"Задача - {Title}, Опис - {Description}, Поточний стан - {Type}");
    }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Task other = (Task)obj;

        return Title == other.Title && Description == other.Description && Type == other.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Description, Type);
    }
}
