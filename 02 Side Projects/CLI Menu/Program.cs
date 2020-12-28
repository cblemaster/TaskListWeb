using System;

namespace CLI_Menu
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.WelcomeMessage();
            p.RunMainMenu();
        }

        private void WelcomeMessage()
        {
            // welcome message
            Console.WriteLine("***********************");
            Console.WriteLine("WELCOME to the CLI Menu");
            Console.WriteLine("***********************");
        }
        
        private void RunMainMenu()
        {
            int mainMenuSelection = -1;
            while (mainMenuSelection != 0)
            {
                // display main menu
                Console.WriteLine("\nMENU SELECTIONS:");
                Console.WriteLine("(1) Option one");
                Console.WriteLine("(2) Option two");
                Console.WriteLine("(3) Option three");
                Console.WriteLine("(4) Option four");
                Console.WriteLine("(5) Option five");
                Console.WriteLine("(0) Exit application\n");

                Console.WriteLine("Enter a selection");

                while (!(int.TryParse(Console.ReadLine(), out mainMenuSelection)) || mainMenuSelection > 5 || mainMenuSelection < 0)
                {
                    Console.WriteLine("Selection invalid. Enter a selection.");
                }
                if (mainMenuSelection == 1)
                {
                    // view details
                    Console.WriteLine("1. View details.\n");
                    string param = "Data to pass to details menu";
                    RunDetailsMenu(param);
                }
                else if (mainMenuSelection == 2)
                {
                    // under construction
                    Console.WriteLine("2. Under construction!\n");
                }
                else if (mainMenuSelection == 3)
                {
                    // under construction
                    Console.WriteLine("3. Under construction!\n");
                }
                else if (mainMenuSelection == 4)
                {
                    // under construction
                    Console.WriteLine("4. Under construction!\n");
                }
                else if (mainMenuSelection == 5)
                {
                    // under construction
                    Console.WriteLine("5. Under construction!\n");
                }
            }
        }

        private void RunDetailsMenu(string param)
        {
            char detailsMenuSelection = '\0';
            while (detailsMenuSelection != 'x' && detailsMenuSelection != 'X')
            {
                // SOME SORT OF DETAILS WOULD BE SHOWN HERE
                // BASED ON THE PARAM PASSED IN...
                Console.WriteLine("Passed in data: " + param);
                
                // display menu
                Console.WriteLine("\nMENU SELECTIONS:");
                Console.WriteLine("(e) Edit item");
                Console.WriteLine("(d) Delete item");
                Console.WriteLine("(x) Return to main menu\n");
                
                Console.WriteLine("Enter a selection");

                while (!(char.TryParse(Console.ReadLine(), out detailsMenuSelection)) || ((detailsMenuSelection != 'e') && (detailsMenuSelection != 'E') && (detailsMenuSelection != 'd') && (detailsMenuSelection != 'D') && (detailsMenuSelection != 'x') && (detailsMenuSelection != 'X')))
                {
                    Console.WriteLine("Selection invalid. Enter a selection.");
                }
                if (detailsMenuSelection == 'e' || detailsMenuSelection == 'E')
                {
                    // edit item
                    Console.WriteLine("1. Edit item.\n");
                }
                else if (detailsMenuSelection == 'd' || detailsMenuSelection == 'D')
                {
                    // delete item
                    Console.WriteLine("2. Delete item.\n");
                    detailsMenuSelection = 'x';
                }
            }
        }
    }
}
