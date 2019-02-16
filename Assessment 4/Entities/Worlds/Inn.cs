using System;
using System.Collections.Generic;

namespace Assessment_4
{
    // The Inn is a lovely resting place
    class Inn : World, IWorld
    {
        public Shopkeeper Shopkeeper { get; set; }

        public Inn(Game gameInstance) : base(gameInstance)
        {
            this.Shopkeeper = new Shopkeeper(this);
            // These are used in what you see when `World.Advance()` is called in order to collect the Player's next plan of action
            this.AdvancingString = "You're at the inn. What would you like to do?";
            this.AdvancingOptions = new List<string> { "Rest", "Browse the Shopkeep", "Equip Gear", "Change Class", "Head out into the Wild" };
        }

        public void Advance(int choice)
        {
            switch (choice)
            {
                case 0:
                    Program.GameInstance.Player.Rest();
                    Console.Write("You rest and regenerate all of your attributes.");
                    break;
                case 1:
                    this.Shopkeeper.Interact();
                    break;
                case 2:
                    Program.GameInstance.Player.EquipMenu();
                    break;
                case 3:
                    this.SwitchClass();
                    break;
                case 4:
                    this.HeadOut();
                    break;
            }
        }

        // This method switches the Player to another totally different Class (Warrior or Wizard)
        // This really only changes their attributes, but it creates variety
        public void SwitchClass()
        {
            string playerClass = this.GameInstance.Player.Class;

            List<string> classChoices = new List<string>() { "Warrior", "Wizard", "Never Mind" };
            classChoices.RemoveAt(classChoices.FindIndex(x => x.ToLower() == playerClass.ToLower()));

            string userChoice = classChoices[Utils.CollectUserChoice($"You're a {playerClass}. What class do you want to change into? Be mindful of the fact that your attributes will change and you will need to adjust your equipment to be optimised in combat.", classChoices.ToArray())];

            if (userChoice == "Warrior")
                this.GameInstance.Player = new Warrior(this.GameInstance.Player);
            else if (userChoice == "Wizard")
                this.GameInstance.Player = new Wizard(this.GameInstance.Player);
            else
            {
                Console.Write("Alright. You're perfect just the way you are.");
                return;
            }

            Console.Write($"You're a {this.GameInstance.Player.Class} now. Congratulations on the new YOU!");
        }

        // This method allows the Player to choose where to go; Spooky Forest or Stinky Sewer?
        public void HeadOut()
        {
            int userChoice = Utils.CollectUserChoice("Choose where you want to head.", new string[] { "Forest", "Sewers" });

            if (userChoice == 0)
                Program.GameInstance.CurrentWorld = EWorlds.Forest;
            else if (userChoice == 1)
                Program.GameInstance.CurrentWorld = EWorlds.Sewer;

            Console.Write($"You choose to delve into the {Program.GameInstance.CurrentWorld.ToString()}.");
        }
    }
}
