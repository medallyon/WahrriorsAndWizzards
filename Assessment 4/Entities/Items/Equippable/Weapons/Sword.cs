namespace Assessment_4
{
    class Sword : Weapon
    {
        public Sword(Character character) : base(character)
        {
            this.damage = 20;
            this.BuyPrice = 80;
            this.StrengthValue = 20;
            this.PowerValue = 20;
        }
    }
}
