namespace csharp_data_structures;

using System;
using System.IO;

public class TaskQueue
{
    private TaskItem? Head { get; set; }
    private TaskItem? Tail { get; set; }
    public int Length { get; private set; }

    public TaskQueue()
    {
        Head = null;
        Tail = null;
        Length = 0;
    }

    public TaskQueue(int id, string description, int priority)
    {
        Head = new TaskItem(id, description, priority);
        Tail = Head;
        Length = 1;
    }

    public void Enqueue(int id, string description, int priority)
    {
        TaskItem newTaskItem = new TaskItem(id, description, priority);

        if (Head == null || Tail == null)
        {
            Head = newTaskItem;
            Tail = Head;
        }
        else
        {
            TaskItem temp = Tail;
            Tail = newTaskItem;
            newTaskItem.Next = temp;
        }

        Length++;
    }

    public TaskItem Dequeue()
    {
        if (Head == null || Tail == null)
        { throw new InvalidOperationException("Queue is empty"); }

        TaskItem temp = Head;
        Head = Head.Next;
        Length--;
        return temp;
    }

    public TaskItem Peek()
    {
        if (Head == null || Tail == null)
        { throw new InvalidOperationException("Queue is empty"); }

        return Head;
    }

    public void SaveToFile(string path)
    {
        if (Head == null || Tail == null)
        { throw new InvalidOperationException("Queue is empty"); }

        using StreamWriter writer = new StreamWriter(path);
        TaskItem? current = Tail;

        writer.WriteLine("TaskQueue");

        while (current != Head)
        {
            if (current == null)
            { return; }
            writer.WriteLine($"{current.Id},{current.Description},{current.Priority}");
            current = current.Next;
        } 

        writer.WriteLine($"{Head.Id},{Head.Description},{Head.Priority}");
    }

    public void LoadFromFile(string path)
    {
        using StreamReader reader = new StreamReader(path);

        string? type = reader.ReadLine();
        if (type == null || type != "TaskQueue")
        { throw new InvalidOperationException("Invalid type or bad path"); }

        Head = null;
        Tail = null;
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
                    Enqueue(id, description, priority);
                }
            }
        }
    }
}
