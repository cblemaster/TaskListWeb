using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace VendingMachineCLI.Classes
{
    public class VendingMachine
    {
        private const int STARTING_INVENTORY_QTY = 5;

        private readonly string INVENTORY_FILE_PATH = Path.Combine(Environment.CurrentDirectory, @"..\..\..\Example Files", "Inventory.txt");
        private readonly string AUDIT_FILE_PATH = Path.Combine(Environment.CurrentDirectory, @"..\..\..\Example Files", "Log_" + DateTime.Today.ToString("M-dd-yyyy") + ".txt");
        private readonly string SALES_FILE_PATH = Path.Combine(Environment.CurrentDirectory, @"..\..\..\Example Files", "SalesReport_" + DateTime.Today.ToString("M-dd-yyyy") + ".txt");

        private const string FILE_IO_ERROR = "File IO error.";
        private const string APPLICATION_ERROR = "Application error.";
        private const string ITEM_SOLD_OUT = "***ITEM SOLD OUT***";
        private const string VENDING_MACHINE_EMPTY = "***VENDING MACHINE EMPTY***";
        private const string INSUFFICIENT_FUNDS = "***INSUFFICIENT FUNDS***";
        private const string INVALID_SELECTION = "***INVALID SELECTION***";

        private const decimal QUARTER_VALUE = 0.25M;
        private const decimal DIME_VALUE = 0.10M;
        private const decimal NICKEL_VALUE = 0.05M;

        public string Owner { get; }
        public string Model { get; }
        public List<VendingMachineItem> CurrentInventory { get; set; }
        public Dictionary<string, int> ItemsSoldToday { get; set; }
        public bool IsOn { get; set; }
        public decimal DailySales { get; set; }
        public decimal CustomerBalance { get; set; }

        //CONSTRUCTOR
        public VendingMachine(string owner, string model)
        {
            Owner = owner;
            Model = model;
            CurrentInventory = new List<VendingMachineItem>();
            ItemsSoldToday = new Dictionary<string, int>();
            IsOn = false;
            DailySales = 0;
            CustomerBalance = 0;

            //Call methods to fill inventory and set quantities
            FillInventory();
            SetInventoryQuantities();
        }

        //METHODS
        public void TurnVendingMachineOn()
        {
            IsOn = true;
        }

        public void TurnVendingMachineOff()
        {
            IsOn = false;
        }

        public void FillInventory()
        {
            // read txt file
            // set object properties from read-in data
            try
            {
                using (StreamReader sr = new StreamReader(INVENTORY_FILE_PATH))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] lineArray = line.Split("|");
                        VendingMachineItem vmi = new VendingMachineItem
                        {
                            SlotLocation = lineArray[0],
                            Name = lineArray[1],
                            Price = Decimal.Parse(lineArray[2]),
                            Category = lineArray[3]
                        };
                        CurrentInventory.Add(vmi);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(FILE_IO_ERROR);
            }
            catch (Exception e)
            {
                Console.WriteLine(APPLICATION_ERROR);
            }
        }

        public void SetInventoryQuantities()
        {
            foreach (VendingMachineItem vmi in CurrentInventory)
            {
                vmi.Quantity = STARTING_INVENTORY_QTY;
            }
        }

        public string DisplayCurrentInventory()
        {
            string returnMessage = "";
            
            // logic to print formatted inventory
            // item displays as sold out if qty less than 1
            if (CurrentInventory.Count > 0)
            {
                //Console.WriteLine("0----5---10---15---20---25---30---35---40---45---50---55---60");
                StringBuilder sb = new StringBuilder();
                sb.Append("*******************************\n");
                sb.Append("***VENDING MACHINE INVENTORY***\n");
                sb.Append("*******************************\n");
                sb.Append(String.Format("{0, -35}", "Item Name"));
                sb.Append(String.Format("{0, 10}", "Item Slot"));
                sb.Append(String.Format("{0, 30}", "Item Price\n"));

                foreach (VendingMachineItem vmi in CurrentInventory)
                {
                    if (vmi.Quantity < 1)
                    {
                        sb.Append(String.Format("{0, -35}", vmi.Name + " " + ITEM_SOLD_OUT));
                    }
                    else
                    {
                        sb.Append(String.Format("{0, -35}", vmi.Name));
                    }
                    sb.Append(String.Format("{0, 10}", vmi.SlotLocation));
                    sb.Append(String.Format("{0, 30:C}", vmi.Price));
                    sb.Append("\n");
                    returnMessage = sb.ToString();
                }
            }
            else
            {
                returnMessage = VENDING_MACHINE_EMPTY;
            }

            return returnMessage;

        }

        public string DepositMoney(int amountDeposited)
        {
            // adds to customer balance
            CustomerBalance += amountDeposited;
            PrintToAuditFile(DateTime.Now.ToString() + " FEED MONEY: $" + (decimal)(amountDeposited) + " $" + CustomerBalance);
            return DisplayBalance();
        }
        
        public string DisplayBalance()
        {
             return "Your balance is: $" + CustomerBalance;
        }

        public string PurchaseItem(string slotSelected)
        {
            string message = "";
            foreach (VendingMachineItem vmi in CurrentInventory)
            {
                if (vmi.SlotLocation == slotSelected)
                {
                    if (vmi.Quantity > 0)
                    {
                        if (vmi.Price <= CustomerBalance)
                        {
                            CustomerBalance -= vmi.Price;       // deduct from customer balance
                            vmi.Quantity--;                     // deduct from inventory qty
                            DailySales += vmi.Price;            // add to machine daily sales
                            if (!ItemsSoldToday.ContainsKey(vmi.Name))  // add to items sold today
                            {
                                ItemsSoldToday.Add(vmi.Name, 1);  // if the item isn't already in the dictionary, add it
                            }
                            else
                            {
                                ItemsSoldToday[vmi.Name] += 1;    // if the item is in the dictionary, add one to the value
                            }

                            // logs in audit file when an item has been purchased
                            // logs date, time, item name, item slot, item price, customer balance
                            PrintToAuditFile(DateTime.Now.ToString() + " " + vmi.Name + " " + vmi.SlotLocation + " $" + vmi.Price + " $" + CustomerBalance);

                            message = vmi.Sound;
                            break;
                        }
                        else
                        {
                            message = INSUFFICIENT_FUNDS;
                            break;
                        }
                    }
                    else
                    {
                        message = ITEM_SOLD_OUT;
                        break;
                    }
                }
                else
                {
                    message = INVALID_SELECTION;
                }
            }
            return message;
        }

        public void PrintSalesReport()
        {
            // creates the file listing items sold today and the daily sales $
            // logic to print formatted sales items
            try
            {
                using (StreamWriter sw = new StreamWriter(SALES_FILE_PATH, true))
                {
                    if (DailySales > 0)
                    {
                        // iterate thru the dictionary of items sold today
                        // and write (append) to sales report file
                        foreach (KeyValuePair<string, int> kvp in ItemsSoldToday)
                        {
                            sw.WriteLine(kvp.Key + "|" + kvp.Value);
                        }
                        sw.WriteLine("\n**TOTAL SALES** $" + DailySales);xit
                    }
                    else
                    {
                        sw.WriteLine("***NO SALES TODAY***");
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(FILE_IO_ERROR);
            }
            catch (Exception e)
            {
                Console.WriteLine(APPLICATION_ERROR);
            }
        }

        public string DispenseChange()
        {
            DisplayBalance();
            string giveChange = "Your change is ";

            if (CustomerBalance > 0)
            {
                int numQuarters = (int)(CustomerBalance / QUARTER_VALUE);
                CustomerBalance -= (decimal)(numQuarters * QUARTER_VALUE);
                int numDimes = (int)(CustomerBalance / DIME_VALUE);
                CustomerBalance -= (decimal)(numDimes * DIME_VALUE);
                int numNickels = (int)(CustomerBalance / NICKEL_VALUE);
                CustomerBalance -= (decimal)(numNickels * NICKEL_VALUE);

                if (numQuarters != 0)
                {
                    giveChange += numQuarters + " quarter(s) ";
                }
                if (numDimes != 0)
                {
                    giveChange += numDimes + " dime(s) ";
                }
                if (numNickels != 0)
                {
                    giveChange += numNickels + " nickel(s)";
                }
                // log to audit file when a customer finishes a transaction
                // logs date, time, amount of change, customer balance (should be zero)
                PrintToAuditFile(DateTime.Now + " GIVE CHANGE: $" + ((numQuarters * QUARTER_VALUE) + (numDimes * DIME_VALUE) + (numNickels * NICKEL_VALUE)) + " $" + CustomerBalance);
            }
            else
            {
                giveChange += "0";
            }

            return giveChange;
        }

        public void PrintToAuditFile(string auditLine)
        {
            // if audit report file doesn't exist
            // creates a file with date in the name to append to
            // otherwise open the existing audit file to append to

            try
            {
                using (StreamWriter sw = new StreamWriter(AUDIT_FILE_PATH, true))
                {
                    sw.WriteLine(auditLine);
                }
            }

            catch (IOException e)
            {
                Console.WriteLine(FILE_IO_ERROR);
            }
            catch (Exception e)
            {
                Console.WriteLine(APPLICATION_ERROR);
            }
        }
    }
}
