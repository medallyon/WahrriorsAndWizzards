namespace Assessment_4
{
    class Dagger : Weapon
    {
        public Dagger(Character character) : base(character)
        {
            this.BuyPrice = 10;
            this.DropChance = 35;
            this.StrengthValue = 1;
            this.PowerValue = 5;
        }
    }
}
