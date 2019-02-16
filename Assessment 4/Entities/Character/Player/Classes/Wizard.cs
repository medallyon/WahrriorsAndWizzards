using System;

namespace Assessment_4
{
    // This is the mighty Wizard Class
    class Wizard : Player
    {
        private double FireStormExpense = 0.35;
        private int[] FireStormDamage = new int[] { 45, 90 };

        private double HealExpense = 0.1;
        private int[] HealAmount = new int[] { 10, 30 };

        public Wizard(string name) : base(name)
        {
            this.Strength = 100;
            this.Power = 150;

            this.Rest();

            this.Abilities = new string[] { "[35%] Inferno", "[10%] Healing Reflection" };
        }

        // Create an Overload for the constructor so the Player can easily switch classes if they want
        public Wizard(Player existingPlayer) : base(existingPlayer)
        {
            this.Strength = 50;
            this.Power = 150;

            this.Health = this.MaxHealth * (existingPlayer.HealthPercent / 100);
            this.Energy = this.MaxEnergy * (existingPlayer.EnergyPercent / 100);

            this.Abilities = new string[] { "[35%] Inferno", "[10%] Healing Reflection" };
        }

        // Wizard Skill #1
        public void Inferno(BaseEnemy enemy)
        {
            if (this.Energy >= (this.MaxEnergy * this.FireStormExpense))
            {
                Console.Write($"You conjure up a Firestorm and cast it in the {enemy.Name}'s direction.");

                double totalDamage = Utils.Random.Next(this.FireStormDamage[0], this.FireStormDamage[1]) * this.Power;
                enemy.Health -= totalDamage;
                this.Energy -= this.MaxEnergy * this.FireStormExpense;

                Console.Write($"\nThe FireStorm inflicts {totalDamage} damage upon the {enemy.Name}.");
            }

            else
                Console.Write("You do not have enough Energy to cast this spell.");
        }

        // Wizard Skill #1
        public void Heal(Character target)
        {
            if (this.Energy >= (this.MaxEnergy * this.HealExpense))
            {
                double totalHealAmount = Utils.Random.Next(this.HealAmount[0], this.HealAmount[1]) * this.Power;
                target.Health += totalHealAmount;
                this.Energy -= this.MaxEnergy * this.HealExpense;

                Console.Write($"You heal yourself for {totalHealAmount} HP.");
            }

            else
                Console.Write("You do not have enough Energy to cast this spell.");
        }
    }
}
