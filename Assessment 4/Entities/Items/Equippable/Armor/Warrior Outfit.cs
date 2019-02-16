namespace Assessment_4
{
    class Warrior_Outfit : Armor
    {
        public Warrior_Outfit(Character character) : base(character)
        {
            this.ArmorValue = 20;
            this.StrengthValue = 30;
            this.PowerValue = 25;
        }
    }
}
