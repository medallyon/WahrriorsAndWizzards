using System;
using System.Collections.Generic;
using System.Linq;

namespace Assessment_4
{
    class Sewer : World, IWorld
    {
        // These are used in what you see when `World.Advance()` is called in order to collect the Player's next plan of action
        private static string[] DeeperQuotes = new string[]
        {
            "You advance deeper into the Sewer.",
            "It stinks in here.",
            "You want to take a deep breath but that's probably a bad idea in here.",
            "\"I-Is that a mutated rat? Ew.\"",
            "\"Man, I hope I'll find the last door soon\", You think to yourself.",
            "You nearly faint from the stench in this Sewer."
        };
        public int DoorsToVictory { get; set; }

        public Sewer(Game gameInstance) : base(gameInstance)
        {
            this.AdvancingString = "You're in the Sewers. What do you do?";
            this.AdvancingOptions = new List<string> { "Venture Forth", "Use an item", "Head back to the Inn" };

            this.DoorsToVictory = Utils.Random.Next(2, 4);
        }

        public void Advance(int choice)
        {
            switch (choice)
            {
                case 0:
                    this.PlayRandomEvent();
                    break;
                case 1:
                    this.GameInstance.Player.UseItemMenu();
                    break;
                case 2:
                    this.HeadBack();
                    break;
            }
        }

        // This method chooses a random event and plays it out to the Player
        public void PlayRandomEvent()
        {
            // Here we choose a random number from 0-8
            int e = Utils.Random.Next(9);

            // And then we take that number check if it's within our desired parameters
            // This way, we can have events happen based on a percentage value:
            // 66% that nothing happens
            // 22% for an enemy encounter
            // 11% to find a suspicious item
            if (e >= 0 && e <= 5)
                Console.Write($"[ X ] {Sewer.DeeperQuotes[Utils.Random.Next(Sewer.DeeperQuotes.Length)]}");

            else if (e >= 6 && e <= 7)
            {
                BaseEnemy enemy = Utils.GenerateRandomEnemy();
                Console.Write($"You come across a{(Utils.Vowels.Any(v => v == enemy.Name.ToLower()[0]) ? "n" : "")} {enemy.Name}.\nTap [ RETURN ] to get the fight started...");
                Console.ReadLine();

                this.GameInstance.Player.EngageCombat(enemy);
            }

            else
            {
                Door door = new Door();
                Console.Write($"You encounter a grate with a door in it. It has {door.RequiredKeys} Lock{((door.RequiredKeys != 1) ? "s" : "")} on it.\nThe inscription at the bottom of the door indicates a '{this.DoorsToVictory}'.");

                if (this.GameInstance.Player.Inventory.Keys.Count >= door.RequiredKeys || this.GameInstance.Player.Gear.Weapon is Wand)
                {
                    if (Utils.CollectUserChoice($"\n\nDo you want to use {((this.GameInstance.Player.Gear.Weapon is Wand) ? "your Wand" : door.RequiredKeys + " of your Keys")} to unlock this door?", new string[] { "Yes", "No" }) == 0)
                    {
                        this.DoorsToVictory--;
                        for (int i = 0; i < door.RequiredKeys; i++)
                            this.GameInstance.Player.Inventory.RemoveAt(this.GameInstance.Player.Inventory.FindIndex(x => x is Key));

                        if (this.DoorsToVictory > 0)
                            Console.Write($"{(this.GameInstance.Player.Gear.Weapon is Wand ? "As you cast your Wand at the door" : "As you insert each key and start turning")}, you hear whispers coming from all around.\nShivers are going down your spine and the door creeks loudly as you push it open.\n\nYou need to recollect yourself before moving on, but you're good to go.");
                        else
                        {
                            Console.Write("As you step through this last door, you feel yourself dis-integrating.\nYou are literally being boiled in light as your body trascends this mortal realm.\n\nThis is the end.");
                            this.GameInstance.Over = true;

                            if (Utils.CollectUserChoice("\n\nPlay Again?", new string[] { "Yes", "No" }) == 0)
                                Program.Main(new string[] { "true" });
                        }
                    }

                    else
                        Console.Write("You back away from this door and follow another path.");
                }
            }
        }
    }
}
