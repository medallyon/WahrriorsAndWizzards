using System;
using System.Collections.Generic;
using System.Linq;

namespace Assessment_4
{
    // The Shopkeeper is a friendly fellow who will buy wares from and sell wares to you at your leisure
    class Shopkeeper : Character
    {
        internal Inn Inn { get; set; }
        internal Player Player { get; set; }

        public Shopkeeper(Inn innstance, string name = "Franz Lohner") : base()
        {
            this.Inn = innstance;
            this.Name = name;
            this.Player = this.Inn.GameInstance.Player;

            this.Inventory = new Inventory()
            {
                new Rogues_Cloths(this.Player),
                new Warrior_Outfit(this.Player),
                new Wizard_Robe(this.Player),
                new Bow(this.Player),
                new Dagger(this.Player),
                new Sword(this.Player),
                new Wand(this.Player),
                new Beef_Lasagne(),
                new Beer(),
                new Berry(),
                new Breadloaf(),
                new Chai_Tea(),
                new Cheesecake(),
                new Chicken_Kiev(),
                new Orange_Juice(),
                new Spaghett(),
                new Thai_Curry(),
                new Key()
            };
        }

        // This method gives the Player an interface to interact with the Shopkeeper
        public void Interact()
        {
            int userChoice = Utils.CollectUserChoice($"Welcome to {this.Name}'s Wares and Goods. How can I help you today?", new string[] { "Buy", "Sell" });

            if (userChoice == 0)
                this.Browse();
            else
                this.SellMenu();
        }

        // This method buys an item from the Shopkeeper and transfers it into the Player's Inventory
        public void Buy(Item item)
        {
            if (this.Player.Gold < item.BuyPrice)
                Console.Write("You do not have enough gold to buy this item.");
            else
            {
                this.Player.Gold -= item.BuyPrice;
                this.TransferItem(item, this.Player.Inventory);
                Console.Write($"You bought {item.Name} for {item.BuyPrice}.");
            }
        }

        // This method lets the Player browse all available items for sale
        public void Browse()
        {
            int category = Utils.CollectUserChoice("You browse the Shopkeeper's inventory. Choose an item category to view.", new string[] { "Weapons", "Armor", "Food", "Miscellaneous", "Never Mind" });

            // The following blocks of code are very identical - I wondered if there isn't a way to modularise this, but I couldn't figure out how

            List<string> itemList;
            if (category == 0)
            {
                // Find all Weapons in the Shopkeeper's Inventory
                List<Weapon> weapons = this.Inventory.FindAll(item => item is Weapon).Select(item => (Weapon)item).ToList();

                // Cast them into a list of strings
                itemList = weapons.Select(w => $"[{w.BuyPrice}] {w.Name}").ToList();
                itemList.Add("Never Mind");

                // Collect the Player's Choice
                int itemChoice = Utils.CollectUserChoice("Choose a Weapon to purchase:", itemList.ToArray());

                // Act on the Player's Choice
                if (itemChoice < weapons.Count)
                    this.Buy(weapons[itemChoice]);
                else
                    Console.Write("Thanks for stopping by!");

                // Rinse and repeat for the other categories (armor, food, misc.)
            }

            else if (category == 1)
            {
                List<Armor> armor = this.Inventory.FindAll(item => item is Armor).Select(item => (Armor)item).ToList();

                itemList = armor.Select(a => $"[{a.BuyPrice}] {a.Name}").ToList();
                itemList.Add("Never Mind");

                int itemChoice = Utils.CollectUserChoice("Choose a piece of Armor to purchase:", itemList.ToArray());

                if (itemChoice < armor.Count)
                    this.Buy(armor[itemChoice]);
                else
                    Console.Write("Thanks for stopping by!");
            }

            else if (category == 2)
            {
                List<Food> foods = this.Inventory.FindAll(item => item is Food).Select(item => (Food)item).ToList();

                itemList = foods.Select(f => $"[{f.BuyPrice}] {f.Name}").ToList();
                itemList.Add("Never Mind");

                int itemChoice = Utils.CollectUserChoice("Choose a Food Item to purchase:", itemList.ToArray());

                if (itemChoice < foods.Count)
                    this.Buy(foods[itemChoice]);
                else
                    Console.Write("Thanks for stopping by!");
            }

            else if (category == 3)
            {
                List<Item> misc = this.Inventory.FindAll(item => !(item is Weapon) && !(item is Armor) & !(item is Food)).ToList();

                itemList = misc.Select(m => $"[{m.BuyPrice}] {m.Name}").ToList();
                itemList.Add("Never Mind");

                int itemChoice = Utils.CollectUserChoice("Choose an Item to purchase:", itemList.ToArray());

                if (itemChoice < misc.Count)
                    this.Buy(misc[itemChoice]);
                else
                    Console.Write("Thanks for stopping by!");
            }

            else
                Console.Write("Thanks for stopping by!");
        }

        // This method provides an interface for the Player to sell their items and earn some Gold
        public void SellMenu()
        {
            Player player = this.Inn.GameInstance.Player;
            if (player.Inventory.Count == 0)
                Console.Write("You have no items to sell. Come back later.");
            else
            {
                List<string> itemsToSell = player.Inventory.FindAll(x => x != null).Select(x => $"[{x.SellPrice}] {x.Name}").ToList();
                itemsToSell.Add("Never Mind");
                int itemToSell = Utils.CollectUserChoice("Which item do you want to sell?", itemsToSell.ToArray());

                if (itemToSell < player.Inventory.Count)
                {
                    Item item = player.Inventory[itemToSell];
                    player.Sell(item);
                    Console.Write($"You sold {item.Name} to {this.Name} for {item.SellPrice} Gold.");
                }

                else
                    Console.Write("Thanks for stopping by!");
            }
        }
    }
}
