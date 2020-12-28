using System;
using VendingMachineCLI.Classes;
using VendingMachineCLI.UI;

namespace VendingMachineCLI
{
    class Program
    {
        private const string MAIN_MENU_OPTION_DISPLAY_ITEMS = "Display Vending Machine Items";
        private const string MAIN_MENU_OPTION_PURCHASE = "Purchase";
        private const string MAIN_MENU_OPTION_EXIT = "Exit";
        private const string MAIN_MENU_OPTION_SALES_REPORT = "Sales Report";
        private readonly string[] MAIN_MENU_OPTIONS = { MAIN_MENU_OPTION_DISPLAY_ITEMS, MAIN_MENU_OPTION_PURCHASE, MAIN_MENU_OPTION_EXIT, MAIN_MENU_OPTION_SALES_REPORT }; //const has to be known at compile time, the array initializer is not const in C#

        private const string PURCHASE_MENU_OPTION_FEED_MONEY = "Feed Money";
        private const string PURCHASE_MENU_OPTION_SELECT_PRODUCT = "Select product";
        private const string PURCHASE_MENU_OPTION_FINISH_TRANSACTION = "Finish transaction";
        private readonly string[] PURCHASE_MENU_OPTIONS = { PURCHASE_MENU_OPTION_FEED_MONEY, PURCHASE_MENU_OPTION_SELECT_PRODUCT, PURCHASE_MENU_OPTION_FINISH_TRANSACTION };

        private const string EXIT_TEXT = "Thank you, please come again!";
        private const string INVALID_SELECTION = "***INVALID SELECTION***";
        private const string FEED_MONEY = "Please enter money in one dollar increments:";
        private const string FEED_MONEY_INVALID = "Negative or zero deposits not allowed.";
        private const string SELECT_PRODUCT = "Please select a product by entering the slot number:";
        private const string NO_MONEY_FED = "Must deposit money before making a selection!";

        private const string VENDING_MACHINE_OWNER = "Umbrella Corp";
        private const string VENDING_MACHINE_MODEL = "Vendo-Matic 600";

        private readonly IBasicUserInterface ui = new MenuDrivenCLI();

        // create vending machine object
        private VendingMachine myVendingMachine = new VendingMachine(VENDING_MACHINE_OWNER, VENDING_MACHINE_MODEL);

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        private void Run()
        {
            myVendingMachine.TurnVendingMachineOn();

            // only show menus if vending machine is on...
            while (myVendingMachine.IsOn)
            {
                while (true)  // infinite loop - need to turn off machine then break on menu exit selection
                {
                    string mainMenuSelection = (string)ui.PromptForSelection(MAIN_MENU_OPTIONS);  // main menu prompt for selection
                    if (mainMenuSelection == MAIN_MENU_OPTION_DISPLAY_ITEMS)
                    {
                        ui.Output(myVendingMachine.DisplayCurrentInventory());
                    }
                    else if (mainMenuSelection == MAIN_MENU_OPTION_PURCHASE)
                    {
                        DisplayPurchaseMenu();
                    }
                    else if (mainMenuSelection == MAIN_MENU_OPTION_EXIT)
                    {
                        ui.Output(EXIT_TEXT);
                        myVendingMachine.TurnVendingMachineOff();
                        break;
                    }
                    else if (mainMenuSelection == MAIN_MENU_OPTION_SALES_REPORT)
                    {
                        myVendingMachine.PrintSalesReport();
                    }
                    else
                    {
                        ui.Output(INVALID_SELECTION);
                    }

                }
            }

        }

        private void DisplayPurchaseMenu()
        {
            while (true)   // infinite loop - need to break on menu finish transaction selection
            {
                string purchaseMenuSelection = (string)ui.PromptForSelection(PURCHASE_MENU_OPTIONS);
                if (purchaseMenuSelection == PURCHASE_MENU_OPTION_FEED_MONEY)
                {
                    ui.Output(FEED_MONEY);
                    string amountDeposited = Console.ReadLine();
                    if (int.Parse(amountDeposited) > 0)
                    {
                        ui.Output(myVendingMachine.DepositMoney(int.Parse(amountDeposited)));
                    }
                    else
                    {
                        ui.Output(FEED_MONEY_INVALID);
                    }
                }
                else if (purchaseMenuSelection == PURCHASE_MENU_OPTION_SELECT_PRODUCT)
                {
                    if (myVendingMachine.CustomerBalance > 0)
                    {
                        ui.Output(SELECT_PRODUCT);
                        string selectedItem = Console.ReadLine();
                        ui.Output(myVendingMachine.PurchaseItem(selectedItem));
                        ui.Output(myVendingMachine.DisplayBalance());
                    }
                    else
                    {
                        ui.Output(NO_MONEY_FED);
                    }
                }
                else if (purchaseMenuSelection == PURCHASE_MENU_OPTION_FINISH_TRANSACTION)
                {
                    ui.Output(myVendingMachine.DispenseChange());
                    break;
                }
            }
        }
    }
}

