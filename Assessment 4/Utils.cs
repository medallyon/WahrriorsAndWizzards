using System;
using System.Collections.Generic;
using System.Globalization;

namespace Assessment_4
{
    class Utils
    {
        public static Random Random = new Random();
        public static List<char> Vowels = new List<char>() { 'a', 'e', 'i', 'o', 'u' };
        public static TextInfo TI = new CultureInfo("en-US", false).TextInfo;

        // Clears only the last line of the Console
        // https://stackoverflow.com/a/8946847/4672263
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        // This is used mainly to allow the Player to read the screen and continue by pressing Enter
        public static void Continue()
        {
            Console.Write("\nPress [ RETURN ] to continue...");
            Console.ReadLine();
        }

        // This function simply collects the Player's String Input
        public static string CollectUserAnswer(string question)
        {
            Console.Write($"{question}\n\n > ");
            string Answer = Console.ReadLine();

            while (Answer.Length == 0)
            {
                Console.CursorTop--;
                Utils.ClearCurrentConsoleLine();
                Console.Write(" > ");
                Answer = Console.ReadLine();
            }

            return Answer;
        }

        // This function collects the Player's choice for a selection of options
        public static int CollectUserChoice(string choiceString, string[] options)
        {
            Console.Write(choiceString + "\n\n");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == 0) Console.Write(" > ");
                else Console.Write("   ");

                Console.Write($"{i + 1}. {options[i]}\n");
            }

            Console.CursorVisible = false;
            int SelectedChoice = 0;
            Console.Write($"\nCurrently Selected: {options[SelectedChoice]}");
            int[] lastStringPos = new int[] { Console.CursorLeft, Console.CursorTop };

            ConsoleKey currentInput = Console.ReadKey().Key;
            int currentOptionCursorPos = Console.CursorTop - 1 - options.Length;

            while (currentInput != ConsoleKey.Enter)
            {
                // Update options ('>' pointer) to reflect a change in selection
                if (currentInput == ConsoleKey.UpArrow)
                {
                    SelectedChoice--;

                    if (SelectedChoice >= 0)
                    {
                        Console.SetCursorPosition(0, --currentOptionCursorPos);
                        Console.Write(" > ");
                        Console.SetCursorPosition(0, Console.CursorTop + 1);
                        Console.Write("   ");
                    }
                    
                    // Also allow for rotating selection
                    else
                    {
                        SelectedChoice = options.Length - 1;

                        Console.SetCursorPosition(0, currentOptionCursorPos);
                        Console.Write("   ");

                        currentOptionCursorPos += SelectedChoice;
                        Console.SetCursorPosition(0, currentOptionCursorPos);
                        Console.Write(" > ");
                    }
                }

                else if (currentInput == ConsoleKey.DownArrow)
                {
                    SelectedChoice++;

                    if (SelectedChoice <= options.Length - 1)
                    {
                        Console.SetCursorPosition(0, ++currentOptionCursorPos);
                        Console.Write(" > ");
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write("   ");
                    }

                    // Also allow for rotating selection
                    else
                    {
                        SelectedChoice = 0;

                        Console.SetCursorPosition(0, currentOptionCursorPos);
                        Console.Write("   ");

                        currentOptionCursorPos -= options.Length - 1;
                        Console.SetCursorPosition(0, currentOptionCursorPos);
                        Console.Write(" > ");
                    }
                }

                // Reset cursor to last line
                Console.SetCursorPosition(lastStringPos[0], lastStringPos[1]);

                // Update the final line to reflect current choice
                Utils.ClearCurrentConsoleLine();
                Console.Write($"Currently Selected: {options[SelectedChoice]}");

                currentInput = Console.ReadKey().Key;
            }

            Utils.ClearCurrentConsoleLine();
            Console.CursorVisible = true;
            return SelectedChoice;
        }

        // This method generate a random BaseEnemy
        public static BaseEnemy GenerateRandomEnemy()
        {
            int type = Utils.Random.Next(3);

            if (type == 0)
                return new Sewage_Rat();
            else if (type == 1)
                return new Void_Wizard();
            else
                return new Slime_Crystal();
        }
    }
}
