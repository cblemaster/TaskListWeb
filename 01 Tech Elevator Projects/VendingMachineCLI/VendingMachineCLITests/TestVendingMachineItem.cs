using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineCLI.Classes;

namespace VendingMachineCLITests
{
    [TestClass]
    public class TestVendingMachineItem
    {
        VendingMachineItem vmi = new VendingMachineItem();
                
        [TestMethod]
        public void TestSound()
        {
            string result1 = "Crunch Crunch, Yum";
            string result2 = "Munch Munch, Yum";
            string result3 = "Glug Glug, Yum";
            string result4 = "Chew Chew, Yum";
            string result5 = "***Unknown item category***";

            vmi.Category = "Chip";
            Assert.AreEqual(result1, vmi.Sound);
            vmi.Category = "Candy";
            Assert.AreEqual(result2, vmi.Sound);
            vmi.Category = "Drink";
            Assert.AreEqual(result3, vmi.Sound);
            vmi.Category = "Gum";
            Assert.AreEqual(result4, vmi.Sound);
            vmi.Category = "Fish";
            Assert.AreEqual(result5, vmi.Sound);
        }

        [TestMethod]
        public void TestToString()
        {
            
            string nameResult = vmi.Name;
            string categoryResult = vmi.Category;
            decimal priceResult = vmi.Price;
            int quantityResult = vmi.Quantity;
            string slotResult = vmi.SlotLocation;

            string result = vmi.ToString();

            Assert.AreEqual(vmi.ToString(), $"Name: {nameResult}, Category: {categoryResult}, Price: {priceResult}, Slot Location: {slotResult}, Quantity: {quantityResult}");

        }
    }
}
