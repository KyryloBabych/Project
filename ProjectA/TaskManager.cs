using System;
using System.Collections.Generic;

public class TaskManager
{
    private List<Task> tasks;

    public TaskManager()
    {
        tasks = new List<Task>();
    }

    // Добавляем агрегацию
    public List<Task> Tasks
    {
        get { return tasks; }
        set { tasks = value; }
    }

    public void AddTask(string title, string description, TaskType type)
    {
        Task task = new Task(title, description, type);
        tasks.Add(task);
    }

    public void RemoveTask(Task task)
    {
        tasks.Remove(task);
    }

    public void ChangeTaskType(Task task, TaskType newType)
    {
        task.Type = newType;
    }

    public Task FindTask(string title)
    {
        return tasks.Find(t => t.Title == title);
    }

    public void Print()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("Список задач пустий.");
        }
        else
        {
            foreach (var task in tasks)
            {
                if (task is IPrintable printableTask)
                {
                    printableTask.Print();
                }
            }
        }
    }
}
