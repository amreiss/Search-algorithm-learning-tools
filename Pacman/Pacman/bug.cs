using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Pacman
{
    public partial class bug : Form
    {
        int[,] small;                   //for maintaining the Maze
        int[,] heuristic;               //for maintaining the heuristic matrix
        Hashtable closed_set = new Hashtable();                         //For keeping track of explored nodes; of the form (state, g(n), parent, f(n))
        Queue open_set_Q = new Queue();                                 //For keeping track of frontier (Queue); of the form (state, g(n), parent, f(n), food, parentFood)
        Stack open_set_S = new Stack();                                 //For keeping track of frontier (Stack); of the form (state, g(n), parent, f(n), food, parentFood)

        public bug()
        {
            InitializeComponent();
        }
        
        public int position(int x, int y)               //Position encoder
        {
            int result = 10000 * x + y;
            return result;
        }
     

        private void button1_Click(object sender, EventArgs e)
        {
            G.d = 0;
            G.path = 0;
            var small = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
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
            G.maxDepth = 0;                                               //resetting the maximum depth frontier

            int goal_x = goal.getPosition() / 10000;                      //x coordinate of Goal
            int goal_y = goal.getPosition() % 10000;                      //y coordinate of Goal
            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not

            Node current = new Node();                                  //to keep track of current node


            //Initialize the frontier with the start node
            G.open_set.Enqueue(start);
            G.maxDepth++;

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
                    int val = current.getPosition();
                    int i = val / 10000;
                    int j = val % 10000;

                    int x = i * 20;
                    int y = j * 20;
                    System.Threading.Thread.Sleep(10);
                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                    System.Drawing.Graphics formGraphics = this.CreateGraphics();
                    formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                    myBrush.Dispose();
                    formGraphics.Dispose();
                    G.d++;
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
                MessageBox.Show("path = " + G.path + Environment.NewLine + " Nodes Expanded = " + G.d);
                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal)", current.getG_score());
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score());

                Console.WriteLine("Maximum depth of the frontier: {0}", G.maxDepth);
            }
            else
            {
                Console.WriteLine("Alas, path couldn't be found.");
            }
        }

        public void backTrackThePath(string structure, Node startFrom)                      //for backtracking through structure, starting from a particular node
        {
            Node temp;
            int i;

            switch (structure)
            {
                case "closedSet":
                    temp = (Node)G.closed_set[startFrom.getPosition()];
                    Console.Write("({0}, {1})", temp.getPosition() / 10000, temp.getPosition() % 10000);

                    while (temp.getParent() != -1)
                    {
                        temp = (Node)G.closed_set[temp.getParent()];
                        Console.Write("<--({0}, {1})", temp.getPosition() / 10000, temp.getPosition() % 10000);
                          int val = temp.getPosition();
                        int m = val / 10000;
                        int j = val % 10000;

                        int x = m * 20;
                        int y = j * 20;
                        System.Threading.Thread.Sleep(10);
                        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
                        System.Drawing.Graphics formGraphics = this.CreateGraphics();
                        formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                        myBrush.Dispose();
                        formGraphics.Dispose();
                        G.path++;
                    

                    }
                    break;

                case "openSet":
                    G.open_set.printQueue();
                    break;

            //case "closedSetMultiDot":                                           //for backtracking a multiDot maze
            //    temp = (Node)closed_set[startFrom.getPosition() + ":" + String.Join(",", startFrom.getFood())];
            //    Console.Write("({0}, {1}):[", temp.getPosition() / 10000, temp.getPosition() % 10000);
            //    for (i = 0; i < temp.getFood().Count; i++)
            //    {
            //        Console.Write("({0}, {1})", temp.getFood()[i] / 10000, temp.getFood()[i] % 10000);
            //    }
            //    Console.Write("]");

            //    while (temp.getParent() != -1)
            //    {
            //        temp = (Node)closed_set[temp.getParent() + ":" + String.Join(",", temp.getParentFood())];
            //        Console.Write("<--({0}, {1}):[", temp.getPosition() / 10000, temp.getPosition() % 10000);

            //        for (i = 0; i < temp.getFood().Count; i++)
            //        {
            //            Console.Write("({0}, {1})", temp.getFood()[i] / 10000, temp.getFood()[i] % 10000);
            //        }
            //        Console.Write("]");
            //    }
            //    break;
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

                        if (G.maxDepth < G.open_set.getQueue().Count)
                        {
                            G.maxDepth = G.open_set.getQueue().Count;                   //update the high water mark of the frontier
                        }
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

                        if (G.maxDepth < G.open_set.getQueue().Count)
                        {
                            G.maxDepth = G.open_set.getQueue().Count;                   //update the high water mark of the frontier
                        }
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

                        if (G.maxDepth < G.open_set.getQueue().Count)
                        {
                            G.maxDepth = G.open_set.getQueue().Count;                   //update the high water mark of the frontier
                        }
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

                        if (G.maxDepth < G.open_set.getQueue().Count)
                        {
                            G.maxDepth = G.open_set.getQueue().Count;                   //update the high water mark of the frontier
                        }
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

        private void button4_Click(object sender, EventArgs e)
        {
            var small = new int[20,33] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1}, 
                                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
                                {1, 0, 2, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
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
                                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}}; 
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 33; j++)
                {
                    if (small[i, j] == 1)
                    {
                        int length = j * 20;
                        int bredth = i * 20;


                        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                        System.Drawing.Graphics formGraphics = this.CreateGraphics();
                        formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                        myBrush.Dispose();
                        formGraphics.Dispose();
                    }
                    if (small[i, j] == 3)
                    {
                        int length = j * 20;
                        int bredth = i * 20;
                        G.count++;
                        G.enx = i;
                        G.eny = j;

                        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Gold);
                        System.Drawing.Graphics formGraphics = this.CreateGraphics();
                        formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                        myBrush.Dispose();
                        formGraphics.Dispose();


                    }
                    if (small[i, j] == 2)
                    {
                        int length = j * 20;
                        int bredth = i * 20;
                        G.count++;
                        G.stx = i;
                        G.sty = j;

                        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                        System.Drawing.Graphics formGraphics = this.CreateGraphics();
                        formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                        myBrush.Dispose();
                        formGraphics.Dispose();



                    }
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
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
            G.maxDepth = 0;

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
            G.maxDepth++;

            while (G.open_set.isEmpty() == false)                           //loop until there are still some nodes in the frontier
            {
                current = G.open_set.Dequeue();                             //dequeue the lowest f(n) node from the frontier
                G.closed_set.Add(current.getPosition(), current);           //transfer it to the explored_set

                if (getHeuristicMeasure(current.getPosition()) == 0)       //check if the current node is the goal state!
                {
                    solutionFlag = true;
                    break;
                }
                int val = current.getPosition();
                int i = val / 10000;
                int j = val % 10000;

                int x = i * 20;
                int y = j * 20;
                System.Threading.Thread.Sleep(10);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                G.d++;
                updateWalkableNodes(current, small);                    //update the walkable nodes in the open_set, according to current
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
                MessageBox.Show("path = " + G.path + Environment.NewLine + " Nodes Expanded = " + G.d);
                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal)", current.getG_score());
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score());

                Console.WriteLine("Maximum depth of the frontier: {0}", G.maxDepth);
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

                        if (G.maxDepth < G.open_set.getQueue().Count)
                        {
                            G.maxDepth = G.open_set.getQueue().Count;                   //update the high water mark of the frontier
                        }
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

                        if (G.maxDepth < G.open_set.getQueue().Count)
                        {
                            G.maxDepth = G.open_set.getQueue().Count;                   //update the high water mark of the frontier
                        }
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

                        if (G.maxDepth < G.open_set.getQueue().Count)
                        {
                            G.maxDepth = G.open_set.getQueue().Count;                   //update the high water mark of the frontier
                        }
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

                        if (G.maxDepth < G.open_set.getQueue().Count)
                        {
                            G.maxDepth = G.open_set.getQueue().Count;                   //update the high water mark of the frontier
                        }
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
    }
}
