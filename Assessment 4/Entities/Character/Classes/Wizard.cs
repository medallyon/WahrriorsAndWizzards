using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_4
{
    class Wizard : Player
    {
        public Wizard(string name) : base(name)
        {
            this.Name = name;

            this.Health = 150;
            this.Mana = 150;
            this.Stamina = 50;

            this.Intelligence = 2;
        }
    }
}
