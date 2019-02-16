using System.Collections.Generic;

namespace Assessment_4
{
    // Gear is like the `Inventory` class, except for `Equipable`s only.
    // I probably could have made this a little more modular
    class Gear : List<Equipable>
    {
        private Weapon weapon;
        internal Weapon Weapon
        {
            get
            {
                return this.weapon;
            }
            set
            {
                if (this.weapon != null)
                    this.Remove(this.weapon);

                this.weapon = value;
                this.Add(value);
            }
        }

        private Armor armor;
        internal Armor Armor
        {
            get
            {
                return this.armor;
            }
            set
            {
                if (this.armor != null)
                    this.Remove(this.armor);

                this.armor = value;
                this.Add(value);
            }
        }

        public Gear(Weapon weapon = null, Armor armor = null)
        {
            if (weapon != null)
                this.Weapon = weapon;
            if (armor != null)
                this.Armor = armor;
        }

        public string[] ToNameArray()
        {
            string[] items = new string[] { "[Weapon] ", "[Armor] " };

            if (this.Weapon != null)
                items[0] += this.Weapon.Name;
            else
                items[0] += "Empty Slot";

            if (this.Armor != null)
                items[1] += this.Armor.Name;
            else
                items[1] += "Empty Slot";

            return items;
        }
    }
}
