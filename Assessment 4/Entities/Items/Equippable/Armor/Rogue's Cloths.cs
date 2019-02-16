namespace Assessment_4
{
    class Rogues_Cloths : Armor
    {
        internal new string Name = "Rogue's Cloths";

        public Rogues_Cloths(Character character) : base(character)
        {
            this.ArmorValue = 10;
            this.StrengthValue = 25;
            this.PowerValue = 25;
        }
    }
}
