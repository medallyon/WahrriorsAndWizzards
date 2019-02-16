namespace Assessment_4
{
    abstract class Armor : Equipable
    {
        internal double ArmorValue { get; set; }

        public Armor(Character character) : base(character)
        {
            this.BuyPrice = 100;
        }
    }
}
