using System;
using System.Collections.Generic;
using System.Linq;

public class TaskManager
{
    private List<Task> tasks;

    public Action<Task> ActionDelegate;
    public Predicate<Task> PredicateDelegate;
    public Func<Task, bool> FuncDelegate;

    public delegate void InProgressCountDelegate(int count); //власний делегат

    public event Action<string> TaskAdded;
    public event Action<string, TaskType, TaskType> TaskStatusChanged;

    public event InProgressCountDelegate InProgressCountChanged; //івент що спрацьовує при зміні кількості задач з типом Inprogress

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
        TaskAdded?.Invoke($"Додано - {task.Title}");
        UpdateInProgressCount();
    }

    public void RemoveTask(Task task)
    {
        tasks.Remove(task);
        UpdateInProgressCount();
    }

    public void ChangeTaskType(Task task, TaskType newType)
    {
        TaskType oldType = task.Type;
        task.Type = newType;
        TaskStatusChanged?.Invoke($"Задача '{task.Title}' змiнена з {oldType} на {newType}", oldType, newType);
        if (newType == TaskType.InProgress && oldType != TaskType.InProgress)
        {
            UpdateInProgressCount();
        }
    }
    private void UpdateInProgressCount()
    {
        int inProgressCount = tasks.Count(task => task.Type == TaskType.InProgress);
        InProgressCountChanged?.Invoke(inProgressCount);
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

    // Метод для использования делегата Action
    public void UseActionDelegate()
    {
        if (ActionDelegate != null && tasks != null && tasks.Any())
        {
            foreach (var task in tasks)
            {
                ActionDelegate(task);
            }
        }
        else
        {
            Console.WriteLine("Делегат Action або список задач не встановленi або пустi.");
        }
    }

    // Метод для использования делегата Predicate
    public void UsePredicateDelegate()
    {
        if (PredicateDelegate != null && tasks != null && tasks.Any())
        {
            var filteredTasks = tasks.FindAll(PredicateDelegate);
            foreach (var task in filteredTasks)
            {
                task.Print();
            }
        }
        else
        {
            Console.WriteLine("Делегат Predicate або список задач не встановленi або пустi.");
        }
    }

    // Метод для использования делегата Func
    public void UseFuncDelegate()
    {
        if (FuncDelegate != null && tasks != null && tasks.Any())
        {
            var result = FuncDelegate(tasks[0]);
            Console.WriteLine($"Результат роботи делегата Func, перший елемент є скасованою задачею?: {result}");
        }
        else
        {
            Console.WriteLine("Делегат Func або список задач не встановленi або пустi.");
        }
    }

}


