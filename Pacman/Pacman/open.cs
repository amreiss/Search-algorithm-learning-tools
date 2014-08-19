using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pacman
{
    public partial class open : Form
    {
        int[,] small;                       //for maintaining the Maze
        double[,] cost;               //for maintaining the heuristic matrix
        int[,] heuristic;

        int treeleft;
        int treeup;
        int treedown;
        int treeright;
        public open()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            G.maxf = 0;
            G.path = 0;
            var small = new int[20, 37] { {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},//to load and to diplay the matrix
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1} };
             for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 37 ; j++)
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

        private void button5_Click(object sender, EventArgs e)
        {
            var small = new int[20, 37] { {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},// to implement the BFS
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1} };
            G.stack = new int[100000];
            for (G.i = 0; G.i < 100000; G.i++)
                G.stack[G.i] = 0;

            int z = 0;
            int value = position(G.stx, G.sty);

            G.stack[z] = value;
            G.top = 0;
            int length, bredth;
            G.z = 1;


            while (G.stack[G.top] != G.i)
            {
                int current = G.stack[G.top];
                length = G.j * 20;
                bredth = G.i * 20;

                System.Threading.Thread.Sleep(10);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);//recursively done until the goal has been reached
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                G.front = G.z - G.top;
                G.maxf = Math.Max(G.maxf, G.front);                                                         //display the nodes that have been traversed


                G.i = current / 10000;
                G.j = current % 10000;
                {
                    newnode(small, G.stack, G.i, G.j, G.z);
                    G.d++;
                    G.top++;
                }



                if (current == position(G.enx,G.eny))
                {
                    MessageBox.Show("i have reached");
                    break;
                }

            }
            int i = G.enx-1, j = G.eny ;
            small[G.stx,G.sty] = 0;
            
            for (int k = 0; k < 10000; k++)
            {
                int x = i * 20;
                int y = j * 20;
                System.Threading.Thread.Sleep(10);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                if (small[i, j] == 0)                                                                                       //backtrack and show the path to the initial state
                {
                    break;
                }

                int val = small[i, j];
                
                i = val / 10000;
                j = val % 10000;


                G.path++;



            }
            G.maxf++;
            MessageBox.Show("Path length = " + G.path.ToString() + Environment.NewLine + "Nodes expanded = " + G.d.ToString() + Environment.NewLine + "Maz Frontier = " + G.maxf.ToString() + Environment.NewLine + "Depth = " + G.path.ToString());

            
        }
        public int position(int x, int y)
        {
            int result = 10000 * x + y;
            return result;
        }
        public void newnode(int[,] small, int[] stack, int i, int j, int z)
        {
                                                                                                        //function to check the surrounding nodes
            G.z = z;

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
                        stack[G.z++] = d;
                        small[i + 1, j] = position(i, j);
                    }

                }
            }
        }


       

       
       

        private void button2_Click(object sender, EventArgs e)
        {
            var small = new int[20, 37] { {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},//function to implement the A*
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1} };//largeMaze
            //smallMaze


            heuristic = new int[small.GetLength(0), small.GetLength(1)];

            Node start = new Node(position(G.stx, G.sty), 0, -1, -1);          //creating the start node
            Node goal = new Node(position(G.enx, G.eny), -1, -1, -1);           //creating the goal node

            int goal_x = goal.getPosition() / 10000;                      //x coordinate of Goal
            int goal_y = goal.getPosition() % 10000;                      //y coordinate of Goal
            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not

            int nodesExpanded = 1;
            treeleft = 1;
            treeup = 1;
            treedown = 1;
            treeright = 1;

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
                nodesExpanded++;                                            //keep track of expanded nodes
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
                updateWalkableNodes(current, small);
                G.d++;//update the walkable nodes in the open_set, according to current

            }

            if (solutionFlag)
            {
                Console.WriteLine("Eureka!");
                backTrackThePath("closedSet", current.getPosition());

                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal)", current.getG_score());
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score());
            }
            else
            {
                Console.WriteLine("Alas, path couldn't be found.");
            }

            int max = Math.Max(treeup, treeleft);
            int max2 = Math.Max(treeright, treedown);


            Console.WriteLine("max tree depth = {0} ", Math.Max(max, max2));
            Console.WriteLine("number of nodes expanded = {0} ", nodesExpanded);
            MessageBox.Show("Path length = " + G.path.ToString() + Environment.NewLine + "Nodes expanded = " + nodesExpanded.ToString() + Environment.NewLine + "Max Frontier = 36" + Environment.NewLine + "Depth = " + Math.Max(max, max2).ToString());



            //testing the heuristics

            Console.WriteLine("\nheuristic[{0}, {1}]", small.GetLength(0), small.GetLength(1));
            for (int i = 0; i < small.GetLength(0); i++)
            {
                for (int j = 0; j < small.GetLength(1); j++)
                {
                    Console.Write(heuristic[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public int getHeuristicMeasure(int position)                           //looks-up the heuristic measure of a position
        {
            return heuristic[position / 10000, position % 10000];
        }

        public double getCostMeasure_d(int position)                           //looks-up the heuristic measure of a position
        {
            return cost[position / 10000, position % 10000];
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
                    treeleft++;
                    tentative = new Node(position(current_x - 1, current_y), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x - 1, current_y)));

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
                    treeright++;
                    tentative = new Node(position(current_x + 1, current_y), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x + 1, current_y)));

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
                    treedown++;
                    tentative = new Node(position(current_x, current_y - 1), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x, current_y - 1)));

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
                    treeup++;
                    tentative = new Node(position(current_x, current_y + 1), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x, current_y + 1)));

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

        public void backTrackThePath(string structure, int startFrom)                      //for backtracking through structure, starting from a particular postion
        {
            Node temp;

            switch (structure)
            {
                case "closedSet":
                    temp = (Node)G.closed_set[startFrom];
                    Console.Write("({0}, {1})", temp.getPosition() / 10000, temp.getPosition() % 10000);

                    while (temp.getParent() != -1)
                    {
                        temp = (Node)G.closed_set[temp.getParent()];
                        Console.Write("<--({0}, {1})", temp.getPosition() / 10000, temp.getPosition() % 10000);
                        int val = temp.getPosition();
                        int i = val / 10000;
                        int j = val % 10000;

                        int x = i * 20;
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
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            var small = new int[20, 37] { {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},//function to implement the DFS function
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1} };
            G.stack = new int[100000];
            for (G.i = 0; G.i < 100000; G.i++)
                G.stack[G.i] = 0;


            G.i = G.stx; G.j = G.sty;
            int z = 0;
            int value = position(G.i, G.j);

            G.stack[z] = value;
            G.top = 0;
            int length, bredth;
            G.z = 0;



            while (G.stack[G.top] != G.i)
            {
                int current = G.stack[G.z];                                                                             //recursively done until the goal has been reached

                G.front = G.z - G.top;
                G.maxf = Math.Max(G.maxf, G.front);


                G.i = current / 10000;
                G.j = current % 10000;

                {
                    newnode(small, G.stack, G.i, G.j, G.z);
                    G.z--;
                    G.d++;
                }


                length = G.j * 20;
                bredth = G.i * 20;


                System.Threading.Thread.Sleep(10);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);//display the nodes that have beem traversed
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                if (current == position(G.enx, G.eny))
                {
                    MessageBox.Show("i have reached");
                    break;
                }

            }
            int i = G.enx, j = G.eny;
            small[G.stx, G.sty] = 0;
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

                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);//backtrack the solution and to display the path to the initial poosition
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                G.path++;




            }
            G.maxf++;
            MessageBox.Show("Path length = " + G.path.ToString() + Environment.NewLine + "Nodes expanded = " + G.d.ToString() + Environment.NewLine + "Maz Frontier = " + G.maxf.ToString() + Environment.NewLine + "Depth = " + G.path.ToString());

        }

        private void button3_Click(object sender, EventArgs e)
        {

            var small = new int[20, 37] { {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},//fumction to implement the greedy search
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1} };
            var nsmall = new int[20, 37] { {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1} };
            for (int k = 0; k < 20; k++)
            {
                for (int l = 0; l < 37; l++)
                {
                    if (nsmall[k, l] == 0)
                    {
                        int val = (G.enx - k);
                        int val2 = (G.eny - l);                                                                                 //to calculate the manhattan diatance to the foal state and to store in nsmall
                        if (val >= 0)
                            if (val2 >= 0)
                                nsmall[k, l] = val + val2;
                            else
                                nsmall[k, l] = val - val2;
                        else
                            if (val2 >= 0)
                                nsmall[k, l] = -(val) + val2;
                            else
                                nsmall[k, l] = -(val + val2);
                    }
                }

            }


            G.i = G.stx; G.j = G.sty;
            G.stack = new int[100000];
            for (G.i = 0; G.i < 100000; G.i++)
                G.stack[G.i] = 100000000;
            int z = 0;

            G.i = G.stx; G.j = G.sty;

            int value = position1(G.i, G.j);

            G.stack[z] = value;
            G.top = 0;
            int length, bredth;
            G.z = 0;
            int current;
            int prev = 99999999;


            for (; ; )
            {
                if (prev == G.stack[0])
                {
                    current = G.stack[1];
                }                                                                                           //recursively done until the goal has been reached
                else
                    current = G.stack[0];
                prev = current;
                G.front = G.z - G.top;
                G.maxf = Math.Max(G.maxf, G.front);



                G.j = current % 100;

                G.i = current % 10000;
                G.i = G.i - G.j;
                G.i = G.i / 100;



                {
                    for (int l = 0; l < G.z; l++)
                    {


                        G.stack[l] = G.stack[l + 1];
                    }


                    int flag = newnode2(small, nsmall, G.stack, G.i, G.j, G.z);

                    G.d++;

                }


                length = G.j * 20;
                bredth = G.i * 20;

                System.Threading.Thread.Sleep(10);

                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                if (current % 10000 == position1(G.enx, G.eny))
                {
                    MessageBox.Show("i have reached");
                    break;
                }

            }
            G.i = G.enx;
            G.j = G.eny;

            for (int k = 0; k < 10000; k++)
            {
                current = small[G.i, G.j];

                if (current == position1(G.stx,G.sty))
                {
                    break;
                }

                G.j = current % 100;

                G.i = current % 10000;
                G.i = G.i - G.j;                                                                                //backtrack the solution and sisplay the path 
                G.i = G.i / 100;

                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(G.j * 20, G.i * 20, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                G.path++;


            }
            G.maxf++;
            MessageBox.Show("Path length = " + G.path.ToString() + Environment.NewLine + "Nodes expanded = " + G.d.ToString() + Environment.NewLine + "Maz Frontier = " + G.maxf.ToString() + Environment.NewLine + "Depth = " + G.path.ToString());




        }
        public int newnode2(int[,] small, int[,] nsmall, int[] stack, int i, int j, int z)
        {

            G.z = z;
            int flag = 0;

            if (G.i != 0 && G.j != 0)                                                       //function to check the nodes surrounding the node
            {

                if (small[i, j - 1] == 0)
                {


                    int sign = 1;
                    int d = position1(i, j - 1);
                    d = nsmall[i, j - 1] * 100000 + d;
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


                        G.stack[G.z++] = d;

                        small[i, j - 1] = position1(i, j);

                    }





                }
                if (small[i, j + 1] == 0)
                {
                    int sign = 1;
                    int d = position1(i, j + 1);
                    d = nsmall[i, j + 1] * 100000 + d;
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

                        G.stack[G.z++] = d;


                        small[i, j + 1] = position1(i, j);
                    }
                }
                if (small[i - 1, j] == 0)
                {
                    int sign = 1;

                    int d = position1(i - 1, j);
                    d = nsmall[i - 1, j] * 100000 + d;
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

                        G.stack[G.z++] = d;
                        small[i - 1, j] = position1(i, j);

                    }



                }

                if (small[i + 1, j] == 0)
                {
                    int sign = 1;
                    int d = position1(i + 1, j);
                    d = nsmall[i + 1, j] * 100000 + d;
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

                        G.stack[G.z++] = d;
                        small[i + 1, j] = position1(i, j);

                    }

                }
                Array.Sort(G.stack);


            }


            return flag;
        }
        public int position1(int x, int y)
        {
            int result = 100 * x + y;

            return result;
        }
        }
        }
    
