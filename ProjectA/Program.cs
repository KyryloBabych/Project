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
                    taskManager.Print();
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
        Console.WriteLine("Завдання успiшно додане.");
    }

    static void RemoveTaskMenu(TaskManager taskManager)
    {
        if (taskManager.Tasks.Count == 0)
        {
            Console.WriteLine("Список задач пуст. Нечего удалять.");
            return;
        }

        Console.Write("Введiть назву завдання для видалення: ");
        string title = Console.ReadLine();

        Task taskToRemove = taskManager.Tasks.Find(t => string.Equals(t.Title, title, StringComparison.OrdinalIgnoreCase));

        if (taskToRemove != null)
        {
            Console.WriteLine("Знайдене Завдання:");
            taskToRemove.Print();

            Console.Write("Ви впевнені що хочете видалити це завдання? (y/n): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "y" || confirmation.ToLower() == "н")
            {
                taskManager.RemoveTask(taskToRemove);
                Console.WriteLine("Завдання успiшно видалене.");
            }
            else
            {
                Console.WriteLine("Удаление отменено.");
            }
        }
        else
        {
            Console.WriteLine("Завдання не знайдене.");
        }
    }


    static void ChangeTaskTypeMenu(TaskManager taskManager)
    {
        Console.Write("Введiть назву завдання для змiни статусу: ");
        string title = Console.ReadLine();

        Task taskToChange = taskManager.Tasks.Find(t => string.Equals(t.Title, title, StringComparison.OrdinalIgnoreCase));

        if (taskToChange != null)
        {
            Console.WriteLine("Знайдене завдання:");
            taskToChange.Print();

            Console.WriteLine("Оберiть новий тип завдання:");
            Console.WriteLine("1. В процесi (InProgress)");
            Console.WriteLine("2. Завершене (Completed)");
            Console.WriteLine("3. Скасоване (Canceled)");
            Console.WriteLine("4. Майбутня (Upcoming)");

            string input = Console.ReadLine();

            TaskType newType;
            if (input == "4")
            {
                newType = TaskType.Upcoming;
            }
            else if (Enum.TryParse(input, out newType))
            {
                if (newType < TaskType.Upcoming || newType > TaskType.Canceled)
                {
                    Console.WriteLine("Неправильний вибiр типу. Статус завдання буде залишений без змiн.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Неправильний вибiр типу. Статус завдання буде залишений без змiн.");
                return;
            }

            Console.WriteLine($"Ви впевнені, що хочете змінити статус цього завдання на {newType}? (y/n): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "y" || confirmation.ToLower() == "н")
            {
                taskManager.ChangeTaskType(taskToChange, newType);
                Console.WriteLine("Статус завдання успiшно змiнений.");
            }
            else
            {
                Console.WriteLine("Зміна статусу відмінена.");
            }
        }
        else
        {
            Console.WriteLine("Завдання не знайдене.");
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

        Task foundTask = taskManager.Tasks.Find(t => string.Equals(t.Title, title, StringComparison.OrdinalIgnoreCase));
        if (foundTask != null)
        {
            Console.WriteLine($"Завдання знайдене: {foundTask.Title}, {foundTask.Description}, {foundTask.Type}");
        }
        else
        {
            Console.WriteLine("Завдання не знайдене.");
        }
    }
}
