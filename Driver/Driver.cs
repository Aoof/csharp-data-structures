namespace Driver;

using System;
using System.IO;
using csharp_data_structures;

public static class Driver
{
    private const string STACK_FILE_PATH = "stack_tasks.dat";
    private const string QUEUE_FILE_PATH = "queue_tasks.dat";
    
    public static void Main(string[] args)
    {
        TaskQueue queue = new TaskQueue();
        TaskStack stack = new TaskStack();
        bool running = true;

        while (running)
        {
            Console.Clear();
            DisplayMainMenu();
            
            string? choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    ManageStackTasks(stack);
                    break;
                case "2":
                    ManageQueueTasks(queue);
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void DisplayMainMenu()
    {
        Console.WriteLine("============================================");
        Console.WriteLine("          TASK MANAGEMENT SYSTEM           ");
        Console.WriteLine("============================================");
        Console.WriteLine();
        Console.WriteLine("1. Use the Stack-based Task Manager (LIFO)");
        Console.WriteLine("2. Use the Queue-based Task Manager (FIFO)");
        Console.WriteLine("3. Exit");
        Console.WriteLine();
        Console.Write("Enter your choice: ");
    }

    private static void ManageStackTasks(TaskStack stack)
    {
        bool stackMenu = true;
        while (stackMenu)
        {
            Console.Clear();
            DisplayStackMenu();
            
            string? choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    AddTaskToStack(stack);
                    break;
                case "2":
                    RemoveTaskFromStack(stack);
                    break;
                case "3":
                    ViewStack(stack);
                    break;
                case "4":
                    ViewTopTask(stack);
                    break;
                case "5":
                    SaveStackToFile(stack);
                    break;
                case "6":
                    LoadStackFromFile(stack);
                    break;
                case "7":
                    stackMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void DisplayStackMenu()
    {
        Console.WriteLine("============================================");
        Console.WriteLine("        STACK-BASED TASK MANAGER (LIFO)    ");
        Console.WriteLine("============================================");
        Console.WriteLine();
        Console.WriteLine("1. Add Task to Stack");
        Console.WriteLine("2. Remove Task from Stack");
        Console.WriteLine("3. View Stack (LIFO)");
        Console.WriteLine("4. View Top Task");
        Console.WriteLine("5. Save to file");
        Console.WriteLine("6. Load from file");
        Console.WriteLine("7. Return to Main Menu");
        Console.WriteLine();
        Console.Write("Enter your choice: ");
    }

    private static void ManageQueueTasks(TaskQueue queue)
    {
        bool queueMenu = true;
        while (queueMenu)
        {
            Console.Clear();
            DisplayQueueMenu();
            
            string? choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    AddTaskToQueue(queue);
                    break;
                case "2":
                    RemoveTaskFromQueue(queue);
                    break;
                case "3":
                    ViewQueue(queue);
                    break;
                case "4":
                    ViewNextTask(queue);
                    break;
                case "5":
                    SaveQueueToFile(queue);
                    break;
                case "6":
                    LoadQueueFromFile(queue);
                    break;
                case "7":
                    queueMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void DisplayQueueMenu()
    {
        Console.WriteLine("============================================");
        Console.WriteLine("        QUEUE-BASED TASK MANAGER (FIFO)    ");
        Console.WriteLine("============================================");
        Console.WriteLine();
        Console.WriteLine("1. Add Task to Queue");
        Console.WriteLine("2. Remove Task from Queue");
        Console.WriteLine("3. View Queue (FIFO)");
        Console.WriteLine("4. View Next Task");
        Console.WriteLine("5. Save to file");
        Console.WriteLine("6. Load from file");
        Console.WriteLine("7. Return to Main Menu");
        Console.WriteLine();
        Console.Write("Enter your choice: ");
    }

    private static void AddTaskToStack(TaskStack stack)
    {
        Console.Clear();
        Console.WriteLine("============ ADD TASK TO STACK ============");
        Console.WriteLine();
        
        Console.Write("Enter Task ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID. Press any key to continue...");
            Console.ReadKey();
            return;
        }
        
        Console.Write("Enter Task Description: ");
        string? description = Console.ReadLine() ?? "No description";
        
        Console.Write("Enter Task Priority (1-5): ");
        if (!int.TryParse(Console.ReadLine(), out int priority) || priority < 1 || priority > 5)
        {
            Console.WriteLine("Invalid priority. Press any key to continue...");
            Console.ReadKey();
            return;
        }
        
        stack.Push(id, description, priority);
        Console.WriteLine("Task added successfully! Press any key to continue...");
        Console.ReadKey();
    }

    private static void RemoveTaskFromStack(TaskStack stack)
    {
        Console.Clear();
        Console.WriteLine("========== REMOVE TASK FROM STACK ==========");
        Console.WriteLine();
        
        try
        {
            var task = stack.Pop();
            Console.WriteLine($"Removed Task: ID: {task.Id}, Description: {task.Description}, Priority: {task.Priority}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void ViewStack(TaskStack stack)
    {
        Console.Clear();
        Console.WriteLine("=============== VIEW STACK ===============");
        Console.WriteLine();
        
        if (stack.Length == 0)
        {
            Console.WriteLine("Stack is empty.");
        }
        else
        {
            Console.WriteLine("Stack Tasks (Top to Bottom):");
            Console.WriteLine("----------------------------");
            
            // Create a copy of the stack to avoid modifying the original
            TaskStack tempStack = new TaskStack();
            int originalLength = stack.Length;
            
            // Pop all items from the original stack
            for (int i = 0; i < originalLength; i++)
            {
                try
                {
                    var task = stack.Pop();
                    Console.WriteLine($"{i+1}. ID: {task.Id}, Description: {task.Description}, Priority: {task.Priority}");
                    // Push the item to the temporary stack to restore it later
                    tempStack.Push(task.Id, task.Description, task.Priority);
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }
            
            // Restore the original stack
            int tempLength = tempStack.Length;
            for (int i = 0; i < tempLength; i++)
            {
                var task = tempStack.Pop();
                stack.Push(task.Id, task.Description, task.Priority);
            }
            
            Console.WriteLine($"\nTotal tasks in stack: {stack.Length}");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void ViewTopTask(TaskStack stack)
    {
        Console.Clear();
        Console.WriteLine("=============== VIEW TOP TASK ===============");
        Console.WriteLine();
        
        try
        {
            var task = stack.Peek();
            Console.WriteLine("Top task in stack:");
            Console.WriteLine("------------------");
            Console.WriteLine($"ID: {task.Id}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Priority: {task.Priority}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static void SaveStackToFile(TaskStack stack)
    {
        Console.Clear();
        Console.WriteLine("=========== SAVE STACK TO FILE ===========");
        Console.WriteLine();
        
        try
        {
            stack.SaveToFile(STACK_FILE_PATH);
            Console.WriteLine($"Stack saved to {STACK_FILE_PATH} successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving stack: {ex.Message}");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void LoadStackFromFile(TaskStack stack)
    {
        Console.Clear();
        Console.WriteLine("========= LOAD STACK FROM FILE =========");
        Console.WriteLine();
        
        try
        {
            stack.LoadFromFile(STACK_FILE_PATH);
            Console.WriteLine($"Stack loaded from {STACK_FILE_PATH} successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading stack: {ex.Message}");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void AddTaskToQueue(TaskQueue queue)
    {
        Console.Clear();
        Console.WriteLine("============ ADD TASK TO QUEUE ============");
        Console.WriteLine();
        
        Console.Write("Enter Task ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID. Press any key to continue...");
            Console.ReadKey();
            return;
        }
        
        Console.Write("Enter Task Description: ");
        string? description = Console.ReadLine() ?? "No description";
        
        Console.Write("Enter Task Priority (1-5): ");
        if (!int.TryParse(Console.ReadLine(), out int priority) || priority < 1 || priority > 5)
        {
            Console.WriteLine("Invalid priority. Press any key to continue...");
            Console.ReadKey();
            return;
        }
        
        queue.Enqueue(id, description, priority);
        Console.WriteLine("Task added successfully! Press any key to continue...");
        Console.ReadKey();
    }

    private static void RemoveTaskFromQueue(TaskQueue queue)
    {
        Console.Clear();
        Console.WriteLine("========== REMOVE TASK FROM QUEUE ==========");
        Console.WriteLine();
        
        try
        {
            var task = queue.Dequeue();
            Console.WriteLine($"Removed Task: ID: {task.Id}, Description: {task.Description}, Priority: {task.Priority}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void ViewQueue(TaskQueue queue)
    {
        Console.Clear();
        Console.WriteLine("=============== VIEW QUEUE ===============");
        Console.WriteLine();
        
        if (queue.Length == 0)
        {
            Console.WriteLine("Queue is empty.");
        }
        else
        {
            Console.WriteLine("Queue Tasks (Front to Back):");
            Console.WriteLine("----------------------------");
            
            // Create a temporary queue to store items while viewing
            TaskQueue tempQueue = new TaskQueue();
            int originalLength = queue.Length;
            
            // Dequeue all items from the original queue
            for (int i = 0; i < originalLength; i++)
            {
                try
                {
                    var task = queue.Dequeue();
                    Console.WriteLine($"{i+1}. ID: {task.Id}, Description: {task.Description}, Priority: {task.Priority}");
                    // Enqueue the item to the temporary queue
                    tempQueue.Enqueue(task.Id, task.Description, task.Priority);
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }
            
            // Restore the original queue
            int tempLength = tempQueue.Length;
            for (int i = 0; i < tempLength; i++)
            {
                var task = tempQueue.Dequeue();
                queue.Enqueue(task.Id, task.Description, task.Priority);
            }
            
            Console.WriteLine($"\nTotal tasks in queue: {queue.Length}");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void ViewNextTask(TaskQueue queue)
    {
        Console.Clear();
        Console.WriteLine("=============== VIEW NEXT TASK ===============");
        Console.WriteLine();
        
        try
        {
            var task = queue.Peek();
            Console.WriteLine("Next task in queue:");
            Console.WriteLine("------------------");
            Console.WriteLine($"ID: {task.Id}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Priority: {task.Priority}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static void SaveQueueToFile(TaskQueue queue)
    {
        Console.Clear();
        Console.WriteLine("=========== SAVE QUEUE TO FILE ===========");
        Console.WriteLine();
        
        try
        {
            queue.SaveToFile(QUEUE_FILE_PATH);
            Console.WriteLine($"Queue saved to {QUEUE_FILE_PATH} successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving queue: {ex.Message}");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void LoadQueueFromFile(TaskQueue queue)
    {
        Console.Clear();
        Console.WriteLine("========= LOAD QUEUE FROM FILE =========");
        Console.WriteLine();
        
        try
        {
            queue.LoadFromFile(QUEUE_FILE_PATH);
            Console.WriteLine($"Queue loaded from {QUEUE_FILE_PATH} successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading queue: {ex.Message}");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
