using System;

namespace Assessment_4
{
    // The over-arching Character Class - This is where it all began
    // Marked `abstract` because we don't intend to instantiate this Class at all
    abstract class Character
    {
        private string name = "";
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                // Make sure names are always in TitleCase
                this.name = Utils.TI.ToTitleCase(value);
            }
        }
        public int Gold { get; set; }

        private double strength = 1;
        internal double Strength
        {
            get
            {
                // Accumulate the Strength of all Gear
                double intermediateStrength = this.strength;
                foreach (Equipable gear in this.Gear)
                    intermediateStrength += gear.StrengthValue;
                return intermediateStrength;
            }
            set
            {
                this.strength = value / 100;
            }
        }
        private double power = 1;
        internal double Power
        {
            get
            {
                // Accumulate the Power of all Gear
                double intermeditePower = this.power;
                foreach (Equipable gear in this.Gear)
                    intermeditePower += gear.PowerValue;
                return intermeditePower;
            }
            set
            {
                this.power = value / 100;
            }
        }

        private double defaultMaxHealth = 100;
        internal double MaxHealth
        {
            get
            {
                return this.defaultMaxHealth * this.Strength;
            }
        }
        private double health;
        internal double Health
        {
            get
            {
                return this.health;
            }
            set
            {
                // Apply Armor Bonuses to enable Health Dampening
                if (value < this.health)
                    this.health = this.health - ((this.health - value) * (1 - this.Armor));
                else
                    this.health = value;

                if (this.health > this.MaxHealth)
                    this.health = this.MaxHealth;
                else if (this.health < 0)
                    this.health = 0;
            }
        }
        internal double HealthPercent
        {
            get
            {
                return (this.Health / this.MaxHealth) * 100;
            }
        }

        private double defaultMaxEnergy = 100;
        internal double MaxEnergy
        {
            get
            {
                return this.defaultMaxEnergy * this.Power;
            }
        }
        private double energy;
        internal double Energy
        {
            get
            {
                return this.energy;
            }
            set
            {
                this.energy = value;
                if (this.energy > this.MaxEnergy)
                    this.energy = this.MaxEnergy;
                else if (this.energy < 0)
                    this.energy = 0;
            }
        }
        internal double EnergyPercent
        {
            get
            {
                return (this.Energy / this.MaxEnergy) * 100;
            }
        }

        internal Inventory Inventory { get; set; }
        internal Gear Gear { get; set; }

        internal double Armor
        {
            get
            {
                // Accumulate the Armor value from all Gear
                double intermediateArmorVal = 0;
                foreach (Armor item in this.Gear.FindAll(x => !(x is Weapon)))
                    intermediateArmorVal += item.ArmorValue;
                return intermediateArmorVal / 10;
            }
        }

        public Character()
        {
            this.Gold = 0;

            this.Gear = new Gear();
            this.Inventory = new Inventory();
        }

        // Add an Overload for the Constructor to enable easy Class-Switching for the Player
        public Character(Character existingChar)
        {
            this.Name = existingChar.Name;
            this.Gold = existingChar.Gold;

            this.Gear = existingChar.Gear;
            this.Inventory = existingChar.Inventory;
        }

        // This function transfers an item from this Player's Inventory to another
        internal Inventory TransferItem(Item item, Inventory targetInv)
        {
            if (!this.Inventory.Contains(item))
                Console.Write("You do not have this item.");

            else if (targetInv.Count >= Inventory.Capacity)
                Console.Write("The target inventory is full.");

            else
            {
                targetInv.Add(item);
                this.Inventory.Remove(item);
            }

            return this.Inventory;
        }

        // This function 'eats' an Item
        internal Character Eat(Food item)
        {
            this.Health += item.Health;
            this.Energy += item.Energy;

            this.Inventory.Remove(item);

            return this;
        }

        // This is the Attack function. It attacks.
        internal Character Attack(Character character)
        {
            if (this.Equals(Program.GameInstance.Player))
                Console.Write($"You inflict {this.Gear.Weapon.DamageValue} damage onto the {((BaseEnemy)character).Name}.");
            else
                Console.Write($"{((BaseEnemy)this).Name} inflicts {this.Gear.Weapon.DamageValue} damage onto you.");

            character.Health -= this.Gear.Weapon.DamageValue;

            return character;
        }
    }
}
