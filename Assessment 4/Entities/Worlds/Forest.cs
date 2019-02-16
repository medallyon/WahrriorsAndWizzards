using System;
using System.Collections.Generic;
using System.Linq;

namespace Assessment_4
{
    // The Forest is spooky and mysterious - perfect for monster hunting and key-scavenging!
    class Forest : World, IWorld
    {
        // These are used in what you see when `World.Advance()` is called in order to collect the Player's next plan of action
        private static string[] SuspiciousItems = new string[] { "Rock", "Bush", "Puddle" };
        private static string[] DeeperQuotes = new string[]
        {
            "You advance deeper into the forest.",
            "It is tranquil and there are trees all around.",
            "You take a deep breath and decide to continue as usual.",
            "*ssSshHhh* - You hear a faint whisper from faraway."
        };

        public Forest(Game gameInstance) : base(gameInstance)
        {
            this.AdvancingString = "You're in the forest. What do you do?";
            this.AdvancingOptions = new List<string> { "Go deeper", "Use an item", "Head back to the Inn" };
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
            // 55% that nothing happens
            // 33% for an enemy encounter
            // 11% to find a suspicious item
            if (e >= 0 && e <= 4)
                Console.Write($"[ X ] {Forest.DeeperQuotes[Utils.Random.Next(Forest.DeeperQuotes.Length)]}");

            else if (e > 5 && e <= 8)
            {
                BaseEnemy enemy = Utils.GenerateRandomEnemy();
                Console.Write($"You come across a{(Utils.Vowels.Any(v => v == enemy.Name.ToLower()[0]) ? "n" : "")} {enemy.Name}.\nTap [ RETURN ] to get the fight started...");
                Console.ReadLine();

                this.GameInstance.Player.EngageCombat(enemy);
            }

            else
            {
                string suspiciousItem = Forest.SuspiciousItems[Utils.Random.Next(Forest.SuspiciousItems.Length)];
                string[] suspiciousActions = new string[] { $"Inspect the {suspiciousItem}", $"Ignore the {suspiciousItem} and venture forth" };
                int userChoice = Utils.CollectUserChoice($"You advance deeper into the forest. In front of you is a suspicious-looking {suspiciousItem}.", suspiciousActions);

                if (userChoice == 0)
                {
                    int suspiciousEvent = Utils.Random.Next(3);
                    if (suspiciousEvent < 2)
                    {
                        if (Utils.CollectUserChoice("You find a Key! Do you want to pick it up?", new string[] { "Yes", "No" }) == 0)
                        {
                            this.GameInstance.Player.Inventory.Add(new Key());
                            Console.Write($"You choose to pick up the key. Keys in Inventory: {this.GameInstance.Player.Inventory.Keys.Count}");
                        }

                        else
                            Console.Write("You leave the key be. You wonder if it would have come in handy at any point.");
                    }

                    else
                    {
                        BaseEnemy enemy = Utils.GenerateRandomEnemy();
                        Console.Write($"WAHH!! The {suspiciousItem} transmogrifies into a{(Utils.Vowels.Any(v => v == enemy.Name.ToLower()[0]) ? "n" : "")} {enemy.Name}!\nTap [ RETURN ] to get the fight started...");
                        Console.ReadLine();

                        this.GameInstance.Player.EngageCombat(enemy);
                    }
                }

                else
                    Console.Write($"You ignore the {suspiciousItem}.");
            }
        }
    }
}
