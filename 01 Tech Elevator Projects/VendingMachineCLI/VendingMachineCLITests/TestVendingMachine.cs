using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VendingMachineCLI.Classes;

namespace VendingMachineCLITests
{
    [TestClass]
    public class TestVendingMachine
    {
        VendingMachine vm = new VendingMachine("Umbrella Corp", "Vendo-Matic 600");
        
        [TestMethod]
        public void TestConstructor()
        {
            string ownerResult = vm.Owner;
            string modelResult = vm.Model;
            decimal customerBalanceResult = vm.CustomerBalance;
            bool isOnResult = vm.IsOn;
            decimal dailySalesResult = vm.DailySales;
            List<VendingMachineItem> resultCurrentInventory = new List<VendingMachineItem>();
            resultCurrentInventory = vm.CurrentInventory;
            Dictionary<string, int> resultItemsSoldToday = new Dictionary<string, int>();
            resultItemsSoldToday = vm.ItemsSoldToday;
            
            Assert.AreEqual(ownerResult, "Umbrella Corp");
            Assert.AreEqual(modelResult, "Vendo-Matic 600");
            Assert.AreEqual(customerBalanceResult, 0);
            Assert.AreEqual(isOnResult, false);
            Assert.AreEqual(dailySalesResult, 0);
            Assert.AreEqual(resultCurrentInventory.Count, 0);
            Assert.AreEqual(resultItemsSoldToday.Count, 0);

        }

        [TestMethod]
        public void TestDepositMoney()
        {
            string result1 = vm.DepositMoney(0);
            string result2 = vm.DepositMoney(-5);
            string result3 = vm.DepositMoney(12);

            Assert.AreEqual(result1, "Your balance is: $0");   // zero and negative deposits are allowed by the class, but caught and refused in the program
            Assert.AreEqual(result2, "Your balance is: $-5");  // zero and negative deposits are allowed by the class, but caught and refused in the program
            Assert.AreEqual(result3, "Your balance is: $7");
        }

        [TestMethod]
        public void TestDispenseChange()
        {
            vm.CustomerBalance = 6.35M;
            string result = vm.DispenseChange();
            Assert.AreEqual(result, "Your change is 25 quarter(s) 1 dime(s) ");
        }
    }
}
