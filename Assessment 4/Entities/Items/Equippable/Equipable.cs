namespace Assessment_4
{
    // The 'Equipable` is an Item you can equip - hence the name
    abstract class Equipable : Item
    {
        private double strength = 0;
        internal double StrengthValue
        {
            get
            {
                return this.strength;
            }
            set
            {
                this.strength = value / 100;
            }
        }
        private double power = 0;
        internal double PowerValue
        {
            get
            {
                return this.power;
            }
            set
            {
                this.power = value / 100;
            }
        }

        internal Character Player { get; set; }

        public Equipable(Character character)
        {
            this.Player = character;
            this.DropChance = 10;
        }
    }
}
