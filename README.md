# C# Data Structures Implementation

A comprehensive implementation of common data structures in C#, built as part of the Object-Oriented Programming course at LCI College.

## Project Structure

This project is organized into multiple components:

- **Stack**: Implementation of the stack data structure (LIFO - Last In, First Out)
- **Queue**: Implementation of the queue data structure (FIFO - First In, First Out)
- **TaskItem**: A custom task item class used by the data structures
- **Driver**: Test application that demonstrates the usage of the implemented data structures

## Features

- Generic Stack implementation with push, pop, and peek operations
- Generic Queue implementation with enqueue, dequeue, and peek operations
- TaskItem class for managing tasks with priority and descriptions
- Comprehensive examples showcasing real-world usage scenarios

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later
- Any code editor (Visual Studio, VS Code, JetBrains Rider, etc.)

### Building the Project

1. Clone the repository
2. Build the solution:
   ```
   dotnet restore
   dotnet build
   ```

### Running the Demo

```
dotnet run --project Driver
```

## Usage Examples

### Using the Stack

```csharp
var stack = new Stack<TaskItem>();
stack.Push(new TaskItem("Complete assignment", 1));
stack.Push(new TaskItem("Read chapter 3", 2));

var topTask = stack.Peek(); // Gets "Read chapter 3"
var task = stack.Pop();     // Removes and returns "Read chapter 3"
```

### Using the Queue

```csharp
var queue = new Queue<TaskItem>();
queue.Enqueue(new TaskItem("Complete assignment", 1));
queue.Enqueue(new TaskItem("Read chapter 3", 2));

var firstTask = queue.Peek(); // Gets "Complete assignment"
var task = queue.Dequeue();   // Removes and returns "Complete assignment"
```

## Releases

Pre-built binaries for multiple platforms are available on the releases page. Each release includes versions for:

- Windows (x64)
- Linux (x64)
- macOS (Intel x64)
- macOS (ARM/Apple Silicon)

## License

This project is licensed for educational purposes only, as part of coursework for LCI College.