using System.Collections.Generic;

namespace Assessment_4
{
    interface IWorld
    {
        string AdvancingString { get; set; }
        List<string> AdvancingOptions { get; set; }

        void Advance(int choice);
    }
}
