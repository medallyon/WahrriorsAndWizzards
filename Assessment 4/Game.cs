/*
 * MIT License
 *
 * Copyright (c) 2018 Tilman Wirawat Raendchen
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;

namespace Assessment_4
{
    enum EWorlds { Inn, Forest, Sewer }

    class Game
    {
        public bool Over { get; set; }

        public Inn Inn { get; set; }
        public Forest Forest { get; set; }
        public Sewer Sewer { get; set; }

        public EWorlds CurrentWorld { get; set; }

        public string Name { get; set; }
        public Player Player { get; set; }

        public Game(string name, Player player)
        {
            this.Player = player;

            /*
             * Uncomment the following line (52) to enable a cheat in order to progress through the game fast
             */
             
            // this.Player.Gear.Weapon = new Wand(this.Player);

            this.Over = false;
            this.Name = name;

            this.Inn = new Inn(this);
            this.Forest = new Forest(this);
            this.Sewer = new Sewer(this);
            this.CurrentWorld = EWorlds.Inn;
        }

        public void Introduce()
        {
            Console.Clear();

            Console.Write($"Welcome to {this.Name}! You will be playing as an adventurer making their way through a forest. During your travels through this environment, you will encounter a random sequence of events. You must find keys, which will come in handy later. These keys can be found under rocks and such. Once you have enough keys, you should go to the sewers, where nasty foes will lurk for you and try to catch you off-guard, where you must defend yourself. You will eventually encounter doors, which you can unlock with the keys mentioned earlier (or a Wand, if you manage to get one). Once you open the last door, you win the game!");

            Console.Write("\n\nTL;DR\n  1. Rummage through the forest\n  2. Find Keys\n  3. Go to the Sewer and fend off foes\n  4. Open doors\n  5. Win the Game!");

            Console.Write($"\n\nAre you ready, {this.Player.Name}?\nPress [ RETURN ] to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        // The 'Advance' method is called repeatedly in a while loop until the game ends
        public void Advance()
        {
            Console.Clear();
            this.Player.Regenerate();
            // This method prints the player's Health and Energy as a visual representation
            this.Player.PrintStatusString();

            World currentWorld;
            if (this.CurrentWorld == EWorlds.Inn)
                currentWorld = this.Inn;
            else if (this.CurrentWorld == EWorlds.Forest)
                currentWorld = this.Forest;
            else
                currentWorld = this.Sewer;

            int AdvanceChoice = Utils.CollectUserChoice(currentWorld.AdvancingString, currentWorld.AdvancingOptions.ToArray());

            // The program thinks that, when calling `currentWorld.Advance`, I'm calling the 'Advance' method from the 'World' class, whereas I'm actually wanting to call `Inn.Advance` (the specific world, not the parent class)
            // This means I need to repeat the if statements as such:
            if (this.CurrentWorld == EWorlds.Inn)
                this.Inn.Advance(AdvanceChoice);
            else if (this.CurrentWorld == EWorlds.Forest)
                this.Forest.Advance(AdvanceChoice);
            else
                this.Sewer.Advance(AdvanceChoice);

            Utils.Continue();
        }
    }

    class Program
    {
        public static Game GameInstance { get; set; }
        public static string GameName = "WAHrriors & Wizzards";

        static string GenerateWelcomeMessage(bool playingAgain)
        {
            string welcomeString = "Welcome";

            if (playingAgain)
                welcomeString += " back";

            welcomeString += $" to {Program.GameName}!";

            if (!playingAgain)
                welcomeString += "\n\nIn this game you will choose to play as a Warrior or a Wizard.\nYou will need to venture through the wilderness and navigate through tunnel systems in order to win the game.\nOn your journey, you will have to fend off attacking creatures.";

            welcomeString += "\n\nPress the [ RETURN ] button when you are ready to start.";

            return welcomeString;
        }

        public static void Main(string[] args)
        {
            Console.Clear();
            if (args.Length == 0)
                args = new string[] { "false" };

            Console.Write(GenerateWelcomeMessage(bool.Parse(args[0])));
            Console.ReadLine();
            Console.Clear();

            // Instantiate the Player and create a new Game instance
            string[] classOptions = new string[] { "Warrior", "Wizard" };
            int classChoice = Utils.CollectUserChoice("Choose your Class!", classOptions);

            string playerName = Utils.CollectUserAnswer($"You chose to be a {classOptions[classChoice]}! What will you call your character?");

            Player PlayerChar;
            if (classChoice == 0)
                PlayerChar = new Warrior(playerName);
            else
                PlayerChar = new Wizard(playerName);

            Console.Clear();
            Program.GameInstance = new Game(Program.GameName, PlayerChar);

            if (!bool.Parse(args[0]))
                Program.GameInstance.Introduce();

            while (!Program.GameInstance.Over)
                Program.GameInstance.Advance();

            Console.ReadLine();
        }
    }
}
