using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TaskManagerTests
{
    [TestMethod]
    public void AddTask_ShouldAddTaskToList()
    {
        // Arrange
        TaskManager taskManager = new TaskManager();
        Task newTask = new Task("Test Task", "Test Description", TaskType.Upcoming);

        // Act
        taskManager.AddTask("Test Task", "Test Description", TaskType.Upcoming);

        // Assert
        CollectionAssert.Contains(GetTasks(taskManager), newTask);
    }


    [TestMethod]
    public void RemoveTask_ShouldRemoveTaskFromList()
    {
        // Arrange
        TaskManager taskManager = new TaskManager();
        Task taskToRemove = new Task("Test Task", "Test Description", TaskType.Upcoming);
        taskManager.AddTask("Test Task", "Test Description", TaskType.Upcoming);

        // Act
        taskManager.RemoveTask(taskToRemove);

        // Assert
        CollectionAssert.DoesNotContain(GetTasks(taskManager), taskToRemove);
    }


    [TestMethod]
    public void ChangeTaskType_ShouldChangeTaskType()
    {
        // Arrange
        TaskManager taskManager = new TaskManager();
        Task taskToChange = new Task("Test Task", "Test Description", TaskType.Upcoming);
        taskManager.AddTask("Test Task", "Test Description", TaskType.Upcoming);

        // Act
        taskManager.ChangeTaskType(taskToChange, TaskType.InProgress);

        // Assert
        Assert.AreEqual(TaskType.InProgress, taskToChange.Type);
    }


    [TestMethod]
    public void FindTask_ShouldFindTaskByTitle()
    {
        // Arrange
        TaskManager taskManager = new TaskManager();
        Task taskToFind = new Task("Test Task", "Test Description", TaskType.Upcoming);
        taskManager.AddTask("Test Task", "Test Description", TaskType.Upcoming);

        // Act
        Task foundTask = taskManager.FindTask("Test Task");

        // Assert
        Assert.AreEqual(taskToFind, foundTask);
    }


    [TestMethod]
    public void Print_ShouldPrintTaskInfo()
    {
        // Arrange
        Task task = new Task("Test Task", "Test Description", TaskType.Upcoming);
        StringWriter sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        task.Print();
        string printedText = sw.ToString().Trim();

        // Assert
        Assert.AreEqual("Задача - Test Task, Опис - Test Description, Поточний стан - Upcoming", printedText);
    }



    private List<Task> GetTasks(TaskManager taskManager)
    {
        FieldInfo field = typeof(TaskManager).GetField("tasks", BindingFlags.NonPublic | BindingFlags.Instance);
        return (List<Task>)field.GetValue(taskManager);
    }

}
