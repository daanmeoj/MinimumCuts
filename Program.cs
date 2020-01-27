using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace Graph
{
    class Program
    {
        public static int RandomGenerator(int lowerBound, int upperBound)
        {
            Random a = new Random();
            int b = a.Next(lowerBound, upperBound);
            return b;
        }
        public static int indexForName(String name, Vertex[] adjLists)
        {
            for (int v = 0; v < adjLists.Length; v++)
            {
                if (adjLists[v].name.Equals(name))
                {
                    return v;
                }
            }
            return -1;
        }

        public static Vertex[] DeleteVertex(Vertex[] adjLists, int position, string chosenVertexName)
        {
            int newSize = adjLists.Length - 1;
            Vertex[] adjLists2 = new Vertex[newSize];

            int count4 = 0;
            for (int v = 0; v < adjLists.Length; v++) 
                {
                if (v != position) {
                    adjLists2[count4] = new Vertex(adjLists[v].name, null);
                    count4++;
                }
            }
            int count3 = 0;
            string NeighborVertexName = "";
            int NeighborVertexNum2;
            for (int v = 0; v < adjLists.Length; v++)
            {
                if (v != position)
                {
                    for (Neighbor nbr = adjLists[v].adjList; nbr != null; nbr = nbr.next)
                    {

                        int NeighborVertexNum = indexForName(adjLists[nbr.vertexNum].name, adjLists2);
                        if (NeighborVertexNum != -1)
                            adjLists2[count3].adjList = new Neighbor(NeighborVertexNum, adjLists2[count3].adjList);
                        else {
                            NeighborVertexName = chosenVertexName;
                            NeighborVertexNum2 = indexForName(NeighborVertexName, adjLists2);
                            //Console.WriteLine(NeighborVertexNum2);
                            adjLists2[count3].adjList = new Neighbor(NeighborVertexNum2, adjLists2[count3].adjList);
                            adjLists2[NeighborVertexNum2].adjList = new Neighbor(count3, adjLists2[NeighborVertexNum2].adjList);
                        }
                    }
                    count3++;
                }
            }
            return adjLists2;
        }
     
        public static int sizeOfVertex(Vertex[] adjLists, int position)
        {
            int adjListsize =0;
            int count2;

            
                count2 = 0;
                for (Neighbor nbr = adjLists[position].adjList; nbr != null; nbr = nbr.next)
                {
                    count2++;
                }

                adjListsize= count2;
            

            return adjListsize;
        }


        public static int EdgeCounter(Vertex[] adjLists)
        {
            int EdgeCounter = 0;

            for (int v = 0; v < adjLists.Length; v++)
            {
                for (Neighbor nbr = adjLists[v].adjList; nbr != null; nbr = nbr.next)
                {
                    EdgeCounter++;
                }
            }

            return EdgeCounter;
        }

        public static string WhatNameIsthatEdge(Vertex [] adjLists, int VertexIndex, int IndexEdge)
        {
            int count6 = 0;
            int v = VertexIndex;
            string name1="";
                for (Neighbor nbr = adjLists[v].adjList; nbr != null; nbr = nbr.next)
                {
                        if (count6 == IndexEdge)
                             name1= adjLists[nbr.vertexNum].name;
                        count6++;
                }
            return name1;
        }

        public static void DeleteEdge(Vertex[] adjLists,int VertexIndex,int CountEdgePosition)
        {

            int v = VertexIndex;
            int w = CountEdgePosition;
            int count5 = 0;
                //Console.WriteLine(adjLists[v].name);
                for (Neighbor nbr = adjLists[v].adjList; nbr != null; nbr = nbr.next)
                {
                //if (nbr.vertexNum == indexForName("97",adjLists))
                //    nbr.next= nbr.next.next;
                if (count5 == w - 1)
                    nbr.next= nbr.next.next;
                count5++;
                }

        }

        public static void printgraph(Vertex[] adjLists)
        {
            for (int v = 0; v < adjLists.Length; v++)
            {
                Console.WriteLine(adjLists[v].name);
                for (Neighbor nbr = adjLists[v].adjList; nbr != null; nbr = nbr.next)
                {
                    Console.Write(" --> " + adjLists[nbr.vertexNum].name);
                }
                Console.WriteLine("\n");
            }
        }

        public static Stack<int> FindRepeatedLocations(Vertex[] adjLists5, string chosenVertexName)
        {
            int count7 = 0;
            Stack<int> Lista = new Stack<int>();
            int v = indexForName(chosenVertexName, adjLists5);
            for (Neighbor nbr = adjLists5[v].adjList; nbr != null; nbr = nbr.next)
            {
                if(adjLists5[nbr.vertexNum].name==chosenVertexName)
                    Lista.Push(count7);
                count7++;
            }
            return Lista;

        }

        public static Vertex[] Contract(Vertex[] adjLists)
        {
            int chosenVertex = RandomGenerator(0, adjLists.Length);
            int sizeV = sizeOfVertex(adjLists, chosenVertex);
            int choseEdge = RandomGenerator(0, sizeV);
            //Console.WriteLine("Vertex Index: " + chosenVertex);
            //Console.WriteLine("Edge Index: " + choseEdge);
            string name1 = WhatNameIsthatEdge(adjLists, chosenVertex, choseEdge);
            //Console.WriteLine("name of edge: " + name1);

           
            int index1 = indexForName(name1, adjLists);
            //Console.WriteLine("Index of vertex belonging to that edge: " + index1);
            string chosenVertexName = adjLists[chosenVertex].name;
            //Console.WriteLine("chosen vertex name: " + chosenVertexName);
            Vertex[] adjLists5 = DeleteVertex(adjLists, index1, chosenVertexName);
            //printgraph(adjLists5);
            Stack<int> l = new Stack<int>();
            l = FindRepeatedLocations(adjLists5, chosenVertexName);
            var index2 = indexForName(chosenVertexName, adjLists5);
            while (l.Count != 0)
            {
                var l2 = l.Pop();
                //Console.WriteLine(l2);
                DeleteEdge(adjLists5, index2, l2);
            }
            
            return adjLists5;
        }
        

        static void Main(string[] args)
        {
            ////////////////////////////////INPUT READ////////////////////////////////////////////////
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader("graph.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line); 
                    
                }
            }
           
            int ListSize = list.Count;
            Vertex[] adjLists = new Vertex[ListSize];
            char[] whitespace = new char[] { ' ', '\t' };


            for (int v = 0; v < adjLists.Length; v++)
            {
                var elements = list.ElementAt(v);
                var e = Regex.Split(elements, @"\s+");
                adjLists[v] = new Vertex(e[0], null);
                //Console.WriteLine("\n");
            }

            int count = 0;
            

            while (count != ListSize)
            {
                var elements = Regex.Split(list.ElementAt(count), @"\s+");
                for (int i = 1; i <(elements.Length-1); i++)
                {
                    int v1 = indexForName(elements[0], adjLists);
                    int v2 = indexForName(elements[i], adjLists);
                    adjLists[v1].adjList = new Neighbor(v2, adjLists[v1].adjList);
                
                }
                count++;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////

            for (int i=0;i<198; i++) { 
            var adjLists3=Contract(adjLists);
            adjLists = adjLists3;
            printgraph(adjLists);
                int a = EdgeCounter(adjLists)/2;
                Console.WriteLine("Edge Amount"+a);
                Console.WriteLine("Vertex amount"+adjLists.Length);
            }

        }
    }
}
