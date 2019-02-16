namespace Assessment_4
{
    // Food is a consumable - I should have made that its own Class...
    abstract class Food : Item
    {
        internal double Health { get; set; }
        internal double Energy { get; set; }

        public Food()
        {
            this.BuyPrice = 15;
            this.DropChance = 50;
        }
    }
}
