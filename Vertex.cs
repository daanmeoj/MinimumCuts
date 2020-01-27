using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Vertex
    {
        public string name { get; set; }

        public Neighbor adjList { get; set; }

        public Vertex(string name, Neighbor neighbors)
        {
            this.name = name;
            this.adjList = neighbors;
        }


    }
}
