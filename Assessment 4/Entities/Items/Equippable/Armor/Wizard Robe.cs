namespace Assessment_4
{
    class Wizard_Robe : Armor
    {
        public Wizard_Robe(Character character) : base(character)
        {
            this.ArmorValue = 5;
            this.StrengthValue = 10;
            this.PowerValue = 50;
        }
    }
}
