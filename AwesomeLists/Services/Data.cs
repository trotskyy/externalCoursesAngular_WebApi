using System;
using System.Collections.Generic;

namespace AwesomeLists.Services
{
    public static class Data
    {
        public static List<TaskList> TaskLists = new List<TaskList> {
            new TaskList
            {
                TaskListId = "1",
                Name = "University Tasks",
                Tasks = new List<Task> {
                    new Task()
                    {
                        TaskId = "task1",
                        TaskListId = "1",
                        Name = "Math",
                        Description = "Do task 1, page 30",
                        Date = new DateTime(2019, 3, 18),
                        Status = Status.ToDo,
                        Priority = Priority.High
                    },
                    new Task()
                    {
                        TaskId = "task2",
                        TaskListId = "1",
                        Name = "Reading",
                        Description = "Read about angular",
                        Date = new DateTime(2019, 3, 19),
                        Status = Status.ToDo,
                        Priority = Priority.High
                    },
                    new Task()
                    {
                        TaskId = "task3",
                        TaskListId = "1",
                        Name = "Reading",
                        Description = "Learn angular cli commands",
                        Date = new DateTime(2019, 3, 18),
                        Status = Status.ToDo,
                        Priority = Priority.Middle
                    },
                    new Task()
                    {
                        TaskId = "task4",
                        TaskListId = "1",
                        Name = "Practice",
                        Description = "Create .net core project",
                        Date = new DateTime(2019, 3, 20),
                        Status = Status.InProgress,
                        Priority = Priority.Middle
                    },
                    new Task()
                    {
                        TaskId = "task5",
                        TaskListId = "1",
                        Name = "Reading",
                        Description = "Read documentation .net core",
                        Date = new DateTime(2019, 3, 15),
                        Status = Status.Done,
                        Priority = Priority.Low
                    }

                }
             },
             new TaskList
             {
                    TaskListId = "2",
                    Name = "University Tasks",
                    Tasks = new List<Task>
                    {
                        new Task()
                        {
                            TaskId = "task1",
                            TaskListId = "2",
                            Name = "C#",
                            Description = "Try out delegates",
                            Date = new DateTime(2019, 3, 18),
                            Status = Status.ToDo,
                            Priority = Priority.High
                        },
                        new Task()
                        {
                            TaskId = "task2",
                            TaskListId = "2",
                            Name = "Reading",
                            Description = "Read about angular",
                            Date = new DateTime(2019, 3, 19),
                            Status = Status.ToDo,
                            Priority = Priority.High
                        },
                        new Task()
                        {
                            TaskId = "task3",
                            TaskListId = "2",
                            Name = "Reading",
                            Description = "Learn angular cli commands",
                            Date = new DateTime(2019, 3, 18),
                            Status = Status.ToDo,
                            Priority = Priority.Middle
                        },
                        new Task()
                        {
                            TaskId = "task4",
                            TaskListId = "2",
                            Name = "Practice",
                            Description = "Create .net core project",
                            Date = new DateTime(2019, 3, 20),
                            Status = Status.InProgress,
                            Priority = Priority.Middle
                        },
                        new Task()
                        {
                            TaskId = "task5",
                            TaskListId = "2",
                            Name = "Reading",
                            Description = "Read documentation .net core",
                            Date = new DateTime(2019, 3, 15),
                            Status = Status.Done,
                            Priority = Priority.Low
                        },
                    }
                }

        };

    }
}
