using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Neighbor
    {
        public int vertexNum { get; set; }

        public Neighbor next { get; set; }

        public Neighbor(int num, Neighbor nbr)
        {
            this.vertexNum = num;
            next = nbr;
        }

        public Neighbor(Neighbor nbr)
        {
           
            next = nbr;
        }

    }
}
