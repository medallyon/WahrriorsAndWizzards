namespace Assessment_4
{
    class Door : Character
    {
        public int RequiredKeys { get; set; } = 1;

        public Door()
        {
            // Make the number of keys required to open this door random
            this.RequiredKeys = Utils.Random.Next(3) + 1;
        }
    }
}
