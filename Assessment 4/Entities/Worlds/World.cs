using System;
using System.Collections.Generic;

namespace Assessment_4
{
    // I dubbed my levels as 'World's, I think it just sounds cooler
    abstract class World
    {
        internal Game GameInstance { get; set; }
        public string AdvancingString { get; set; }
        public List<string> AdvancingOptions { get; set; }
        
        public World(Game instance)
        {
            this.GameInstance = instance;
        }

        // This resets the current World to the Inn
        public void HeadBack()
        {
            Program.GameInstance.CurrentWorld = EWorlds.Inn;
            Program.GameInstance.Inn.Shopkeeper = new Shopkeeper(Program.GameInstance.Inn);

            Console.Write($"You choose to go back to the Inn.");
        }
    }
}
