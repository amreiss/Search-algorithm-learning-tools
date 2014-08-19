using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Multidot;

namespace Pacman
{
    public partial class MainForm : Form
    {
        int[,] small;                   //for maintaining the Maze
        int[,] heuristic;               //for maintaining the heuristic matrix
        Hashtable closed_set = new Hashtable();                         //For keeping track of explored nodes; of the form (state, g(n), parent, f(n))
        Queue open_set_Q = new Queue();                                 //For keeping track of frontier (Queue); of the form (state, g(n), parent, f(n), food, parentFood)
        Stack open_set_S = new Stack();                                 //For keeping track of frontier (Stack); of the form (state, g(n), parent, f(n), food, parentFood)

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBFS_Click(object sender, EventArgs e)
        {
            //Representing a small maze
            var small = new int[10, 22] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            G.stack = new int[100000];

            for (G.i = 0; G.i < 100000; G.i++)                  //Initializing all stack elements with 0
                G.stack[G.i] = 0;

            G.i = 3; G.j = 11;                                  //Starting position in the maze

            int z = 0;
            int value = position(G.i, G.j);

            G.stack[z] = value;
            G.top = 0; 
            int length, breadth;

            while (G.stack[G.top] != G.i)
            {
                int current = G.stack[G.top];                   //Pop the top-most element out of the stack

                G.i = current/10000;                            //Decompose into x and y coordinates
                G.j = current%10000;

                {
                    newnode(small, G.stack, G.i, G.j, G.z);
                    G.top++;
                }

                length = G.j * 20;
                breadth = G.i * 20;

                System.Threading.Thread.Sleep(100);
                
                if (current == 80001)
                {
                    MessageBox.Show("i have reached");
                    break;
                }
            }

            int i = 8, j = 1;
            for (int k = 0; k < 10000; k++)
            {
                if (small[i, j] == 0)
                {
                    break;
                }

                int val = small[i, j];
                i = val / 10000;
                j = val % 10000;
                int x = i * 20;
                int y = j * 20;

                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
            }
        }

        public int position(int x, int y)               //Position encoder
        {
            int result = 10000 * x + y;
            return result;
        }
       
        public void newnode(int[,] small, int[] stack, int i, int j, int z)
        {
            G.z = z;
          
            if (G.i != 0 && G.j != 0)                               //Check if you're in the (0, 0) position of the maze
            {
                if (small[i, j - 1] == 0)                           //Check if the square left is walkable
                {
                    int sign = 1;
                    int d = position(i, j - 1);

                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }
                    }

                    if (sign == 1)
                    {
                        stack[G.z++] = d;
                        small[i, j - 1] = position(i, j);
                    }
                }

                if (small[i, j + 1] == 0)                                  //Check if the square right is walkable
                {
                    int sign = 1;
                    int d = position(i, j + 1);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }
                    }

                    if (sign == 1)
                    {
                        stack[G.z++] = d;
                        small[i, j + 1] = position(i, j);
                    }
                }

                if (small[i - 1, j] == 0)                                   //Check if the square above is walkable
                {
                    int sign = 1;

                    int d = position(i - 1, j);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }
                    }

                    if (sign == 1)
                    {
                        stack[G.z++] = d;
                        small[i - 1, j] = position(i, j);
                    }
                }

                if (small[i + 1, j] == 0)                                   //Check if the square below is walkable
                {
                    int sign = 1;
                    int d = position(i + 1, j);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }
                    }

                    if (sign == 1)
                    {
                        stack[G.z++] = d;
                        small[i + 1, j] = position(i, j);
                    }
                }
            }
        }

        private void btnDFS_Click(object sender, EventArgs e)
        {
            var small = new int[10, 22] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };

            G.stack = new int[100000];
            for (G.i = 0; G.i < 100000; G.i++)
                G.stack[G.i] = 0;


            G.i = 3; G.j = 11;
            int z = 0;
            int value = position(G.i, G.j);

            G.stack[z] = value;
            G.top = 0; 
            int length, breadth;
            


            while (G.stack[G.top] != G.i)
            {
                int current = G.stack[G.z];
                small[3, 11] = value;


                G.i = current / 10000;
                G.j = current % 10000;
                {
                    int flag =newnode1(small, G.stack, G.i, G.j, G.z);
                    G.z--;
                }


                length = G.j * 20;
                breadth = G.i * 20;

                System.Threading.Thread.Sleep(100);
               
                if (current == 80001)
                {
                    MessageBox.Show("i have reached");
                    break;
                }

            }
            int i = 8, j = 1;
            for (int k = 0; k < 10000; k++)
            {
                if (small[i, j] == 0)
                {
                    break;
                }

                int val = small[i, j];
                i = val / 10000;
                j = val % 10000;
                int x = i * 20;
                int y = j * 20;

                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();




            }
        }

        public int newnode1(int[,] small, int[] stack, int i, int j, int z)
        {

            G.z = z;
            int flag = 0;

            if (G.i != 0 && G.j != 0)
            {
                if (small[i, j - 1] == 0)
                {
                    

                    int sign = 1;
                    int d = position(i, j - 1);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        flag = 1;
                        stack[G.z++] = d;
                        small[i, j - 1] = position(i, j);
                    }





                }
                if (small[i, j + 1] == 0)
                {
                    int sign = 1;
                    int d = position(i, j + 1);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        flag = 1;
                        stack[G.z++] = d;

                        small[i, j + 1] = position(i, j);
                    }
                }
                if (small[i - 1, j] == 0)
                {
                    int sign = 1;

                    int d = position(i - 1, j);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        flag = 1;
                        stack[G.z++] = d;
                        small[i - 1, j] = position(i, j);
                    }



                }

                if (small[i + 1, j] == 0)
                {
                    int sign = 1;
                    int d = position(i + 1, j);
                    for (int h = 0; h < z; h++)
                    {
                        if (G.stack[h] == d)
                        {
                            sign = 0;
                        }


                    }
                    if (sign == 1)
                    {
                        flag = 1;
                        stack[G.z++] = d;
                        small[i + 1, j] = position(i, j);
                    }

                }
                
            }

            return flag;
        }

        private void btnAStar_Click(object sender, EventArgs e)
        {
           
           
        }

        public void backTrackThePath(string structure, Node startFrom)                      //for backtracking through structure, starting from a particular node
        { 
            Node temp;
            int i;

            switch(structure)
            {
                case "closedSet":
                            temp = (Node)G.closed_set[startFrom.getPosition()];
                            Console.Write( "({0}, {1})", temp.getPosition()/10000, temp.getPosition()%10000);
                            
                            while(temp.getParent() != -1)
                            {
                                temp = (Node)G.closed_set[temp.getParent()];
                                Console.Write( "<--({0}, {1})", temp.getPosition()/10000, temp.getPosition()%10000);
                            }
                            break;

                case "openSet": 
                            G.open_set.printQueue();
                            break;

                case "closedSetMultiDot":                                           //for backtracking a multiDot maze
                            temp = (Node)closed_set[startFrom.getPosition() + ":" + String.Join(",", startFrom.getFood())];
                            Console.Write("({0}, {1}):[", temp.getPosition() / 10000, temp.getPosition() % 10000);
                            for (i = 0; i < temp.getFood().Count; i++)
                            { 
                                Console.Write("({0}, {1})", temp.getFood()[i] / 10000, temp.getFood()[i] % 10000);
                            }
                            Console.Write("]");

                                while (temp.getParent() != -1)
                                {
                                    temp = (Node)closed_set[temp.getParent() + ":" + String.Join(",", temp.getParentFood())];
                                    Console.Write("<--({0}, {1}):[", temp.getPosition() / 10000, temp.getPosition() % 10000);

                                    for (i = 0; i < temp.getFood().Count; i++)
                                    {
                                        Console.Write("({0}, {1})", temp.getFood()[i] / 10000, temp.getFood()[i] % 10000);
                                    }
                                    Console.Write("]");
                                }
                            break;
            }

        }

        private void btnMultiDotBFS_Click(object sender, EventArgs e)
        {
            //small = new int[,] {{1, 1, 1, 1, 1}, {1, 0, 1, 0, 1}, {1, 0, 1, 0, 1}, {1, 0, 0, 0, 1}, {1, 1, 1, 1, 1}};                        //toy_set multi-dot maze
            /*small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 0, 1}, 
                                {1, 0, 1, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 1, 0, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1}};           //tinySearch multi-dot maze
            */
            /*small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};           //smallSearch multi-dot maze
             */
            small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1}, 
                                {1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};              //trickySearch multi-dot maze
            
            //Node start = new Node(position(1, 3), 0, -1, -1, null, null);                   //creating the start node (toy_set)
            //List<int> foodList = new List<int>() { position(1, 1), position(3, 2) };        //Initial list of food positions (toy_set)
            //Node start = new Node(position(4, 4), 0, -1, -1, null, null);                     //creating the start node  (tinyMaze)
            //List<int> foodList = new List<int>() { position(1, 1), position(5, 1), position(7, 1), position(5, 3), position(7, 3), position(3, 4), position(5, 4), position(1, 5), position(1, 6), position(7, 6), position(3, 7), position(5, 7), position(7, 7) };        //Initial list of food positions (tinyMaze)
            //Node start = new Node(position(2, 9), 0, -1, -1, null, null);                     //creating the start node  (smallSearch)
            //List<int> foodList = new List<int>() { position(1, 1), position(1, 4), position(1, 5), position(1, 6), position(1, 11), position(1, 12), position(1, 13), position(1, 18), position(1, 19), position(2, 1), position(2, 3), position(2, 17), position(3, 1), position(3, 3), position(3, 5), position(3, 11), position(3, 13), position(3, 14), position(3, 18), position(3, 19) };        //Initial list of food positions (smallSearch)
            Node start = new Node(position(3, 9), 0, -1, -1, null, null);                     //creating the start node  (trickySearch)
            List<int> foodList = new List<int>() { position(1, 1), position(1, 13), position(1, 14), position(2, 1), position(2, 4), position(2, 7), position(2, 10), position(2, 13), position(5, 1), position(5, 2), position(5, 3), position(5, 4), position(5, 5) };        //Initial list of food positions (trickySearch)

            Console.WriteLine(foodList .Count + " food items to eat! Yay!\nHere are their locations:");
            foreach (int foodPosition  in foodList)
            {
                Console.WriteLine("(" + foodPosition/10000 + ", " + foodPosition%10000 + ")");
            }

            start.setFood(new List<int>(foodList));

            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not
            Node current = new Node();                                  //to keep track of current node


            //Initialize the frontier with the start node
            open_set_Q.Enqueue(start);


            while (open_set_Q.isEmpty() == false && G.expandedNodes <= 7000)    //loop until there are still some nodes in the frontier; and set the upper bound to 1000 as this is an uninformed search and we don't want to spend a lot of time on this
            {
                current = open_set_Q.Dequeue();                             //dequeue from the frontier

                if (!closed_set.Contains(current.getPosition() + ":" + String.Join(",", current.getFood())))
                {
                    closed_set.Add(current.getPosition() + ":" + String.Join(",", current.getFood()), current);           //transfer it to the explored_set; Here key is of type: 10003:10001,30002


                    if (current.getFood().Count == 0)                       //check if the current node is the goal state! (i.e. food list is empty for the current node)
                    {
                        solutionFlag = true;
                        break;
                    }

                    updateWalkableNodesMultiDotBFS(current, small);         //update the walkable nodes in the open_set, according to current
                }
                
                /*Console.WriteLine("\nOpen set: ");
                open_set_Q.printQueue();
                Console.WriteLine("Closed set:");

                foreach (DictionaryEntry de in closed_set)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                }

                Console.WriteLine("-----------------------------------------------------------");
                */
            }

            if (solutionFlag)
            {
                Console.WriteLine("Eureka!");

                foreach (DictionaryEntry de in closed_set)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                }

                backTrackThePath("closedSetMultiDot", current);
                Console.WriteLine("\nExpanded nodes: {0}", G.expandedNodes);

                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal) | food | parentFood");
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5} | {6} | {7}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score(), String.Join(",", current.getFood()), String.Join(",", current.getParentFood()));
            }
            else
            {
                if (G.expandedNodes > 7000)
                {
                    Console.WriteLine("Alas, path couldn't be found on expanding less than 7000 nodes.");
                } else
                {
                    Console.WriteLine("Alas, path couldn't be found.");
                }
            }

        }

        public void updateWalkableNodesMultiDotBFS(Node current, int[,] small)
        {
            int current_x = current.getPosition() / 10000;                      //find out the coordinates of current node
            int current_y = current.getPosition() % 10000;
            Node tentative;                                                  //to keep track of the next 


            if (small[current_x + 1, current_y] == 0)                       //check if the square below is walkable
            {
                tentative = new Node(position(current_x + 1, current_y), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    open_set_Q.Enqueue(tentative);                      //enqueue the node into the frontier
                }
            }

            if (small[current_x - 1, current_y] == 0)                       //check if the square above is walkable
            {
                tentative = new Node(position(current_x - 1, current_y), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    open_set_Q.Enqueue(tentative);                      //enqueue the node into the frontier
                }
            }

            if (small[current_x, current_y - 1] == 0)          //check if the square left is walkable
            {
                tentative = new Node(position(current_x, current_y - 1), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    open_set_Q.Enqueue(tentative);                      //enqueue the node into the frontier
                }
            }

            if (small[current_x, current_y + 1] == 0)          //check if the square right is walkable
            {
                tentative = new Node(position(current_x, current_y + 1), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    open_set_Q.Enqueue(tentative);                      //enqueue the node into the frontier
                }
            }
        }

        public void updateFoodList(Node node)                               //if the current position is present in the food list, please remove it as you've eaten it! (hungry fellow)
        {
            int index;
            index = node.getFood().IndexOf(node.getPosition());

            if (index != -1)
            {
                node.getFood().RemoveAt(index);
            }
        }

        private void btnMultiDotDFS_Click(object sender, EventArgs e)
        {
            //small = new int[,] {{1, 1, 1, 1, 1}, {1, 0, 1, 0, 1}, {1, 0, 1, 0, 1}, {1, 0, 0, 0, 1}, {1, 1, 1, 1, 1}};                        //toy_set multi-dot maze
            /*small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 0, 1}, 
                                {1, 0, 1, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 1, 0, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1}};           //tinySearch multi-dot maze
            */
            /*small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};           //smallSearch multi-dot maze
            */
            small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1}, 
                                {1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};              //trickySearch multi-dot maze
            
            //Node start = new Node(position(1, 3), 0, -1, -1, null, null);                   //creating the start node (toy_set)
            //List<int> foodList = new List<int>() { position(1, 1), position(3, 2) };        //Initial list of food positions (toy_set)
            //Node start = new Node(position(4, 4), 0, -1, -1, null, null);                     //creating the start node  (tinyMaze)
            //List<int> foodList = new List<int>() { position(1, 1), position(5, 1), position(7, 1), position(5, 3), position(7, 3), position(3, 4), position(5, 4), position(1, 5), position(1, 6), position(7, 6), position(3, 7), position(5, 7), position(7, 7) };        //Initial list of food positions (tinyMaze)
            //Node start = new Node(position(2, 9), 0, -1, -1, null, null);                     //creating the start node  (smallSearch)
            //List<int> foodList = new List<int>() { position(1, 1), position(1, 4), position(1, 5), position(1, 6), position(1, 11), position(1, 12), position(1, 13), position(1, 18), position(1, 19), position(2, 1), position(2, 3), position(2, 17), position(3, 1), position(3, 3), position(3, 5), position(3, 11), position(3, 13), position(3, 14), position(3, 18), position(3, 19) };        //Initial list of food positions (smallSearch)
            Node start = new Node(position(3, 9), 0, -1, -1, null, null);                     //creating the start node  (trickySearch)
            List<int> foodList = new List<int>() { position(1, 1), position(1, 13), position(1, 14), position(2, 1), position(2, 4), position(2, 7), position(2, 10), position(2, 13), position(5, 1), position(5, 2), position(5, 3), position(5, 4), position(5, 5) };        //Initial list of food positions (trickySearch)


            Console.WriteLine(foodList.Count + " food items to eat! Yay!\nHere are their locations:");
            foreach (int foodPosition in foodList)
            {
                Console.WriteLine("(" + foodPosition / 10000 + ", " + foodPosition % 10000 + ")");
            }

            start.setFood(new List<int>(foodList));

            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not
            Node current = new Node();                                  //to keep track of current node


            //Initialize the frontier with the start node
            open_set_S.Push(start);

            //Console.WriteLine(String.Join(",", foodList());                  //Output: 10001,30002

            while (open_set_S.isEmpty() == false)                           //loop until there are still some nodes in the frontier
            {
                current = open_set_S.Pop();                             //Pop from the frontier

                if (!closed_set.Contains(current.getPosition() + ":" + String.Join(",", current.getFood())))
                {
                    closed_set.Add(current.getPosition() + ":" + String.Join(",", current.getFood()), current);           //transfer it to the explored_set; Here key is of type: 10003:10001,30002


                    if (current.getFood().Count == 0)                       //check if the current node is the goal state! (i.e. food list is empty for the current node)
                    {
                        solutionFlag = true;
                        break;
                    }

                    updateWalkableNodesMultiDotDFS(current, small);                    //update the walkable nodes in the open_set, according to current

                }
                
                /*Console.WriteLine("\n Open set: ");
                open_set_S.printStack();
                Console.WriteLine("Closed set:");

                foreach (DictionaryEntry de in closed_set)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                }

                Console.WriteLine("-----------------------------------------------------------");
                */
            }

            if (solutionFlag)
            {
                Console.WriteLine("Eureka!");

                foreach (DictionaryEntry de in closed_set)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                }

                backTrackThePath("closedSetMultiDot", current);
                Console.WriteLine("\nExpanded nodes: {0}", G.expandedNodes);

                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal) | food | parentFood");
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5} | {6} | {7}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score(), String.Join(",", current.getFood()), String.Join(",", current.getParentFood()));
            }
            else
            {
                Console.WriteLine("Alas, path couldn't be found.");
            }
        }

        public void updateWalkableNodesMultiDotDFS(Node current, int[,] small)
        {
            int current_x = current.getPosition() / 10000;                      //find out the coordinates of current node
            int current_y = current.getPosition() % 10000;
            Node tentative;                                                  //to keep track of the next 


            if (small[current_x + 1, current_y] == 0)                       //check if the square below is walkable
            {
                tentative = new Node(position(current_x + 1, current_y), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    open_set_S.Push(tentative);                      //Push the node into the frontier
                }
            }

            if (small[current_x - 1, current_y] == 0)                       //check if the square above is walkable
            {
                tentative = new Node(position(current_x - 1, current_y), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    open_set_S.Push(tentative);                      //Push the node into the frontier
                }
            }

            if (small[current_x, current_y - 1] == 0)          //check if the square left is walkable
            {
                tentative = new Node(position(current_x, current_y - 1), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    open_set_S.Push(tentative);                      //Push the node into the frontier
                }
            }

            if (small[current_x, current_y + 1] == 0)          //check if the square right is walkable
            {
                tentative = new Node(position(current_x, current_y + 1), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    open_set_S.Push(tentative);                      //Push the node into the frontier
                }
            }
        }

        private void btnMultiDotGreedy_Click(object sender, EventArgs e)
        {
            //small = new int[,] {{1, 1, 1, 1, 1}, {1, 0, 1, 0, 1}, {1, 0, 1, 0, 1}, {1, 0, 0, 0, 1}, {1, 1, 1, 1, 1}};                        //toy_set multi-dot maze
            /*small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 0, 1}, 
                                {1, 0, 1, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 1, 0, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1}};           //tinySearch multi-dot maze
            */
            /*small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};           //smallSearch multi-dot maze
            */
            small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1}, 
                                {1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};              //trickySearch multi-dot maze
            
            //Node start = new Node(position(1, 3), 0, -1, -1, null, null);                   //creating the start node (toy_set)
            //List<int> foodList = new List<int>() { position(1, 1), position(3, 2) };        //Initial list of food positions (toy_set)
            //Node start = new Node(position(4, 4), 0, -1, -1, null, null);                     //creating the start node  (tinyMaze)
            //List<int> foodList = new List<int>() { position(1, 1), position(5, 1), position(7, 1), position(5, 3), position(7, 3), position(3, 4), position(5, 4), position(1, 5), position(1, 6), position(7, 6), position(3, 7), position(5, 7), position(7, 7) };        //Initial list of food positions (tinyMaze)
            //Node start = new Node(position(2, 9), 0, -1, -1, null, null);                     //creating the start node  (smallSearch)
            //List<int> foodList = new List<int>() { position(1, 1), position(1, 4), position(1, 5), position(1, 6), position(1, 11), position(1, 12), position(1, 13), position(1, 18), position(1, 19), position(2, 1), position(2, 3), position(2, 17), position(3, 1), position(3, 3), position(3, 5), position(3, 11), position(3, 13), position(3, 14), position(3, 18), position(3, 19) };        //Initial list of food positions (smallSearch)
            Node start = new Node(position(3, 9), 0, -1, -1, null, null);                     //creating the start node  (trickySearch)
            List<int> foodList = new List<int>() { position(1, 1), position(1, 13), position(1, 14), position(2, 1), position(2, 4), position(2, 7), position(2, 10), position(2, 13), position(5, 1), position(5, 2), position(5, 3), position(5, 4), position(5, 5) };        //Initial list of food positions (trickySearch)

            Console.WriteLine(foodList.Count + " food items to eat! Yay!\nHere are their locations:");
            foreach (int foodPosition in foodList)
            {
                Console.WriteLine("(" + foodPosition / 10000 + ", " + foodPosition % 10000 + ")");
            }

            start.setFood(new List<int>(foodList));                     //Initialize the food list for start node

            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not
            Node current = new Node();                                  //to keep track of current node


            //Initialize the frontier with the start node
            G.open_set.Enqueue (start);

            while (G.open_set.isEmpty() == false)                           //loop until there are still some nodes in the frontier
            {
                current = G.open_set.Dequeue();                             //Dequeue from the frontier

                if (!closed_set.Contains(current.getPosition() + ":" + String.Join(",", current.getFood())))
                {
                    closed_set.Add(current.getPosition() + ":" + String.Join(",", current.getFood()), current);           //transfer it to the explored_set; Here key is of type: 10003:10001,30002


                    if (current.getFood().Count == 0)                       //check if the current node is the goal state! (i.e. food list is empty for the current node)
                    {
                        solutionFlag = true;
                        break;
                    }

                    updateWalkableNodesMultiDotGreedy(current, small);                    //update the walkable nodes in the open_set, according to current
                }

                /*Console.WriteLine("\n Open set: ");
                G.open_set.printQueueComplex();
                Console.WriteLine("Closed set:");

                foreach (DictionaryEntry de in closed_set)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                }

                Console.WriteLine("-----------------------------------------------------------");
                */
            }

            if (solutionFlag)
            {
                Console.WriteLine("Eureka!");

                foreach (DictionaryEntry de in closed_set)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                }

                backTrackThePath("closedSetMultiDot", current);
                Console.WriteLine("\nExpanded nodes: {0}", G.expandedNodes);

                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal) | food | parentFood");
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5} | {6} | {7}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score(), String.Join(",", current.getFood()), String.Join(",", current.getParentFood()));
            }
            else
            {
                Console.WriteLine("Alas, path couldn't be found.");
            }
        }

        public void updateWalkableNodesMultiDotGreedy(Node current, int[,] small)
        {
            int current_x = current.getPosition() / 10000;                      //find out the coordinates of current node
            int current_y = current.getPosition() % 10000;
            int index;
            Node tentative;                                                     //to keep track of the next 


            if (small[current_x + 1, current_y] == 0)                   //check if the square below is walkable
            {
                tentative = new Node(position(current_x + 1, current_y), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    updateFScore(tentative, "greedy");                                  //Update f_score of the tentative node

                    index = G.open_set.indexOfComplex(tentative);                       //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x - 1, current_y] == 0)                   //check if the square above is walkable
            {
                tentative = new Node(position(current_x - 1, current_y), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    updateFScore(tentative, "greedy");                                  //Update f_score of the tentative node

                    index = G.open_set.indexOfComplex(tentative);                       //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }                            //Enqueue the node into the frontier
                }
            }

            if (small[current_x, current_y - 1] == 0)                   //check if the square left is walkable
            {
                tentative = new Node(position(current_x, current_y - 1), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    updateFScore(tentative, "greedy");                                  //Update f_score of the tentative node

                    index = G.open_set.indexOfComplex(tentative);                       //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x, current_y + 1] == 0)                   //check if the square right is walkable
            {
                tentative = new Node(position(current_x, current_y + 1), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    updateFScore(tentative, "greedy");                                  //Update f_score of the tentative node

                    index = G.open_set.indexOfComplex(tentative);                       //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }
        }

        public void updateFScore(Node node, string algo)                //for the updating the f_scores in case of multiDot mazes
        {
            int current_x, current_y;
            int h_score;

            current_x = node.getPosition() / 10000;
            current_y = node.getPosition() % 10000;

            h_score = 50 * node.getFood().Count;                    //As I want to give higher weightage (or lower priority) to the number of food items left to eat
            foreach (int item in node.getFood())
            {
                h_score += Math.Abs((item / 10000 - current_x)) + Math.Abs((item % 10000 - current_y));
            }

            switch (algo)
            { 
                case "greedy":
                            node.setF_score(h_score);                               //since f(n) = h(n)
                            break;

                case "aStar":
                            node.setF_score(node.getG_score() + h_score);           //since f(n) = g(n) + h(n)
                            break;
            }
        }

        private void btnMultiDotAStar_Click(object sender, EventArgs e)
        {
            //small = new int[,] {{1, 1, 1, 1, 1}, {1, 0, 1, 0, 1}, {1, 0, 1, 0, 1}, {1, 0, 0, 0, 1}, {1, 1, 1, 1, 1}};                        //toy_set multi-dot maze
            /*small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 0, 1}, 
                                {1, 0, 1, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 1, 0, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1}};           //tinySearch multi-dot maze
            */
            /*small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};           //smallSearch multi-dot maze
            */
            small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1}, 
                                {1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};              //trickySearch multi-dot maze
            
            //Node start = new Node(position(1, 3), 0, -1, -1, null, null);                   //creating the start node (toy_set)
            //List<int> foodList = new List<int>() { position(1, 1), position(3, 2) };        //Initial list of food positions (toy_set)
            //Node start = new Node(position(4, 4), 0, -1, -1, null, null);                     //creating the start node  (tinyMaze)
            //List<int> foodList = new List<int>() { position(1, 1), position(5, 1), position(7, 1), position(5, 3), position(7, 3), position(3, 4), position(5, 4), position(1, 5), position(1, 6), position(7, 6), position(3, 7), position(5, 7), position(7, 7) };        //Initial list of food positions (tinyMaze)
            //Node start = new Node(position(2, 9), 0, -1, -1, null, null);                     //creating the start node  (smallSearch)
            //List<int> foodList = new List<int>() { position(1, 1), position(1, 4), position(1, 5), position(1, 6), position(1, 11), position(1, 12), position(1, 13), position(1, 18), position(1, 19), position(2, 1), position(2, 3), position(2, 17), position(3, 1), position(3, 3), position(3, 5), position(3, 11), position(3, 13), position(3, 14), position(3, 18), position(3, 19) };        //Initial list of food positions (smallSearch)
            Node start = new Node(position(3, 9), 0, -1, -1, null, null);                     //creating the start node  (trickySearch)
            List<int> foodList = new List<int>() { position(1, 1), position(1, 13), position(1, 14), position(2, 1), position(2, 4), position(2, 7), position(2, 10), position(2, 13), position(5, 1), position(5, 2), position(5, 3), position(5, 4), position(5, 5) };        //Initial list of food positions (trickySearch)

            Console.WriteLine(foodList.Count + " food items to eat! Yay!\nHere are their locations:");
            foreach (int foodPosition in foodList)
            {
                Console.WriteLine("(" + foodPosition / 10000 + ", " + foodPosition % 10000 + ")");
            }

            start.setFood(new List<int>(foodList));                     //Initialize the food list for start node

            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not
            Node current = new Node();                                  //to keep track of current node


            //Initialize the frontier with the start node
            G.open_set.Enqueue(start);

            while (G.open_set.isEmpty() == false)                           //loop until there are still some nodes in the frontier
            {
                current = G.open_set.Dequeue();                             //Dequeue from the frontier

                if (!closed_set.Contains(current.getPosition() + ":" + String.Join(",", current.getFood())))
                {
                    closed_set.Add(current.getPosition() + ":" + String.Join(",", current.getFood()), current);           //transfer it to the explored_set; Here key is of type: 10003:10001,30002

                    if (current.getFood().Count == 0)                       //check if the current node is the goal state! (i.e. food list is empty for the current node)
                    {
                        solutionFlag = true;
                        break;
                    }

                    updateWalkableNodesMultiDotAStar(current, small);                    //update the walkable nodes in the open_set, according to current

                }
                /*
                Console.WriteLine("\n Open set: ");
                G.open_set.printQueueComplex();
                Console.WriteLine("Closed set:");

                foreach (DictionaryEntry de in closed_set)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                }

                Console.WriteLine("-----------------------------------------------------------");
                */
            }

            if (solutionFlag)
            {
                Console.WriteLine("Eureka!");

                foreach (DictionaryEntry de in closed_set)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                }

                backTrackThePath("closedSetMultiDot", current);

                Console.WriteLine("\nExpanded nodes: {0}", G.expandedNodes);

                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal) | food | parentFood");
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5} | {6} | {7}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score(), String.Join(",", current.getFood()), String.Join(",", current.getParentFood()));
            }
            else
            {
                Console.WriteLine("Alas, path couldn't be found.");
            }
        }

        public void updateWalkableNodesMultiDotAStar(Node current, int[,] small)
        {
            int current_x = current.getPosition() / 10000;                      //find out the coordinates of current node
            int current_y = current.getPosition() % 10000;
            int index;
            Node tentative;                                                     //to keep track of the next 


            if (small[current_x + 1, current_y] == 0)                   //check if the square below is walkable
            {
                tentative = new Node(position(current_x + 1, current_y), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    updateFScore(tentative, "aStar");                                  //Update f_score of the tentative node

                    index = G.open_set.indexOfComplex(tentative);                       //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x - 1, current_y] == 0)                   //check if the square above is walkable
            {
                tentative = new Node(position(current_x - 1, current_y), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    updateFScore(tentative, "aStar");                                  //Update f_score of the tentative node

                    index = G.open_set.indexOfComplex(tentative);                       //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }                            //Enqueue the node into the frontier
                }
            }

            if (small[current_x, current_y - 1] == 0)                   //check if the square left is walkable
            {
                tentative = new Node(position(current_x, current_y - 1), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    updateFScore(tentative, "aStar");                                  //Update f_score of the tentative node

                    index = G.open_set.indexOfComplex(tentative);                       //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x, current_y + 1] == 0)                   //check if the square right is walkable
            {
                tentative = new Node(position(current_x, current_y + 1), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getFood()), new List<int>((current.getFood())));            //its food list is not updated, will update it in the next step
                updateFoodList(tentative);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getFood())))          //if the node hasn't been explored yet
                {
                    updateFScore(tentative, "aStar");                                  //Update f_score of the tentative node

                    index = G.open_set.indexOfComplex(tentative);                       //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }
        }

        private void btnBugtrap_Click(object sender, EventArgs e)
        {
            small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};          //bugTrap
            

            Node start = new Node(position(6, 29), 0, -1, -1);          //creating the start node
            Node goal = new Node(position(8, 2), -1, -1, -1);           //creating the goal node

            int goal_x = goal.getPosition() / 10000;                      //x coordinate of Goal
            int goal_y = goal.getPosition() % 10000;                      //y coordinate of Goal
            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not

            Node current = new Node();                                  //to keep track of current node


            //Initialize the frontier with the start node
            G.open_set.Enqueue(start);

            while (G.open_set.isEmpty() == false)                           //loop until there are still some nodes in the frontier
            {
                current = G.open_set.Dequeue();                             //dequeue the lowest f(n) node from the frontier

                if (!G.closed_set.Contains(current.getPosition()))
                {
                    G.closed_set.Add(current.getPosition(), current);           //transfer it to the explored_set

                    if (current.getPosition() - goal.getPosition() == 0)       //check if the current node is the goal state!
                    {
                        solutionFlag = true;
                        break;
                    }

                    updateWalkableNodesBugTrap(current, goal, small);                    //update the walkable nodes in the open_set, according to current

                }
            }
            /*
            Console.WriteLine("\n Open set: ");
            G.open_set.printQueue();
            Console.WriteLine("Closed set:");

            foreach (DictionaryEntry de in G.closed_set)
            {
                Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
            }

            Console.WriteLine("-----------------------------------------------------------");
            */
            if (solutionFlag)
            {
                Console.WriteLine("Eureka!");
                backTrackThePath("closedSet", current);

                Console.WriteLine("\nNodes expanded: {0}", G.expandedNodes);

                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal)", current.getG_score());
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score());
            }
            else
            {
                Console.WriteLine("Alas, path couldn't be found.");
            }
        }

        public void updateWalkableNodesBugTrap(Node current, Node goal, int[,] small)
        {
            int current_x = current.getPosition() / 10000;                      //find out the coordinates of current node
            int current_y = current.getPosition() % 10000;
            int index;
            Node tentative;                                                     //to keep track of the next 


            if (small[current_x + 1, current_y] == 0)                   //check if the square below is walkable
            {
                tentative = new Node(position(current_x + 1, current_y), current.getG_score() + 1, current.getPosition(), -1);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition()))                      //if the node hasn't been explored yet
                {
                    updateFScoreSingleDot(tentative, goal, small, "bugTrap");           //Update f_score of the tentative node

                    index = G.open_set.indexOf(tentative.getPosition());                //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x - 1, current_y] == 0)                   //check if the square above is walkable
            {
                tentative = new Node(position(current_x - 1, current_y), current.getG_score() + 1, current.getPosition(), -1);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition()))                       //if the node hasn't been explored yet
                {
                    updateFScoreSingleDot(tentative, goal, small, "bugTrap");           //Update f_score of the tentative node

                    index = G.open_set.indexOf(tentative.getPosition());                 //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                     //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x, current_y - 1] == 0)                   //check if the square left is walkable
            {
                tentative = new Node(position(current_x, current_y - 1), current.getG_score() + 1, current.getPosition(), -1);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition()))                       //if the node hasn't been explored yet
                {
                    updateFScoreSingleDot(tentative, goal, small, "bugTrap");           //Update f_score of the tentative node

                    index = G.open_set.indexOf(tentative.getPosition());                 //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                     //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x, current_y + 1] == 0)                   //check if the square right is walkable
            {
                tentative = new Node(position(current_x, current_y + 1), current.getG_score() + 1, current.getPosition(), -1);
                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition()))                       //if the node hasn't been explored yet
                {
                    updateFScoreSingleDot(tentative, goal, small, "bugTrap");           //Update f_score of the tentative node

                    index = G.open_set.indexOf(tentative.getPosition());                 //check if this nodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                     //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }
        }

        public void updateFScoreSingleDot(Node node, Node goal, int[,] small, string algo)                //for the updating the f_scores in case of multiDot mazes
        {
            int current_x, current_y;
            int x_min, x_max, y_min, y_max;
            int h_score;

            current_x = node.getPosition() / 10000;
            current_y = node.getPosition() % 10000;
            x_min = 0; y_min = 0;
            x_max = small.GetLength(0) - 1;                         //get the max index for the rows in the maze
            y_max = small.GetLength(1) - 1;                         //get the max index for the columns in the maze


            h_score = 10 * Math.Min(Math.Abs(current_x - x_min), Math.Min(Math.Abs(current_x - x_max), Math.Min(Math.Abs(current_y - y_min), Math.Abs(current_y - y_max))));                     //As I want to give higher weightage (or lower priority) to the closeness of a node to the walls
            h_score += 2 * Math.Abs(current_x - goal.getPosition() / 10000) + Math.Abs(current_y - goal.getPosition() % 10000);                                                                       //Plus, I want to take into account the distance from the goal as well :)

            switch (algo)
            {
                case "bugTrap":
                    node.setF_score(node.getG_score() + h_score);                               //since f(n) = g(n) + h(n)
                    break;
            }
        }

        private void btnRandomizedBugtrap_Click(object sender, EventArgs e)
        {
            small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};          //bugTrap


            heuristic = new int[small.GetLength(0), small.GetLength(1)];

            Node start;                                                 //creating the start node
            Node goal = new Node(position(8, 2), -1, -1, -1);           //creating the goal node

            int goal_x = goal.getPosition() / 10000;                    //x coordinate of Goal
            int goal_y = goal.getPosition() % 10000;                    //y coordinate of Goal
            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not


            for (int i = 0; i < small.GetLength(0); i++)                    //Populating the heuristics using Manhattan distance from Goal
            {
                for (int j = 0; j < small.GetLength(1); j++)
                {
                    heuristic[i, j] = Math.Abs(i - goal_x) + Math.Abs(j - goal_y);
                }
            }

            int x_min = 0;
            int y_min = 0;
            int x_max = small.GetLength(0);
            int y_max = small.GetLength(1);
            int tentative_x, tentative_y;
            int sumCost = 0, sumExpandedNodes = 0;

            Random rand_x = new Random();                               //for calculating random x-index in maze
            Random rand_y = new Random();                               //for calculating random y-index in maze            

            Node current = new Node();                                  //to keep track of current node


            for(int i = 0; i<1000; i++)
            {
                tentative_x = rand_x.Next(x_min, x_max);
                tentative_y = rand_y.Next(y_min, y_max);

                if (small[tentative_x, tentative_y] == 1)               //if it's a wall, please go find another node, but don't increment the counter this time
                {
                    i--;
                }
                else {
                    Console.WriteLine("Run #{0}::", i+1);
                    Console.WriteLine("Starting at: ({0}, {1})", tentative_x, tentative_y);              //generates a random number between [a, b)
                    start = new Node(position(tentative_x, tentative_y), 0, -1, -1);                     //creating the start node
                    start.setF_score(start.getG_score() + getHeuristicMeasure(start.getPosition()));     //Populate f(n) for start node; where f(n) = g(n) + h(n)

                    //Clear the frontier, the closed_set, and the nodes expanded counter
                    G.open_set = new PriorityQueue();
                    G.closed_set = new Hashtable();
                    G.expandedNodes = 1;

                    //Initialize the frontier with the start node
                    G.open_set.Enqueue(start);

                    while (G.open_set.isEmpty() == false)                           //loop until there are still some nodes in the frontier
                    {
                        current = G.open_set.Dequeue();                             //dequeue the lowest f(n) node from the frontier
                        G.closed_set.Add(current.getPosition(), current);           //transfer it to the explored_set

                        if (getHeuristicMeasure(current.getPosition()) == 0)       //check if the current node is the goal state!
                        {
                            solutionFlag = true;
                            break;
                        }

                        updateWalkableNodes(current, small);                    //update the walkable nodes in the open_set, according to current
                    }

                    if (solutionFlag)
                    {
                        Console.WriteLine("Eureka!");
                        backTrackThePath("closedSet", current);

                        Console.WriteLine("\nNodes expanded: {0}", G.expandedNodes);

                        Console.WriteLine("Coordinates | g(Goal) | parent | f(Goal)", current.getG_score());
                        Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score());

                        sumCost += current.getG_score();                        //Add the path cost to the tracker, to calculate the average later on
                        sumExpandedNodes += G.expandedNodes;                    //Add the # of expanded nodes to the tracker, to calculate the average later on
                    }
                    else
                    {
                        Console.WriteLine("Alas, path couldn't be found.");
                    }
                    Console.WriteLine("-----------------------------------------------------------");
                }                
            }

            //Report the average results
            Console.WriteLine("Average path cost: {0}", sumCost/1000);
            Console.WriteLine("Average # of expanded nodes: {0}", sumExpandedNodes/1000);
        }

        private void btnGreedy_Click(object sender, EventArgs e)
        {
            //small = new int[,] {{ 1, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1 }};          //toy_set
            /*small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1}, 
                                {1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1}, 
                                {1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1}, 
                                {1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1}, 
                                {1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1}, 
                                {1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1}, 
                                {1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};     //smallMaze
            
            small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                {1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1}, 
                                {1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1}, 
                                {1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1}, 
                                {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};     //mediumMaze
            */
            /*small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1},
                                {1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1},
                                {1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                {1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1},
                                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
                                {1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1},
                                {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                                {1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                                {1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                                {1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1},
                                {1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                                {1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1},
                                {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1},
                                {1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1},
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                                {1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1},
                                {1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1},
                                {1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1},
                                {1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1},
                                {1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1},
                                {1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1},
                                {1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1},
                                {1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                {1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1},
                                {1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                                {1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1},
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1},
                                {1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1},
                                {1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                                {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
                                {1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1},
                                {1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1},
                                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1},
                                {1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1},
                                {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                                {1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1},
                                {1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1},
                                {1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1},
                                {1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1},
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};           //largeMaze
            */
            small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};          //bugTrap

            heuristic = new int[small.GetLength(0), small.GetLength(1)];

            Node start = new Node(position(6, 29), 0, -1, -1);          //creating the start node
            Node goal = new Node(position(8, 2), -1, -1, -1);           //creating the goal node

            int goal_x = goal.getPosition() / 10000;                      //x coordinate of Goal
            int goal_y = goal.getPosition() % 10000;                      //y coordinate of Goal
            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not


            for (int i = 0; i < small.GetLength(0); i++)                    //Populating the heuristics using Manhattan distance from Goal
            {
                for (int j = 0; j < small.GetLength(1); j++)
                {
                    heuristic[i, j] = Math.Abs(i - goal_x) + Math.Abs(j - goal_y);
                }
            }

            start.setF_score(getHeuristicMeasure(start.getPosition()));  //Populate f(n) for start node; where f(n) = h(n)

            Node current = new Node();                                  //to keep track of current node


            //Initialize the frontier with the start node
            G.open_set.Enqueue(start);

            while (G.open_set.isEmpty() == false)                           //loop until there are still some nodes in the frontier
            {
                current = G.open_set.Dequeue();                             //dequeue the lowest f(n) node from the frontier
                G.closed_set.Add(current.getPosition(), current);           //transfer it to the explored_set

                if (getHeuristicMeasure(current.getPosition()) == 0)       //check if the current node is the goal state!
                {
                    solutionFlag = true;
                    break;
                }

                updateWalkableNodesGreedy(current, small);                    //update the walkable nodes in the open_set, according to current
            }

            Console.WriteLine("\n Open set: ");
            G.open_set.printQueue();
            Console.WriteLine("Closed set:");

            foreach (DictionaryEntry de in G.closed_set)
            {
                Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
            }

            Console.WriteLine("-----------------------------------------------------------");

            if (solutionFlag)
            {
                Console.WriteLine("Eureka!");
                backTrackThePath("closedSet", current);

                Console.WriteLine("\nNodes expanded: {0}", G.expandedNodes);

                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal)", current.getG_score());
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score());
            }
            else
            {
                Console.WriteLine("Alas, path couldn't be found.");
            }
        }

        public void updateWalkableNodesGreedy(Node current, int[,] small)
        {
            int current_x = current.getPosition() / 10000;                      //find out the coordinates of current node
            int current_y = current.getPosition() % 10000;
            Node tentative;                                                  //to keep track of the next 
            int index;                                                       //to get index of tentative from open_set

            if (small[current_x - 1, current_y] == 0)             //check if the square above is walkable
            {
                if (!G.closed_set.Contains(position(current_x - 1, current_y)))          //if the node hasn't been explored yet
                {
                    tentative = new Node(position(current_x - 1, current_y), current.getG_score() + 1, current.getPosition(), getHeuristicMeasure(position(current_x - 1, current_y)));
                    G.expandedNodes++;

                    index = G.open_set.indexOf(tentative.getPosition());

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x + 1, current_y] == 0)         //check if the square below is walkable
            {
                if (!G.closed_set.Contains(position(current_x + 1, current_y)))          //if the node hasn't been explored yet
                {
                    tentative = new Node(position(current_x + 1, current_y), current.getG_score() + 1, current.getPosition(), getHeuristicMeasure(position(current_x + 1, current_y)));
                    G.expandedNodes++;

                    index = G.open_set.indexOf(tentative.getPosition());

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x, current_y - 1] == 0)          //check if the square left is walkable
            {
                if (!G.closed_set.Contains(position(current_x, current_y - 1)))          //if the node hasn't been explored yet
                {
                    tentative = new Node(position(current_x, current_y - 1), current.getG_score() + 1, current.getPosition(), getHeuristicMeasure(position(current_x, current_y - 1)));
                    G.expandedNodes++;

                    index = G.open_set.indexOf(tentative.getPosition());

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }

            }

            if (small[current_x, current_y + 1] == 0)          //check if the square right is walkable
            {
                if (!G.closed_set.Contains(position(current_x, current_y + 1)))          //if the node hasn't been explored yet
                {
                    tentative = new Node(position(current_x, current_y + 1), current.getG_score() + 1, current.getPosition(), getHeuristicMeasure(position(current_x, current_y + 1)));
                    G.expandedNodes++;

                    index = G.open_set.indexOf(tentative.getPosition());

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};          //bugTrap

            heuristic = new int[small.GetLength(0), small.GetLength(1)];

            //Node start = new Node(position(1, 4), 0, -1, -1);           //creating the start node (toy_set)
            //Node goal = new Node(position(1, 1), -1, -1, -1);           //creating the goal node (toy_set)
            //Node start = new Node(position(9, 1), 0, -1, -1);             //creating the start node (smallMaze)
            //Node goal = new Node(position(1, 21), -1, -1, -1);            //creating the goal node (smallMaze)
            Node start = new Node(position(6, 29), 0, -1, -1);          //creating the start node (bugTrap)
            Node goal = new Node(position(8, 2), -1, -1, -1);           //creating the goal node (bugTrap)

            int goal_x = goal.getPosition() / 10000;                      //x coordinate of Goal
            int goal_y = goal.getPosition() % 10000;                      //y coordinate of Goal
            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not


            for (int i = 0; i < small.GetLength(0); i++)                    //Populating the heuristics using Manhattan distance from Goal
            {
                for (int j = 0; j < small.GetLength(1); j++)
                {
                    heuristic[i, j] = Math.Abs(i - goal_x) + Math.Abs(j - goal_y);
                }
            }

            start.setF_score(start.getG_score() + getHeuristicMeasure(start.getPosition()));  //Populate f(n) for start node; where f(n) = g(n) + h(n)

            Node current = new Node();                                  //to keep track of current node


            //Initialize the frontier with the start node
            G.open_set.Enqueue(start);

            while (G.open_set.isEmpty() == false)                           //loop until there are still some nodes in the frontier
            {
                current = G.open_set.Dequeue();                             //dequeue the lowest f(n) node from the frontier
                G.closed_set.Add(current.getPosition(), current);           //transfer it to the explored_set

                if (getHeuristicMeasure(current.getPosition()) == 0)       //check if the current node is the goal state!
                {
                    solutionFlag = true;
                    break;
                }

                updateWalkableNodes(current, small);                    //update the walkable nodes in the open_set, according to current
            }

            Console.WriteLine("\n Open set: ");
            G.open_set.printQueue();
            Console.WriteLine("Closed set:");

            foreach (DictionaryEntry de in G.closed_set)
            {
                Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
            }

            Console.WriteLine("-----------------------------------------------------------");

            if (solutionFlag)
            {
                Console.WriteLine("Eureka!");
                backTrackThePath("closedSet", current);

                Console.WriteLine("\nNodes expanded: {0}", G.expandedNodes);

                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal)", current.getG_score());
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score());
            }
            else
            {
                Console.WriteLine("Alas, path couldn't be found.");
            }


            //testing the heuristics
            /*Console.WriteLine("\nheuristic[{0}, {1}]", small.GetLength(0), small.GetLength(1));
            for (int i = 0; i < small.GetLength(0); i++)            
            {
                for (int j = 0; j < small.GetLength(1); j++)
                {
                    Console.Write(heuristic[i, j] + "\t");
                }
                Console.WriteLine();
            }*/
        }

        public int getHeuristicMeasure(int position)                           //looks-up the heuristic measure of a position
        {
            return heuristic[position / 10000, position % 10000];
        }

        public void updateWalkableNodes(Node current, int[,] small)
        {
            int current_x = current.getPosition() / 10000;                      //find out the coordinates of current node
            int current_y = current.getPosition() % 10000;
            Node tentative;                                                  //to keep track of the next 
            int index;                                                       //to get index of tentative from open_set

            if (small[current_x - 1, current_y] == 0)             //check if the square above is walkable
            {
                if (!G.closed_set.Contains(position(current_x - 1, current_y)))          //if the node hasn't been explored yet
                {
                    tentative = new Node(position(current_x - 1, current_y), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x - 1, current_y)));
                    G.expandedNodes++;

                    index = G.open_set.indexOf(tentative.getPosition());

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x + 1, current_y] == 0)         //check if the square below is walkable
            {
                if (!G.closed_set.Contains(position(current_x + 1, current_y)))          //if the node hasn't been explored yet
                {
                    tentative = new Node(position(current_x + 1, current_y), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x + 1, current_y)));
                    G.expandedNodes++;

                    index = G.open_set.indexOf(tentative.getPosition());

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x, current_y - 1] == 0)          //check if the square left is walkable
            {
                if (!G.closed_set.Contains(position(current_x, current_y - 1)))          //if the node hasn't been explored yet
                {
                    tentative = new Node(position(current_x, current_y - 1), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x, current_y - 1)));
                    G.expandedNodes++;

                    index = G.open_set.indexOf(tentative.getPosition());

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }

            }

            if (small[current_x, current_y + 1] == 0)          //check if the square right is walkable
            {
                if (!G.closed_set.Contains(position(current_x, current_y + 1)))          //if the node hasn't been explored yet
                {
                    tentative = new Node(position(current_x, current_y + 1), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x, current_y + 1)));
                    G.expandedNodes++;

                    index = G.open_set.indexOf(tentative.getPosition());

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set.getQueue()[index].getF_score())
                        {
                            G.open_set.removeAt(index);
                            G.open_set.Enqueue(tentative);
                        }
                    }
                }

            }
        }

        private void btn8Puzzle_Click(object sender, EventArgs e)
        {
            small = new int[,] {{-1, -1, -1, -1, -1}, 
                                {-1, 1, 4, 2, -1}, 
                                {-1, 3, 5, 0, -1}, 
                                {-1, 6, 7, 8, -1}, 
                                {-1, -1, -1, -1, -1}};             //8 puzzle

            TileNode start = new TileNode(position(2, 3), 0, -1, -1, null, null);                     //creating the start state (8 puzzle)
            TileNode goal = new TileNode(position(1, 1), 0, -1, -1, null, null);                      //creating the goal state (8 puzzle)
            List<int> startTileList = new List<int>() { 1, 4, 2, 3, 5, 0, 6, 7, 8 };                 //Initial list of tile positions (8 puzzle)
            List<int> goalTileList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };                   //Goal list of tile positions (8 puzzle)

            start.setTileList(new List<int>(startTileList));                     //Initialize the tileList for start node
            goal.setTileList(new List<int>(goalTileList));                       //Initialize the tileList for goal node

            bool solutionFlag = false;                                      //to keep track if solution to the maze has been found or not
            TileNode current = new TileNode();                              //to keep track of current node


            //Initialize the frontier with the start node
            G.open_set_8.Enqueue(start);


            while (G.open_set_8.isEmpty() == false)                           //loop until there are still some nodes in the frontier
            {
                current = G.open_set_8.Dequeue();                             //Dequeue from the frontier

                if (!closed_set.Contains(current.getPosition() + ":" + String.Join(",", current.getTileList())))
                {
                    closed_set.Add(current.getPosition() + ":" + String.Join(",", current.getTileList()), current);           //transfer it to the explored_set; Here key is of type: 10003:1,2,3,4,5,7,0,8

                    if (String.Join(",", current.getTileList()).Equals(String.Join(",", goal.getTileList())))                       //check if the current node is the goal state! (i.e. the tileList is same as that for the goal state)
                    {
                        solutionFlag = true;
                        break;
                    }

                    updateWalkableNodesMultiDotAStar8Puzzle(current, small, goal);                    //update the walkable nodes in the open_set_8, according to current
                }
                
                Console.WriteLine("\nOpen set: ");
                G.open_set_8.printQueueComplex();
                Console.WriteLine("Closed set:");

                foreach (DictionaryEntry de in closed_set)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                }

                Console.WriteLine("-----------------------------------------------------------");
                
            }

            if (solutionFlag)
            {
                Console.WriteLine("Eureka!");

                foreach (DictionaryEntry de in closed_set)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                }

                backTrackThePathPuzzle(current, small);

                Console.WriteLine("\nExpanded nodes: {0}", G.expandedNodes);

                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal) | food | parentFood");
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5} | {6} | {7}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score(), String.Join(",", current.getTileList()), String.Join(",", current.getParentTileList()));
            }
            else
            {
                Console.WriteLine("Alas, path couldn't be found.");
            }
        }

        public void updateWalkableNodesMultiDotAStar8Puzzle(TileNode current, int[,] small, TileNode goal)
        {
            int current_x = current.getPosition() / 10000;                      //find out the coordinates of current node
            int current_y = current.getPosition() % 10000;
            int index;
            int index1, index2;                                                 //to keep the index of blank tile, and the tile to be swapped
            TileNode tentative;                                                 //to keep track of the next 


            if (small[current_x + 1, current_y] != -1)                          //check if the square below is walkable
            {
                tentative = new TileNode(position(current_x + 1, current_y), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getTileList()), new List<int>((current.getTileList())));            //its tile list is not updated, will update it in the next step
                index1 = tentative.getTileList().IndexOf(0);
                index2 = index1 + (small.GetLength(1) - 2);                     //or, i + (colLength - 2)

                updateTileList(tentative, index1, index2);

                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getTileList())))          //if the TileNode hasn't been explored yet
                {
                    updateFScorePuzzle(current, goal, "aStar");                         //Update f_score of the tentative TileNode

                    index = G.open_set_8.indexOfComplex(tentative);                     //check if this TileNodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set_8.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set_8.getQueue()[index].getF_score())
                        {
                            G.open_set_8.removeAt(index);
                            G.open_set_8.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x - 1, current_y] != -1)                   //check if the square above is walkable
            {
                tentative = new TileNode(position(current_x - 1, current_y), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getTileList()), new List<int>((current.getTileList())));            //its tile list is not updated, will update it in the next step
                index1 = tentative.getTileList().IndexOf(0);
                index2 = index1 - (small.GetLength(1) - 2);                     //or, i - (colLength - 2)

                updateTileList(tentative, index1, index2);

                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getTileList())))          //if the TileNode hasn't been explored yet
                {
                    updateFScorePuzzle(current, goal, "aStar");                         //Update f_score of the tentative TileNode

                    index = G.open_set_8.indexOfComplex(tentative);                     //check if this TileNodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set_8.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set_8.getQueue()[index].getF_score())
                        {
                            G.open_set_8.removeAt(index);
                            G.open_set_8.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x, current_y - 1] != -1)                   //check if the square left is walkable
            {
                tentative = new TileNode(position(current_x, current_y - 1), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getTileList()), new List<int>((current.getTileList())));            //its tile list is not updated, will update it in the next step
                index1 = tentative.getTileList().IndexOf(0);
                index2 = index1 - 1;                                    //or, i - 1

                updateTileList(tentative, index1, index2);

                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getTileList())))          //if the TileNode hasn't been explored yet
                {
                    updateFScorePuzzle(current, goal, "aStar");                         //Update f_score of the tentative TileNode

                    index = G.open_set_8.indexOfComplex(tentative);                     //check if this TileNodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set_8.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set_8.getQueue()[index].getF_score())
                        {
                            G.open_set_8.removeAt(index);
                            G.open_set_8.Enqueue(tentative);
                        }
                    }
                }
            }

            if (small[current_x, current_y + 1] != -1)                   //check if the square right is walkable
            {
                tentative = new TileNode(position(current_x, current_y + 1), current.getG_score() + 1, current.getPosition(), -1, new List<int>(current.getTileList()), new List<int>((current.getTileList())));            //its tile list is not updated, will update it in the next step
                index1 = tentative.getTileList().IndexOf(0);
                index2 = index1 + 1;                                    //or, i + 1

                updateTileList(tentative, index1, index2);

                G.expandedNodes++;

                if (!closed_set.Contains(tentative.getPosition() + ":" + String.Join(",", tentative.getTileList())))          //if the TileNode hasn't been explored yet
                {
                    updateFScorePuzzle(current, goal, "aStar");                         //Update f_score of the tentative TileNode

                    index = G.open_set_8.indexOfComplex(tentative);                     //check if this TileNodes exists already in the frontier, here (key=position:food list)

                    if (index == -1)                                                    //check if tentative has already been added to open_set
                    {
                        G.open_set_8.Enqueue(tentative);
                    }
                    else
                    {
                        if (tentative.getF_score() < G.open_set_8.getQueue()[index].getF_score())
                        {
                            G.open_set_8.removeAt(index);
                            G.open_set_8.Enqueue(tentative);
                        }
                    }
                }
            }
        }

        public void updateTileList(TileNode node, int index1, int index2)                               //if the current position is present in the food list, please remove it as you've eaten it! (hungry fellow)
        {
            int temp;
            List<int> tempList = node.getTileList();

            temp = tempList[index1];
            tempList[index1] = tempList[index2];
            tempList[index2] = temp;
            node.setTileList(tempList);
        }

        public void updateFScorePuzzle(TileNode node, TileNode goal, string algo)                //for the updating the f_scores in case of multiDot mazes
        {
            int h_score;

            h_score = levenshteinDistance(String.Join(",", node.getTileList()), String.Join(",", goal.getTileList()));
            //Console.WriteLine("Distance between {0} and {1} is: {2}", String.Join(",", node.getTileList()), String.Join(",", goal.getTileList()), h_score);

            switch (algo)
            {
                case "greedy":
                    node.setF_score(h_score);                               //since f(n) = h(n)
                    break;

                case "aStar":
                    node.setF_score(node.getG_score() + h_score);           //since f(n) = g(n) + h(n)
                    break;
            }
        }

        public int levenshteinDistance(string one, string two)
        {
            int lengthOne = one.Length;             //length of string one
            int lengthTwo = two.Length;             //length of string two
            int i, j;                               //iterators

            int[,] distance = new int[lengthOne + 1, lengthTwo + 1];

            for (i = 1; i < lengthOne; i++)
            {
                distance[i, 0] = i;
            }

            for (i = 1; i < lengthTwo; i++)
            {
                distance[0, i] = i;
            }

            for (j = 1; j <= lengthTwo; j++)
            {
                for (i = 1; i <= lengthOne; i++)
                {
                    if (one[i - 1].Equals(two[j - 1]))
                    {
                        distance[i, j] = distance[i - 1, j - 1];
                    }
                    else
                    {
                        distance[i, j] = Math.Min(distance[i - 1, j] + 1, Math.Min(distance[i, j - 1] + 1, distance[i - 1, j - 1] + 1));
                    }
                }
            }
            return distance[lengthOne, lengthTwo];
        }

        public void backTrackThePathPuzzle(TileNode startFrom, int[,] small)                      //for backtracking through structure, starting from a particular TileNode
        {
            TileNode temp;
            int i, j, index;
            int rows = small.GetLength(0) - 2;                                      //rows of the puzzle (without the fences of -1)
            int columms = small.GetLength(1) - 2;                                   //columns of the puzzle (without the fences of -1)

            temp = (TileNode)G.closed_set[startFrom.getPosition()];

            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < columms; j++)
                {
                    index = (i * columms) + j;                        //calculate the index in the list, mapping it to the matrix of the puzzle
                    Console.Write(temp.getTileList()[index]);
                }
                Console.WriteLine();
            }

            while (temp.getParent() != -1)
            {
                temp = (TileNode)G.closed_set[temp.getParent()];

                for (i = 0; i < rows; i++)
                {
                    for (j = 0; j < columms; j++)
                    {
                        index = (i * columms) + j;                        //calculate the index in the list, mapping it to the matrix of the puzzle
                        Console.Write(temp.getTileList()[index]);
                    }
                    Console.WriteLine();
                }
                Console.Write(" <-- ");
            }
        }
    }
}
