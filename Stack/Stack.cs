namespace csharp_data_structures;

using System;
using System.IO;

public class TaskStack
{
    private TaskItem? Top { get; set; }
    public int Length { get; private set; }

    public TaskStack()
    {
        Top = null;
        Length = 0;
    }

    public TaskStack(int id, string description, int priority)
    {
        Top = new TaskItem(id, description, priority);
        Length = 1;
    }

    public void Push(int id, string description, int priority)
    {
        TaskItem newTaskItem = new TaskItem(id, description, priority);

        if (Top == null)
        {
            Top = newTaskItem;
        }
        else
        {
            TaskItem temp = Top;
            Top = newTaskItem;
            newTaskItem.Next = temp;
        }

        Length++;
    }

    public TaskItem Pop()
    {
        if (Top == null)
        { throw new InvalidOperationException("Stack is empty"); }

        TaskItem temp = Top;
        Top = Top.Next;
        Length--;
        return temp;
    }

    public TaskItem Peek()
    {
        if (Top == null)
        { throw new InvalidOperationException("Stack is empty"); }

        return Top;
    }

    public void SaveToFile(string path)
    {
        if (Top == null)
        { throw new InvalidOperationException("Stack is empty"); }

        using StreamWriter writer = new StreamWriter(path);
        TaskItem? current = Top;

        writer.WriteLine("TaskStack");

        while (current != null)
        {
            writer.WriteLine($"{current.Id},{current.Description},{current.Priority}");
            current = current.Next;
        }
    }

    public void LoadFromFile(string path)
    {
        using StreamReader reader = new StreamReader(path);

        string? type = reader.ReadLine();
        if (type == null || type != "TaskStack")
        { throw new InvalidOperationException("Invalid type or bad path"); }

        Top = null;
        Length = 0;

        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();
            if (line != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    int id = int.Parse(parts[0]);
                    string description = parts[1];
                    int priority = int.Parse(parts[2]);
                    Push(id, description, priority);
                }
            }
        }
    }
}
