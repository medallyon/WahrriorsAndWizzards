namespace Assessment_4
{
    class Wand : Weapon
    {
        public Wand(Character character) : base(character)
        {
            this.damage = 50;
            this.BuyPrice = 250;
            this.DropChance = 1;
            this.StrengthValue = 10;
            this.PowerValue = 50;
        }
    }
}
