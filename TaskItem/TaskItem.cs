namespace csharp_data_structures;

public class TaskItem
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public TaskItem? Next { get; set; }

    public TaskItem(int id, string description, int priority)
    {
        Id = id;
        Description = description;
        Priority = priority;
    }
}

