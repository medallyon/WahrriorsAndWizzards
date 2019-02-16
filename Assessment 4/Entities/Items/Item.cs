namespace Assessment_4
{
    // What would a game be without collectable Items? Here we have the Item Class:
    abstract class Item
    {
        private string name = "";
        internal string Name
        {
            get
            {
                return this.GetType().Name.Replace('_', ' ');
            }
            set
            {
                this.name = value;
            }
        }

        // These are for buying and selling in Franz Lohner's shop
        public int BuyPrice { get; set; }
        public int SellPrice
        {
            get
            {
                return this.BuyPrice / 4;
            }
        }
        
        // The `droprate` property helps figure out which items are dropped when a monster is slain
        private double droprate;
        public double DropChance
        {
            get
            {
                return this.droprate;
            }
            set
            {
                this.droprate = value / 100;
            }
        }
    }
}
