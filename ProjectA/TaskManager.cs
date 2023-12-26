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
        throw new NotImplementedException();
    }

    public void ChangeTaskType(Task task, TaskType newType)
    {
        throw new NotImplementedException();
    }

    public Task FindTask(string title)
    {
        throw new NotImplementedException();
    }

    public void Print()
    {
        throw new NotImplementedException();
    }
}
