using System;

namespace Assessment_4
{
    // This is the base class for any Enemy Character
    abstract class BaseEnemy : Character
    {
        public new string Name
        {
            get
            {
                return this.GetType().Name.Replace('_', ' ');
            }
        }

        public BaseEnemy()
        {
            this.Gold = Utils.Random.Next(5, 30);

            var Weapons = new[]
            {
                typeof(Bow),
                typeof(Dagger),
                typeof(Sword)
            };
            // Equip a random Weapon
            this.Gear.Weapon = (Weapon)Activator.CreateInstance(Weapons[Utils.Random.Next(Weapons.Length)], new object[] { this });

            // Allow for a random difficulty
            this.Strength = Utils.Random.Next(20, 150);
            this.Power = Utils.Random.Next(50, 200);

            this.Health = this.MaxHealth;
            this.Energy = this.MaxEnergy;

            this.AddRandomLoot();
        }

        private void AddRandomLoot()
        {
            // I know it's bad to call the next two lines every time an enemy is created, but I can't find way to make these static
            var EquipableLoot = new[]
            {
                typeof(Rogues_Cloths),
                typeof(Warrior_Outfit),
                typeof(Wizard_Robe),
                typeof(Bow),
                typeof(Dagger),
                typeof(Sword),
                typeof(Wand)
            };
            var OtherLoot = new[]
            {
                typeof(Beef_Lasagne),
                typeof(Beer),
                typeof(Berry),
                typeof(Breadloaf),
                typeof(Chai_Tea),
                typeof(Cheesecake),
                typeof(Chicken_Kiev),
                typeof(Orange_Juice),
                typeof(Spaghett),
                typeof(Thai_Curry),
                typeof(Key)
            };

            // Add Equipable Item to Inventory
            Equipable WeaponDrop = (Equipable)Activator.CreateInstance(EquipableLoot[Utils.Random.Next(EquipableLoot.Length)], new object[] { this });
            double gonnaDrop = Utils.Random.NextDouble();
            if (gonnaDrop <= WeaponDrop.DropChance)
                this.Inventory.Add(WeaponDrop);

            // Add variable amount of other loot to Inventory
            int lootNumber = Utils.Random.Next(5);
            for (int i = 0; i < lootNumber; i++)
            {
                Item SelectedItem = (Item)Activator.CreateInstance(OtherLoot[Utils.Random.Next(OtherLoot.Length)]);

                double dropChance = Utils.Random.NextDouble();
                if (dropChance <= SelectedItem.DropChance)
                    this.Inventory.Add(SelectedItem);
            }
        }

        // This method is similar to `Player.PrintStatusScreen`, but only prints the Health
        public void PrintStatusScreen()
        {
            // The following Console Output is achieved by using https://msdn.microsoft.com/en-us/library/system.console.foregroundcolor(v=vs.110).aspx

            // Print the amount of Health
            Console.Write($"{this.Name}\n[ ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int i = 0; i < 100; i += 2)
            {
                if (this.HealthPercent > i)
                    Console.Write("▓");
                else
                    Console.Write(" ");
            }
            Console.ResetColor();
            Console.Write($" ] ( {this.Health.ToString("0.##")} / {this.MaxHealth.ToString("0.##")} )\n\n");
        }
    }
}
