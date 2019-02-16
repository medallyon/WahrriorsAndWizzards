using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_4
{
    class Rogue : Player
    {
        public Rogue(string name) : base(name)
        {
            this.Name = name;

            this.Health = 75;
            this.Stamina = 175;

            this.Dexterity = 2;
        }
    }
}
