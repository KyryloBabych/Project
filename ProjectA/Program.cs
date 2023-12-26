using System;

class Program
{
    static void Main(string[] args)
    {

        Run();
    }

    static void Run()
    {
        TaskManager taskManager = new TaskManager();
        taskManager.TaskAdded += OnTaskAdded;

        taskManager.TaskStatusChanged += OnTaskStatusChanged;

        taskManager.InProgressCountChanged += InProgressCountChangedHandler;

        while (true)
        {
            Console.WriteLine("===== Меню =====");
            Console.WriteLine("1. Добавити завдання");
            Console.WriteLine("2. Видалити завдання");
            Console.WriteLine("3. Змiнити статус завдання");
            Console.WriteLine("4. Знайти завдання");
            Console.WriteLine("5. Показати всi завдання");
            Console.WriteLine("6. Вийти");

            Console.Write("Оберiть дiю (введiть номер): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTaskMenu(taskManager);
                    break;
                case "2":
                    RemoveTaskMenu(taskManager);
                    break;
                case "3":
                    ChangeTaskTypeMenu(taskManager);
                    break;
                case "4":
                    FindTaskMenu(taskManager);
                    break;
                case "5":
                    if (taskManager.Tasks == null || !taskManager.Tasks.Any())
                    {
                        Console.WriteLine("Список задач пустий.");
                    }
                    else
                    {
                        taskManager.Print();
                        PrintUpcomingTasks(taskManager.Tasks);
                        Console.WriteLine("\nВикористання делегата Action для виведення iнформацii о задачах:");
                        taskManager.ActionDelegate = task => Console.WriteLine($"Задача: {task.Title}, Опис: {task.Description}, Стан: {task.Type}");
                        taskManager.UseActionDelegate();

                        // Використання делегата Predicate
                        Console.WriteLine("\nВикористання делегата Predicate для виведення завершених задач:");
                        taskManager.PredicateDelegate = task => task.Type == TaskType.Completed;
                        taskManager.UsePredicateDelegate();

                        // Використання делегата Func
                        Console.WriteLine("\nВикористання делегата Func для перевірки однiєi задачi:");
                        taskManager.FuncDelegate = task => task.Type == TaskType.Canceled;
                        taskManager.UseFuncDelegate();
                    }
                    break;
                case "6":
                    Console.WriteLine("Програма завершена.");
                    return;
                default:
                    Console.WriteLine("Невiрний ввод. Будь ласка, введiть коректний номер.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void AddTaskMenu(TaskManager taskManager)
    {
        Console.Write("Введiть назву завдання: ");
        string title = Console.ReadLine();

        Console.Write("Введiть опис завдання: ");
        string description = Console.ReadLine();

        Console.Write("Хочете вибрати стан завдання? За замовчуванням - Майбутня (Upcoming) (y/n): ");
        string choice = Console.ReadLine();

        TaskType type;
        if (choice.ToLower() == "y" || choice.ToLower() == "н")
        {
            Console.WriteLine("Оберiть тип завдання:");
            Console.WriteLine("1. В процесi (InProgress)");
            Console.WriteLine("2. Завершене (Completed)");
            Console.WriteLine("3. Скасоване (Canceled)");
            Console.WriteLine("4. Майбутня (Upcoming)");

            string input = Console.ReadLine();

            if (input == "4")
            {
                type = TaskType.Upcoming;
            }
            else if (Enum.TryParse(input, out type))
            {
                // Проверка, чтобы избежать неправильного ввода типа
                if (type < TaskType.Upcoming || type > TaskType.Canceled)
                {
                    Console.WriteLine("Неправильний вибiр типу. Завдання буде додане зі станом по умовчанню.");
                    type = TaskType.Upcoming;
                }
            }
            else
            {
                Console.WriteLine("Неправильний вибiр типу. Завдання буде додане зі станом по умовчанню.");
                type = TaskType.Upcoming;
            }
        }
        else
        {
            type = TaskType.Upcoming;
        }

        taskManager.AddTask(title, description, type);
       // Console.WriteLine("Завдання успiшно додане.");
    }

    static void RemoveTaskMenu(TaskManager taskManager)
    {
        if (taskManager.Tasks.Count == 0)
        {
            Console.WriteLine("Список задач пустий. Нема об'єктів для видалення.");
            return;
        }

        Console.Write("Введiть назву завдання для видалення: ");
        string title = Console.ReadLine();

        List<Task> foundTasks = taskManager.Tasks
            .Where(t => string.Equals(t.Title, title, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (foundTasks.Count > 0)
        {
            Console.WriteLine("Знайдено наступнi завдання:");
            for (int i = 0; i < foundTasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Задача - {foundTasks[i].Title}, Опис - {foundTasks[i].Description}, Стан - {foundTasks[i].Type}");
            }

            if (foundTasks.Count == 1)
            {
                Console.Write("Ви впевненi, що хочете видалити це завдання? (y/n): ");
                if (Console.ReadLine().Trim().ToLower() == "y")
                {
                    Task taskToRemove = foundTasks[0];
                    taskManager.RemoveTask(taskToRemove);
                    Console.WriteLine($"Завдання '{taskToRemove.Title}' видалено.");
                }
                else
                {
                    Console.WriteLine("Видалення скасовано.");
                }
            }
            else
            {
                Console.Write("Введіть номер завдання для видалення: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= foundTasks.Count)
                {
                    Task taskToRemove = foundTasks[choice - 1];
                    taskManager.RemoveTask(taskToRemove);
                    Console.WriteLine($"Завдання '{taskToRemove.Title}' видалено.");
                }
                else
                {
                    Console.WriteLine("Невірний ввід. Видалення скасовано.");
                }
            }
        }
        else
        {
            Console.WriteLine("Завдання не знайдене.");
        }
    }
    static void InProgressCountChangedHandler(int count)
    {
        Console.WriteLine($"Кiлькiсть задач в станi InProgress: {count}");
    }
    static void ChangeTaskTypeMenu(TaskManager taskManager)
    {
        if (taskManager.Tasks.Count == 0)
        {
            Console.WriteLine("Список задач пустий. Нема об'єктів для зміни статусу.");
            return;
        }

        Console.Write("Введiть назву завдання для змiни статусу: ");
        string title = Console.ReadLine();

        List<Task> foundTasks = taskManager.Tasks
            .Where(t => string.Equals(t.Title, title, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (foundTasks.Count > 0)
        {
            Console.WriteLine("Знайдено наступні завдання:");
            for (int i = 0; i < foundTasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {foundTasks[i].Title}, {foundTasks[i].Description}, {foundTasks[i].Type}");
            }

            Console.Write("Введіть номер завдання для зміни статусу: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= foundTasks.Count)
            {
                Task taskToChange = foundTasks[choice - 1];

                Console.WriteLine("Оберіть новий тип завдання:");
                Console.WriteLine("0. Майбутня (Upcoming)");
                Console.WriteLine("1. В процесi (InProgress)");
                Console.WriteLine("2. Завершене (Completed)");
                Console.WriteLine("3. Скасоване (Canceled)");

                if (int.TryParse(Console.ReadLine(), out int newTypeChoice) && Enum.IsDefined(typeof(TaskType), newTypeChoice))
                {
                    TaskType newType = (TaskType)newTypeChoice;
                    taskManager.ChangeTaskType(taskToChange, newType);
                  //  Console.WriteLine($"Статус завдання '{taskToChange.Title}' змінено на '{newType}'.");
                }
                else
                {
                    Console.WriteLine("Невірний ввід. Зміна статусу скасована.");
                }
            }
            else
            {
                Console.WriteLine("Невірний ввід. Зміна статусу скасована.");
            }
        }
        else
        {
            Console.WriteLine("Завдання не знайдене.");
        }
    }

    static void PrintUpcomingTasks(List<Task> tasks)
    {
        

        if (tasks == null || !tasks.Any(task => task.Type == TaskType.Upcoming))
        {
            Console.WriteLine("Вiдсутнi майбутнi задачi в списку.");
            return;
        }

        foreach (var task in tasks)
        {
            if (task is Task printableTask && printableTask.Type == TaskType.Upcoming)
            {
                Console.WriteLine("Полiморфiзм: \nМайбутнi задачi:");
                task.Print();
            }
        }
    }


    static void FindTaskMenu(TaskManager taskManager)
    {
        if (taskManager.Tasks.Count == 0)
        {
            Console.WriteLine("Список задач пустий. Нема об'єктів для пошуку.");
            return;
        }

        Console.Write("Введiть назву завдання для пошуку: ");
        string title = Console.ReadLine();

        List<Task> foundTasks = taskManager.Tasks
            .Where(t => string.Equals(t.Title, title, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (foundTasks.Count > 0)
        {
            Console.WriteLine("Завдання(-я) знайдене(-i):");
            foreach (var task in foundTasks)
            {
                Console.WriteLine($"{task.Title}, {task.Description}, {task.Type}");
            }
        }
        else
        {
            Console.WriteLine("Завдання не знайдене.");
        }
    }
    static void OnTaskAdded(string message)
    {
        Console.WriteLine(message);
    }

    static void OnTaskStatusChanged(string message, TaskType oldType, TaskType newType)
    {
        Console.WriteLine($"{message}. Попереднiй статус: {oldType}, Новий статус: {newType}");
    }

}
