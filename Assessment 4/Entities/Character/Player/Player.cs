using System;
using System.Collections.Generic;
using System.Linq;

namespace Assessment_4
{
    abstract class Player : Character
    {
        public string Class
        {
            get
            {
                return this.GetType().Name;
            }
        }

        private double HealthRegen = 3;
        private double EnergyRegen = 5;

        public string[] Abilities { get; set; }

        public Player(string name) : base()
        {
            this.Name = name;

            this.Gear.Weapon = new Dagger(this);
        }

        public Player(Player existingPlayer) : base(existingPlayer)
        { }

        public void Regenerate()
        {
            this.Health += this.HealthRegen;
            this.Energy += this.EnergyRegen;
        }

        // This method visualises the Player's Character's Attributes based on their percentaged values
        public void PrintStatusString()
        {
            Console.Write($"{this.Name} | Gold: {this.Gold} | Inventory: [ {this.Inventory.Count} | {this.Inventory.Capacity} ] | # of Keys: {this.Inventory.Keys.Count}");

            // The following Console Output is achieved by using https://msdn.microsoft.com/en-us/library/system.console.foregroundcolor(v=vs.110).aspx
            
            // Print the amount of Health
            Console.Write($"\n\nHealth / {this.Strength} STR\n[ ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int i = 0; i < 100; i += 2)
            {
                if (this.HealthPercent > i)
                    Console.Write("▓");
                else
                    Console.Write(" ");
            }
            Console.ResetColor();
            Console.Write($" ] ( {this.Health.ToString("0.##")} / {this.MaxHealth.ToString("0.##")} )");

            // Print the amount of Energy
            Console.Write($"\n\nEnergy / {this.Power} POW\n[ ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < 100; i += 2)
            {
                if (this.EnergyPercent > i)
                    Console.Write("▓");
                else
                    Console.Write(" ");
            }
            Console.ResetColor();
            Console.Write($" ] ( {this.Energy.ToString("0.##")} / {this.MaxEnergy.ToString("0.##")} )\n\n");
        }

        public void Rest()
        {
            this.Health = this.MaxHealth;
            this.Energy = this.MaxEnergy;
        }

        public void EquipMenu()
        {
            if (this.Inventory.GearItems == null)
                Console.Write("You do not have any items that you could replace right now.");
            else
            {
                List<Equipable> equipables = this.Inventory.GearItems;

                int gearSlot = Utils.CollectUserChoice("Which Gear Slot do you want to equip an item in?", this.Gear.ToNameArray());
                
                if (gearSlot == 0)
                {
                    IEnumerable<Weapon> weapons = this.Inventory.GearItems.FindAll(item => item is Weapon).Select(item => (Weapon)item);

                    if (weapons == null || weapons.Count() == 0)
                        Console.Write("You have no Weapons to equip.");
                    else
                    {
                        int weaponChoice = Utils.CollectUserChoice("Choose a Weapon to replace your current Weapon:", weapons.Select(w => w.Name).ToArray());

                        this.Inventory.Add(this.Gear.Weapon);
                        Weapon chosenWeapon = weapons.ToList()[weaponChoice];
                        this.Gear.Weapon = chosenWeapon;
                        this.Inventory.Remove(chosenWeapon);

                        Console.Write($"\nYou have equipped {this.Gear.Weapon.Name}.");
                    }
                }

                else
                {
                    IEnumerable<Armor> armorItems = this.Inventory.GearItems.FindAll(item => item is Armor).Select(item => (Armor)item);

                    if (armorItems == null || armorItems.Count() == 0)
                        Console.Write("You have no Armor to equip.");
                    else
                    {
                        int armorChoice = Utils.CollectUserChoice("Choose Armor to replace your current Armor Item:", armorItems.Select(a => a.Name).ToArray());

                        this.Inventory.Add(this.Gear.Armor);
                        Armor chosenArmor = armorItems.ToList()[armorChoice];
                        this.Gear.Armor = chosenArmor;
                        this.Inventory.Remove(chosenArmor);

                        Console.Write($"\nYou have equipped {this.Gear.Armor.Name}.");
                    }
                }
            }
        }

        internal void Sell(Item item)
        {
            this.Gold += item.SellPrice;
            this.Inventory.Remove(item);
        }

        public void UseItemMenu()
        {
            List<Food> foods = this.Inventory.FindAll(item => item is Food).Select(x => (Food)x).ToList();
            if (foods == null || foods.Count == 0)
                Console.Write("There are no consumables in your inventory.");
            else
            {
                Food ChosenFood = foods[Utils.CollectUserChoice("Choose an item to consume.", foods.Select(f => f.Name).ToArray())];

                this.Eat(ChosenFood);
                Console.Write($"You've consumed {ChosenFood.Name}.");
            }
        }

        public void EngageCombat(BaseEnemy enemy = null)
        {
            if (enemy == null)
                enemy = Utils.GenerateRandomEnemy();

            int turn = 0;
            while (this.Health > 0 && enemy.Health > 0)
            {
                Console.Clear();
                this.PrintStatusString();
                Console.Write(" ] " + new string('=', 60) + " [\n\n");
                enemy.PrintStatusScreen();

                if (turn == 0)
                {
                    string[] actions = new string[] { "Attack", "Use an Ability", "Consume an Item", "Run Away" };
                    int playerChoice = Utils.CollectUserChoice("What do you do?", actions);

                    if (playerChoice == 0)
                        this.Attack(enemy);

                    else if (playerChoice == 1)
                    {
                        int abilityChoice = Utils.CollectUserChoice("Choose an ability to execute:", this.Abilities);

                        if (this is Warrior)
                        {
                            if (abilityChoice == 0)
                                ((Warrior)this).Decapacitate(enemy);
                            else
                                ((Warrior)this).StoneFist(enemy);
                        }

                        else if (this is Wizard)
                        {
                            if (abilityChoice == 0)
                                ((Wizard)this).Inferno(enemy);
                            else
                                ((Wizard)this).Heal(this);
                        }
                    }

                    else if (playerChoice == 2)
                        this.UseItemMenu();

                    else
                    {
                        if (Utils.Random.Next(2) == 1)
                        {
                            Console.Write("You managed to run away.");
                            break;
                        }

                        else
                            Console.Write("You tried to run away but tripped. You have to keep fighting.");
                    }
                }

                else
                    enemy.Attack(this);

                Utils.Continue();

                // This is much like a toggle. It will always either equal 0 (Player's turn) or -1 (Enemy's turn).
                turn = (turn + 1) * -1;
            }

            if (this.Health <= 0)
            {
                Program.GameInstance.CurrentWorld = EWorlds.Inn;
                this.Rest();

                Console.Write("\nYou fainted and wake up at the Inn. You find yourself to be rested and ready to go.");
            }

            else if (enemy.Health <= 0)
            {
                bool looted = false;
                this.Gold += enemy.Gold;
                Console.Write($"\nYou loot {enemy.Gold} Gold from the {enemy.Name}.");

                // Remove any keys from the lootables if we're in the Sewer
                if (Program.GameInstance.CurrentWorld == EWorlds.Sewer && enemy.Inventory.Any(x => x is Key))
                {
                    foreach (Key key in enemy.Inventory)
                        enemy.Inventory.Remove(key);
                }

                while (enemy.Inventory.Count > 0)
                {
                    looted = true;

                    Console.Clear();
                    this.PrintStatusString();
                    Console.Write($"You killed the {enemy.Name}.");

                    List<string> lootables = enemy.Inventory.Select(x => x.Name).ToList();
                    lootables.Add("Never Mind");
                    int lootChoice = Utils.CollectUserChoice("\nIt dropped some loot. Select what you want to pick up:", lootables.ToArray());

                    if (lootChoice < enemy.Inventory.Count)
                        enemy.TransferItem(enemy.Inventory[lootChoice], this.Inventory);

                    else
                        break;
                }

                Console.Clear();
                this.PrintStatusString();
                Console.Write($"You killed the {enemy.Name}.");
                if (looted)
                    Console.Write($"\nYou also loot {enemy.Gold} Gold from the {enemy.Name}.");
            }
        }
    }
}
