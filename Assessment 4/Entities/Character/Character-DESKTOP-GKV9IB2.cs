using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_4
{
    class Character
    {
        public string Name { get; set; }

        internal double MaxHealth { get; set; } = 100;
        internal double Health { get; set; }
        internal double MaxMana { get; set; } = 100;
        internal double Mana { get; set; }
        internal double MaxStamina { get; set; } = 100;
        internal double Stamina { get; set; }

        internal double Strength { get; set; } = 1;
        internal double Intelligence { get; set; } = 1;
        internal double Dexterity { get; set; } = 1;

        internal Inventory Inventory { get; set; }
        internal Gear Gear { get; set; }

        internal double Armor
        {
            get
            {
                double intermediateArmorVal = 0;
                foreach (Item item in this.Gear.FindAll(x => !(x is Weapon)))
                    intermediateArmorVal += item.ArmorValue;
                return intermediateArmorVal;
            }
        }

        public Character(string name)
        {
            this.Name = name;

            this.Health = this.MaxHealth;
            this.Mana = this.MaxMana;
            this.Stamina = this.MaxStamina;

            this.Gear = new Gear();
            this.Inventory = new Inventory();
        }

        internal Character Eat(Food item)
        {
            

            return this;
        }

        internal Character Attack(Character character)
        {
            if (!this.Gear.Any(item => item is Weapon))
                throw new Exception("NO_WPN");

            // Add attacking mechanic here

            return character;
        }
    }
}
