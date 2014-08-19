using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Multidot;

namespace Pacman
{
    class G
    {
        public static int z;
        public static int i,j;
        public static int[] stack;
        public static int top;
        public static int expandedNodes = 1;                                          //For keeping track of # of expanded nodes

        public static PriorityQueue open_set = new PriorityQueue();                   //For keeping track of frontier (Priority Queue); of the form (state, g(n), parent, f(n))
        public static Hashtable closed_set = new Hashtable();                         //For keeping track of explored nodes; of the form (state, g(n), parent, f(n))

        public static Queue open_set_Q = new Queue();                                 //For keeping track of frontier (Queue); of the form (state, g(n), parent, f(n), food, parentFood)
        public static Stack<Node> open_set_S = new Stack<Node>();                     //For keeping track of frontier (Stack); of the form (state, g(n), parent, f(n), food, parentFood)

        public static PriorityQueueForTileNode open_set_8 = new PriorityQueueForTileNode();                   //For keeping track of frontier (Priority Queue); for the 8 puzzle problem
        public static int maxDepth = 0;                                               //to keep track of the max depth (or high water mark) of the frontier
    }
}
