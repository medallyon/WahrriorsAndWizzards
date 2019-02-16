using System;

namespace Assessment_4
{
    // This is the unique Warrior Class
    class Warrior : Player
    {
        private double DecapacitateExpense = 0.1;
        private int[] DecapacitateDamage = new int[] { 10, 30 };

        private double StoneFistExpense = 0.3;
        private int[] StoneFistDamage = new int[] { 35, 50 };

        public Warrior(string name) : base(name)
        {
            this.Strength = 150;
            this.Power = 100;

            this.Rest();

            this.Abilities = new string[] { "[10%] Decapacitate", "[30%] Stone Fist" };
        }

        // Create an Overload for the constructor so the Player can easily switch classes if they want
        public Warrior(Player existingPlayer) : base(existingPlayer)
        {
            this.Strength = 150;
            this.Power = 100;

            this.Health = this.MaxHealth * (existingPlayer.HealthPercent / 100);
            this.Energy = this.MaxEnergy * (existingPlayer.EnergyPercent / 100);

            this.Abilities = new string[] { "[10%] Decapacitate", "[30%] Stone Fist" };
        }

        // Warrior Skill #1
        public void Decapacitate(BaseEnemy enemy)
        {
            if (this.Energy >= (this.MaxEnergy * this.DecapacitateExpense))
            {
                double totalDamage = Utils.Random.Next(this.DecapacitateDamage[0], this.DecapacitateDamage[1]) * this.Power;
                enemy.Health -= totalDamage;
                this.Energy -= this.MaxEnergy * this.DecapacitateExpense;

                Console.Write($"You attempt to decapacitate {enemy.Name} and deal {totalDamage} damage.");
            }

            else
                Console.Write("You do not have enough energy to cast this ability.");
        }

        // Warrior Skill #2
        public void StoneFist(BaseEnemy enemy)
        {
            if (this.Energy >= (this.MaxEnergy * this.StoneFistExpense))
            {
                double totalDamage = Utils.Random.Next(this.StoneFistDamage[0], this.StoneFistDamage[1]) * this.Power;
                enemy.Health -= totalDamage;
                this.Energy -= this.MaxEnergy * this.StoneFistExpense;

                Console.Write($"You charge up your Fist and cast a devastating blow to your enemy for {totalDamage} damage.");
            }

            else
                Console.Write("You do not have enough energy to cast this ability.");
        }
    }
}
