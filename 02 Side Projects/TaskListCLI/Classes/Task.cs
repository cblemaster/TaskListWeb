using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace TaskListCLI.Classes
{
    public class Task
    {
        private const string TASK_FILE_NAME = @"..\..\..\Data\Tasks.txt";
        private const string TASK_NAME_MAX_50_ERROR = "Max length for task name is 20.";
        private const string EDIT_OR_DELETE_SELECTION_INVALID = "Invalid input. Please enter 'e' to edit or 'd' to delete this task ('x' for main menu): ";
        private const string INVALID_INPUT = "Invalid input.";
        private const string DUE_DATE_FORMAT_ERROR = "Due date must be in mm/dd/yyyy format.";
        private const string IS_COMPLETE_VALUE_ERROR = "Is Complete must be either true or false.";
        private const string IS_IMPORTANT_VALUE_ERROR = "Is Important must be either true or false.";
        private const string TASK_UPDATED = "Task updated.";
        private const string ADD_SELECTION_INVALID = "Invalid input. Please enter 'a' to add task ('x' for main menu): ";

        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = TASK_NAME_MAX_50_ERROR)]
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }
        public bool IsImportant { get; set; }
        public List<Task> TasksList { get; set; } = new List<Task>();
        public bool IsDataLoaded { get; set; } = false;

        private int MaxTaskId { get
            {
                int maxId = 1;
                foreach (Task task in TasksList)
                {
                    if (task.Id >= maxId)
                    {
                        maxId = task.Id;
                    }
                }
                maxId++;
                return maxId;
            }
        }

        public Task(int id, string name, DateTime duedate, bool iscomplete, bool isimportant)
        {
            Id = id;
            Name = name;
            DueDate = duedate;
            IsComplete = iscomplete;
            IsImportant = isimportant;
        }

        public Task()
        {
            IsComplete = false;
            IsImportant = false;
        }

        public void ReadDataFromTextFile()
        {
            if (!IsDataLoaded) // don't populate the list if it is already populated!
            {
                using (StreamReader sr = new StreamReader(TASK_FILE_NAME))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] array = line.Split("|");
                        Task task = new Task
                        {
                            Id = Convert.ToInt32(array[0]),
                            Name = Convert.ToString(array[1]),
                            DueDate = Convert.ToDateTime(array[2]),
                            IsImportant = Convert.ToBoolean(array[3]),
                            IsComplete = Convert.ToBoolean(array[4])
                        };

                        TasksList.Add(task);
                    }
                }
                IsDataLoaded = true;
            }
        }

        public void PrintListOfTasks(List<Task> tasks)
        {
            Console.WriteLine("\n**********************************");
            Console.WriteLine("***       ALL TASKS LIST       ***");
            Console.WriteLine("*** 'important' are in all CAPS***");
            Console.WriteLine("**********************************");
            //Console.WriteLine("0----5---10---15---20---25---30---35---40---45---50---55---60");
            Console.Write(String.Format("{0, -5}", "Id"));
            Console.Write(String.Format("{0, -30}", "Name"));
            Console.Write(String.Format("{0, -12}", "Due Date"));
            Console.Write(String.Format("{0, 15}", "Complete\n"));

            foreach (Task t in tasks)
            {
                Console.Write(String.Format("{0, -5}", t.Id));
                if (t.IsImportant)
                {
                    Console.Write(String.Format("{0, -30}", t.Name).ToUpper());
                }
                else
                {
                    Console.Write(String.Format("{0, -30}", t.Name));
                }
                Console.Write(String.Format("{0, -12:MM/dd/yyyy}", t.DueDate));
                Console.Write(String.Format("{0, 15}", t.IsComplete + "\n"));
            }
        }

        public void PrintTaskDetails(Task task)
        {
            Console.WriteLine("\n**********************************");
            Console.WriteLine($"***    DETAILS FOR TASK #{task.Id}    ***");
            Console.WriteLine("**********************************\n");
            Console.WriteLine($"Id: {task.Id}");
            Console.WriteLine($"Name: {task.Name}");
            Console.WriteLine($"Due Date: {task.DueDate:MM/dd/yyyy}");
            Console.WriteLine($"Important: {task.IsImportant}");
            Console.WriteLine($"Complete: {task.IsComplete}");
        }

        public Task GetTaskByTaskId(List<Task> tasks, int detailId)
        {
            foreach (Task task in tasks)
            {
                if (task.Id == detailId)
                {
                    return task;
                }
            }
            return null;
        }

        public Task EditTask(Task taskToUpdate)
        {
            char editPropertySelection = '\0';

            while (editPropertySelection != 'f' && editPropertySelection != 'F')
            {
                Console.WriteLine("\n**********************************");
                Console.WriteLine($"***  EDIT TASK #{taskToUpdate.Id}  ***");
                Console.WriteLine("**********************************\n");
                Console.WriteLine($"Id: {taskToUpdate.Id}");
                Console.WriteLine($"Name: {taskToUpdate.Name}");
                Console.WriteLine($"Due Date: {taskToUpdate.DueDate:MM/dd/yyyy}");
                Console.WriteLine($"Important: {taskToUpdate.IsImportant}");
                Console.WriteLine($"Complete: {taskToUpdate.IsComplete}\n");
                Console.WriteLine("Please make a selection: ");
                Console.WriteLine("'n': Edit task name");               // new value must be string 20 chars or less
                Console.WriteLine("'d': Edit task due date");           // new value must parse into a date mm/dd/yyyy
                Console.WriteLine("'i': Edit task is important");       // new value must parse into bool
                Console.WriteLine("'c': Edit task is complete");        // new value must parse into bool
                Console.WriteLine("'f': Finished editing");             // cancel

                while (!char.TryParse(Console.ReadLine(), out editPropertySelection) || !(editPropertySelection == 'n' || editPropertySelection == 'N' || editPropertySelection == 'd' || editPropertySelection == 'D' || editPropertySelection == 'c' || editPropertySelection == 'C' || editPropertySelection == 'i' || editPropertySelection == 'I' || editPropertySelection == 'f' || editPropertySelection == 'F'))
                {
                    Console.WriteLine(EDIT_OR_DELETE_SELECTION_INVALID);
                }

                string newValue = "";
                if (editPropertySelection != 'f' && editPropertySelection != 'F')
                {
                    newValue = PromptForNewValue();
                }

                if (editPropertySelection == 'n' || editPropertySelection == 'N')
                {
                    if (newValue.Length <= 20)
                    {
                        taskToUpdate.Name = newValue;
                        Console.WriteLine(TASK_UPDATED);
                    }
                    else
                    {
                        Console.WriteLine(TASK_NAME_MAX_50_ERROR);
                    }
                }
                else if (editPropertySelection == 'd' || editPropertySelection == 'D')
                {
                    DateTime newDueDate = DateTime.MinValue;
                    if (DateTime.TryParse(newValue, out newDueDate))
                    {
                        taskToUpdate.DueDate = newDueDate;
                        Console.WriteLine(TASK_UPDATED);
                    }
                    else
                    {
                        Console.WriteLine(DUE_DATE_FORMAT_ERROR);
                    }

                }
                else if (editPropertySelection == 'c' || editPropertySelection == 'C')
                {
                    if (bool.TryParse(newValue, out bool newIsComplete))
                    {
                        taskToUpdate.IsComplete = newIsComplete;
                        Console.WriteLine(TASK_UPDATED);
                    }
                    else
                    {
                        Console.WriteLine(IS_COMPLETE_VALUE_ERROR);
                    }

                }
                else if (editPropertySelection == 'i' || editPropertySelection == 'I')
                {
                    if (bool.TryParse(newValue, out bool newIsImportant))
                    {
                        taskToUpdate.IsImportant = newIsImportant;
                        Console.WriteLine(TASK_UPDATED);
                    }
                    else
                    {
                        Console.WriteLine(IS_IMPORTANT_VALUE_ERROR);
                    }
                }
            }
            return taskToUpdate;
        }

        public Task AddTask()
        {
            char addMenuSelection = '\0';
            while (addMenuSelection != 'x' && addMenuSelection != 'X')
            {
                Console.WriteLine("\n**********************************");
                Console.WriteLine($"***********  ADD TASK #  *********");
                Console.WriteLine("**********************************\n");
                Console.WriteLine("Please make a selection: ");
                Console.WriteLine("'a': Add new task");
                Console.WriteLine("'x': Cancel");

                while (!char.TryParse(Console.ReadLine(), out addMenuSelection) || !(addMenuSelection == 'a' || addMenuSelection == 'A' || addMenuSelection == 'x' || addMenuSelection == 'X'))
                {
                    Console.WriteLine(ADD_SELECTION_INVALID);
                }

                if (addMenuSelection == 'a' || addMenuSelection == 'A')
                {
                    // create new task object
                    Task newTask = new Task
                    {
                        Name = null,
                        DueDate = DateTime.MinValue
                    };

                    while (newTask.Name == null)
                    {
                        // prompt for new task name
                        Console.WriteLine("Enter a name for the new task:");
                        string newName = PromptForNewValue();
                        if (newName.Length <= 20)
                        {
                            newTask.Name = newName;
                        }
                        else
                        {
                            Console.WriteLine(TASK_NAME_MAX_50_ERROR);
                        }
                    }

                    while (newTask.DueDate == DateTime.MinValue)
                    {
                        // prompt for new task due date
                        Console.WriteLine("Enter a due date for the new task:");
                        DateTime newDueDate = DateTime.MinValue;
                        string dueDateInput = PromptForNewValue();
                        if (DateTime.TryParse(dueDateInput, out newDueDate))
                        {
                            newTask.DueDate = newDueDate;
                        }
                        else
                        {
                            Console.WriteLine(DUE_DATE_FORMAT_ERROR);
                        }
                    }
                    if (newTask.Name != null)
                    {
                        // new tasks are isimportant=false, iscomplete=false
                        newTask.IsComplete = false;
                        newTask.IsImportant = false;

                        // get an Id for the new task
                        newTask.Id = MaxTaskId;

                        TasksList.Add(newTask);
                        addMenuSelection = 'x';
                        return newTask;
                    }
                }
            }
            return null;
        }

        public string PromptForNewValue()
        {
            string newValue = "";
            while (newValue == "")
            {
                Console.WriteLine("Enter the new value: ");
                newValue = Console.ReadLine();
            }
            return newValue;
        }

        public void WriteDataToTextFile()
        {
            using (StreamWriter sw = new StreamWriter(TASK_FILE_NAME, false))
            {
                foreach (Task task in TasksList)
                {
                    sw.WriteLine(task.Id.ToString() + "|" + task.Name + "|" + task.DueDate.ToString("MM/dd/yyyy") + "|" + task.IsImportant.ToString() + "|" + task.IsComplete.ToString());
                }
            }
        }
    }
}