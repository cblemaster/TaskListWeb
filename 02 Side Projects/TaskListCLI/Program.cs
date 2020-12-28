using System;
using TaskListCLI.Classes;
using System.Collections.Generic;
using System.Linq; // using linq to sort lists!

namespace TaskListCLI
{
    class Program
    {
        // During the application session tasks are stored as elements of a list
        
        private const string NO_DATA_LOADED_MESSAGE = "No data loaded - please choose option 1 at the main menu.";
        private const string DATA_LOADED_SUCCESSFULLY_MESSAGE = "Data loaded successfully.";
        private const string DATA_ALREADY_LOADED_MESSAGE = "Data already loaded!";
        private const string INVALID_INPUT = "Invalid input.";
        private const string DELETE_SUCCESSFUL = "Task deleted.";
        private const string EDIT_SUCCESSFUL = "Task updated.";
        private const string MAIN_MENU_SELECTION_INVALID = "Invalid input. Please enter only a number.";
        private const string DETAILS_ID_SELECTION_INVALID = "Invalid input. Please enter only a valid Task Id number (0 for main menu): ";
        private const string UNDER_CONSTRUCTION = "This option is under construction.";
        private const string EDIT_MENU_SELECTION_INVALID = "Enter (e) to edit, (d) to delete, or (x) to return to the main menu";

        private readonly Task myTasks = new Task();

        static void Main(string[] args)
        {
            Program p = new Program();
            p.PrintWelcomeMessage();
            p.RunMainMenu();
        }         
        
        private void PrintWelcomeMessage()
        {
            //welcome message
            Console.WriteLine("**********************************");
            Console.WriteLine("WELCOME to the C# CLI Task List");
            Console.WriteLine("**********************************");
        }

        private void RunMainMenu()
        {
            int mainMenuSelection = -1;
            while (mainMenuSelection != 0)
            {
                // display main menu
                PrintMainMenu();
                mainMenuSelection = PromptForMainMenuSelection();

                if (mainMenuSelection == 1)         // load tasks from txt file (if not already loaded)
                {
                    if (!myTasks.IsDataLoaded)
                    {
                        myTasks.ReadDataFromTextFile();
                        Console.WriteLine(DATA_LOADED_SUCCESSFULLY_MESSAGE);
                    }
                    else
                    {
                        Console.WriteLine(DATA_ALREADY_LOADED_MESSAGE);
                    }
                }
                else if (mainMenuSelection == 2)    // add a new task
                {
                    Task addedTask = myTasks.AddTask();
                    if (addedTask != null)
                    {
                        RunDetailsMenu(addedTask.Id);
                    }
                }
                else if (mainMenuSelection == 3)    // Display all tasks
                {
                    myTasks.PrintListOfTasks(myTasks.TasksList);
                    int detailIdSelection = PromptForDetailId(myTasks.TasksList);
                    if (detailIdSelection != 0)
                    {
                        RunDetailsMenu(detailIdSelection);
                    }
                }
                else if (mainMenuSelection == 4)    // Display all tasks with a due date, ordered by due date
                {
                    List<Task> filteredTasks = new List<Task>();
                    foreach (Task task in myTasks.TasksList)
                    {
                        if (task.DueDate != null && task.DueDate != DateTime.MinValue)
                        {
                            filteredTasks.Add(task);
                        }
                    }
                    filteredTasks = filteredTasks.OrderBy(x => x.DueDate).ToList();
                    myTasks.PrintListOfTasks(filteredTasks);    // TO DO: label the list as tasks w/o due date, indicating the sort field/order too
                    int detailIdSelection = PromptForDetailId(filteredTasks);
                    if (detailIdSelection != 0)
                    {
                        RunDetailsMenu(detailIdSelection);
                    }
                }
                else if (mainMenuSelection == 5)    // Display all tasks without a due date, ordered by name
                {
                    List<Task> filteredTasks = new List<Task>();
                    foreach (Task task in myTasks.TasksList)
                    {
                        if (task.DueDate == null || task.DueDate == DateTime.MinValue)
                        {
                            filteredTasks.Add(task);
                        }
                    }
                    filteredTasks = filteredTasks.OrderBy(x => x.Name).ToList();
                    myTasks.PrintListOfTasks(filteredTasks);    // TO DO: label the list as tasks w/o due date, indicating the sort field/order too
                    int detailIdSelection = PromptForDetailId(filteredTasks);
                    if (detailIdSelection != 0)
                    {
                        RunDetailsMenu(detailIdSelection);
                    }
                }
                else if (mainMenuSelection == 6)    // Display all tasks complete, ordered by due date
                {
                    List<Task> filteredTasks = new List<Task>();
                    foreach (Task task in myTasks.TasksList)
                    {
                        if (task.IsComplete)
                        {
                            filteredTasks.Add(task);
                        }
                    }
                    filteredTasks = filteredTasks.OrderBy(x => x.DueDate).ToList();
                    myTasks.PrintListOfTasks(filteredTasks);    // TO DO: label the list as tasks w/o due date, indicating the sort field/order too
                    int detailIdSelection = PromptForDetailId(filteredTasks);
                    if (detailIdSelection != 0)
                    {
                        RunDetailsMenu(detailIdSelection);
                    }
                }
                else if (mainMenuSelection == 7)    // Display all tasks incomplete, ordered by due date
                {
                    List<Task> filteredTasks = new List<Task>();
                    foreach (Task task in myTasks.TasksList)
                    {
                        if (!task.IsComplete)
                        {
                            filteredTasks.Add(task);
                        }
                    }
                    filteredTasks = filteredTasks.OrderBy(x => x.DueDate).ToList();
                    myTasks.PrintListOfTasks(filteredTasks);    // TO DO: label the list as tasks w/o due date, indicating the sort field/order too
                    int detailIdSelection = PromptForDetailId(filteredTasks);
                    if (detailIdSelection != 0)
                    {
                        RunDetailsMenu(detailIdSelection);
                    }
                }
                else if (mainMenuSelection == 8)    // Display all tasks overdue, ordered by due date
                {
                    List<Task> filteredTasks = new List<Task>();
                    foreach (Task task in myTasks.TasksList)
                    {
                        if (task.DueDate.Date < DateTime.Today.Date)
                        {
                            filteredTasks.Add(task);
                        }
                    }
                    filteredTasks = filteredTasks.OrderBy(x => x.DueDate).ToList();
                    myTasks.PrintListOfTasks(filteredTasks);    // TO DO: label the list as tasks w/o due date, indicating the sort field/order too
                    int detailIdSelection = PromptForDetailId(filteredTasks);
                    if (detailIdSelection != 0)
                    {
                        RunDetailsMenu(detailIdSelection);
                    }
                }
                else if (mainMenuSelection == 9)    // Display all tasks important, ordered by due date
                {
                    List<Task> filteredTasks = new List<Task>();
                    foreach (Task task in myTasks.TasksList)
                    {
                        if (task.IsImportant)
                        {
                            filteredTasks.Add(task);
                        }
                    }
                    filteredTasks = filteredTasks.OrderBy(x => x.DueDate).ToList();
                    myTasks.PrintListOfTasks(filteredTasks);    // TO DO: label the list as tasks w/o due date, indicating the sort field/order too
                    int detailIdSelection = PromptForDetailId(filteredTasks);
                    if (detailIdSelection != 0)
                    {
                        RunDetailsMenu(detailIdSelection);
                    }
                }
                else if (mainMenuSelection == 0)
                {
                    // tasks are written (over-write) to the txt file then program ends
                    // write tasks to text file
                    myTasks.WriteDataToTextFile();
                    //exit
                }
            }
        }

        private void RunDetailsMenu(int detailId)
        {
            Task currentTask = myTasks.GetTaskByTaskId(myTasks.TasksList, detailId);
            myTasks.PrintTaskDetails(currentTask);
            PrintEditMenu();

            char editMenuSelection = '\0';
            while (editMenuSelection != 'x' && editMenuSelection != 'X')
            {
                editMenuSelection = PromptForEditMenuSelection();

                if (editMenuSelection == 'e' || editMenuSelection == 'E')
                {
                    // edit item
                    currentTask = myTasks.EditTask(currentTask);
                }
                else if (editMenuSelection == 'd' || editMenuSelection == 'D')
                {
                    // delete item
                    myTasks.TasksList.Remove(currentTask);
                    Console.WriteLine(DELETE_SUCCESSFUL);
                    editMenuSelection = 'x';
                }
            }
        }

        private void PrintMainMenu()
        {
            Console.WriteLine("\nMAIN MENU SELECTIONS");
            Console.WriteLine("1: Load data");                              // load tasks from txt file
            Console.WriteLine("2. Add a task");                             // add a new task
            Console.WriteLine("3: Display all tasks");                          
            Console.WriteLine("4: Display all tasks with a due date");      // ordered by due date    
            Console.WriteLine("5: Display all tasks without a due date");   // ordered by name   
            Console.WriteLine("6: Display all tasks complete");             // ordered by due date   
            Console.WriteLine("7: Display all tasks incomplete");           // ordered by due date 
            Console.WriteLine("8: Display all tasks overdue");              // ordered by due date
            Console.WriteLine("9: Display all tasks important");            // ordered by due date
            Console.WriteLine("0: Save and exit");                          // tasks are written (over-write) to the txt file then program ends
            Console.WriteLine("---------");
            Console.Write("Please choose an option: ");
        }

        private void PrintEditMenu()
        {
            // display menu
            Console.WriteLine("\nDETAIL MENU SELECTIONS:");
            Console.WriteLine("(e) Edit item");
            Console.WriteLine("(d) Delete item");
            Console.WriteLine("(x) Return to main menu");
            Console.WriteLine("---------");
            Console.Write("Please choose an option: ");
        }
        
        private int PromptForMainMenuSelection()
        {
            int menuSelection = -1;

            while (!int.TryParse(Console.ReadLine(), out menuSelection) || menuSelection < 0 || menuSelection > 9)
            {
                Console.WriteLine(MAIN_MENU_SELECTION_INVALID);
            }
            return menuSelection;
        }

        private int PromptForDetailId(List<Task> tasks)
        {
            int detailIdSelection = -1;

            Console.WriteLine("Enter a task Id to see its details (0 for main menu): ");
            while (!int.TryParse(Console.ReadLine(), out detailIdSelection) || !IsValidDetailId(tasks, detailIdSelection))
            {
                Console.WriteLine(DETAILS_ID_SELECTION_INVALID);
            }
            return detailIdSelection;
        }

        private bool IsValidDetailId(List<Task> tasks, int detailId)
        {
            if (detailId == 0)  // user can enter 0 to cancel
            {
                return true;
            }
            else
            {
                foreach (Task task in tasks)
                {
                    if (task.Id == detailId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private char PromptForEditMenuSelection()
        {
            char editOrDeleteSelection = '\0';

            Console.WriteLine("Enter (e) to edit, (d) to delete, or (x) to return to the main menu:");
            while (!char.TryParse(Console.ReadLine(), out editOrDeleteSelection) || (editOrDeleteSelection != 'e' && editOrDeleteSelection != 'E' && editOrDeleteSelection != 'd' && editOrDeleteSelection != 'D' && editOrDeleteSelection != 'x' && editOrDeleteSelection != 'X'))
            {
                Console.WriteLine(EDIT_MENU_SELECTION_INVALID);
            }
            return editOrDeleteSelection;
        }
    }
}