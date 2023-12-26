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
        throw new NotImplementedException();
    }

    static void RemoveTaskMenu(TaskManager taskManager)
    {
        throw new NotImplementedException();
    }

    static void ChangeTaskTypeMenu(TaskManager taskManager)
    {
        throw new NotImplementedException();
    }

    static void FindTaskMenu(TaskManager taskManager)
    {
        throw new NotImplementedException();
    }
}
