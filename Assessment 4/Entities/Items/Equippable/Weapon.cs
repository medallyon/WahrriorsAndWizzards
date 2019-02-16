namespace Assessment_4
{
    abstract class Weapon : Equipable
    {
        internal double damage { get; set; }
        internal double Multiplier
        {
            get
            {
                return this.Player.Power;
            }
        }
        public double DamageValue
        {
            get
            {
                return this.damage * this.Multiplier;
            }
        }

        public Weapon(Character character) : base(character)
        {
            this.damage = 10;
        }
    }
}
