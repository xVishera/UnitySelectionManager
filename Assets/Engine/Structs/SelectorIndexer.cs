using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Engine.Structs
{
    public struct SelectorIndexer
    {
        public SelectorIndexer Init()
        {
            LeftIndex = 0;
            MidIndex = 1;
            RightIndex = 2;
            return this;
        }

        public int LeftIndex { get; set; }
        public int MidIndex { get; set; }
        public int RightIndex { get; set; }

        public int GetMaxIndex()
        {
            int maxIndex = LeftIndex;
            if (MidIndex > maxIndex)
                maxIndex = MidIndex;
            if (RightIndex > maxIndex)
                maxIndex = RightIndex;
            return maxIndex;
        }

        public int GetLowestIndex()
        {
            int lowestIndex = LeftIndex;
            if (MidIndex < lowestIndex)
                lowestIndex = MidIndex;
            if (RightIndex < lowestIndex)
                lowestIndex = RightIndex;
            return lowestIndex;
        }
    }
}
