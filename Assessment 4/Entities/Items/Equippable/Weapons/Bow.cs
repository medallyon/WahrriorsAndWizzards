namespace Assessment_4
{
    class Bow : Weapon
    {
        public Bow(Character character) : base(character)
        {
            this.damage = 30;
            this.BuyPrice = 150;
            this.StrengthValue = 10;
            this.PowerValue = 30;
        }
    }
}
