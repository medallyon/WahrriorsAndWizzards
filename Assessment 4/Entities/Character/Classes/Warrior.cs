using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_4
{
    class Warrior : Player
    {
        public Warrior(string name) : base(name)
        {
            this.Name = name;

            this.Health = 200;
            this.Mana = 50;

            this.Strength = 2;
        }
    }
}
