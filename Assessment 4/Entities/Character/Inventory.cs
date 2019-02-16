using System.Collections.Generic;
using System.Linq;

namespace Assessment_4
{
    // The Inventory Class is essential to this kind of project - It stores Items!
    class Inventory : List<Item>
    {
        // Hide the `Capacity` property and use it for our own good
        internal new int Capacity { get; set; }

        // This getter is a short-hand that returns all Equipables in the Inventory
        internal List<Equipable> GearItems
        {
            get
            {
                var gear = this.FindAll(item => item is Equipable).Select(item => (Equipable)item);
                if (gear == null)
                    return null;
                else
                    return gear.ToList();
            }
        }
        // This getter is a short-hand that returns all Keys in the Inventory
        internal List<Key> Keys
        {
            get
            {
                return this.FindAll(item => item is Key).Select(item => (Key)item).ToList();
            }
        }

        // This constructs the Object
        public Inventory(List<Item> initialItems = null)
        {
            if (initialItems == null) initialItems = new List<Item>();
            this.AddRange(initialItems);

            this.Capacity = 15;
        }
    }
}
