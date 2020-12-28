
namespace VendingMachineCLI.Classes
{
    public class VendingMachineItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string SlotLocation { get; set; }
        public int Quantity { get; set; }

        public string Sound
        {
            get
            {
                switch (Category)
                {
                    case "Chip":
                        return "Crunch Crunch, Yum"; ;
                    case "Candy":
                        return "Munch Munch, Yum";
                    case "Drink":
                        return "Glug Glug, Yum";
                    case "Gum":
                        return "Chew Chew, Yum";
                    default:
                        return "***Unknown item category***";
                }

            }
        }

        public override string ToString()
        {
            return "Name: " + Name + ", Category: " + Category + ", Price: " + Price + ", Slot Location: " + SlotLocation + ", Quantity: " + Quantity;
        }   
    }
}
