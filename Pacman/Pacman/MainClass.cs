using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Pacman
{
    class G
    {
        public static int z;
        public static int i,j;
        public static int[] stack;                                               //initialising the global variables
        public static int top;
        public static int count;
        public static int stx;
        public static int sty;
        public static int eny;
        public static int enx;
        public static int[] passed;
        public static int index;
        public static int d;
        public static int path;
        public static int l;
        public static int r;
        public static int u;
        public static int h;
        public static int maxf;
        public static int front;
        public static int depth;
        public static int expandedNodes;
        public static int maxDepth = 0;
        //public static List<int[]> open_set = new List<int[]>();                     //For keeping track of frontier (Priority Queue); of the form (state, g(n), parent, f(n))
        //public static List<int[]> closed_set = new List<int[]>();                   //For keeping track of explored nodes; of the form (state, g(n), parent, f(n))
        public static PriorityQueue open_set = new PriorityQueue();
        public static Hashtable closed_set = new Hashtable();
    }
}
