using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID3.DecisionTree
{
    public class NodeID3
    {
        public string name { get; set; } //nazwa at po podziale
        public Dictionary<string,NodeID3> nodes { get; set; } //arc + wezel
    }
}
