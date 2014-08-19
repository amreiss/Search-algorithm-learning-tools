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
    public partial class cost1 : Form
    {
        int[,] small;                       //for maintaining the Maze
        double[,] cost;               //for maintaining the heuristic matrix
        int[,] heuristic;

        int treeleft;
        int treeup;
        int treedown;
        int treeright;
        public cost1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            G.path = 0;
            G.d = 0;
            G.count = 0;

            small = new int[21,41] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                    {1, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
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
                                    {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 3, 1}, 
                                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}}; 
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 41; j++)
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

 
        public int position(int x, int y)
        {
            int result = 10000 * x + y;
            return result;
        }
        public void newnode(int[,] small, int[] stack, int i, int j, int z)
        {

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

      
        public int newnode2(int[,] small,int[,] nsmall, int[] stack, int i, int j, int z)
        {

            G.z = z;
            int flag = 0;

            if (G.i != 0 && G.j != 0)
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
                        G.z++;
                        
                        G.stack[G.z] = d;
                       
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
                        G.z++;
                        G.stack[G.z] = d;
                        

                        small[i, j + 1] = position1(i, j);
                    }
                }
                if (small[i - 1, j] == 0)
                {
                    int sign = 1;

                    int d = position1(i - 1, j);
                    d = nsmall[i-1, j ] * 100000 + d;
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
                        G.z++;
                        G.stack[G.z] = d;
                        small[i - 1, j] = position1(i, j);
                        
                    }



                }

                if (small[i + 1, j] == 0)
                {
                    int sign = 1;
                    int d = position1(i + 1, j);
                    d = nsmall[i+1, j ] * 100000 + d;
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
                        G.z++;
                        G.stack[G.z] = d;
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
                    treeright++;
                    tentative = new Node(position(current_x + 1, current_y), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x + 1, current_y)));

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
                    treedown++;
                    tentative = new Node(position(current_x, current_y - 1), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x, current_y - 1)));

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
                    treeup++;
                    tentative = new Node(position(current_x, current_y + 1), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x, current_y + 1)));

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
                        System.Drawing.Graphics formGraphics = CreateGraphics();
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

        public int getHeuristicMeasure(int position)                           //looks-up the heuristic measure of a position
        {
            return heuristic[position / 10000, position % 10000];
        }

        public void updateWalkableNodes_cost(Node current, int[,] small)
        {
            int current_x = current.getPosition() / 10000;                      //find out the coordinates of current node
            int current_y = current.getPosition() % 10000;
            Node tentative;                                                  //to keep track of the next 
            int index;                                                       //to get index of tentative from open_set

            if (small[current_x - 1, current_y] == 0)                       //check if the square above is walkable
            {
                if (!G.closed_set.Contains(position(current_x - 1, current_y)))          //if the node hasn't been explored yet
                {
                    treeleft++;
                    tentative = new Node(position(current_x - 1, current_y), current.getG_score() + cost[current_x - 1, current_y], current.getPosition(), (current.getG_score() + cost[current_x - 1, current_y]));

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
                    treeright++;
                    tentative = new Node(position(current_x + 1, current_y), current.getG_score() + cost[current_x + 1, current_y], current.getPosition(), (current.getG_score() + cost[current_x + 1, current_y]));

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
                    treedown++;
                    tentative = new Node(position(current_x, current_y - 1), current.getG_score() + cost[current_x, current_y - 1], current.getPosition(), (current.getG_score() + cost[current_x, current_y - 1]));

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
                    treeup++;
                    tentative = new Node(position(current_x, current_y + 1), current.getG_score() + cost[current_x, current_y + 1], current.getPosition(), (current.getG_score() + cost[current_x, current_y + 1]));

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

        public double getCostMeasure_d(int position)                           //looks-up the heuristic measure of a position
        {
            return cost[position / 10000, position % 10000];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //small = new int[,] {{ 1, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1 }};          //toy_set

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

            cost = new double[small.GetLength(0), small.GetLength(1)];                  //the cost matrix representing g(n)

            Node start = new Node(position(19, 39), 0, -1, -1);          //creating the start node
            Node goal = new Node(position(1, 1), -1, -1, -1);           //creating the goal node
            G.maxDepth = 0;                                             //resetting the maximum size of frontier

            int goal_x = goal.getPosition() / 10000;                      //x coordinate of Goal
            int goal_y = goal.getPosition() % 10000;                      //y coordinate of Goal
            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not
            int nodesExpanded = 1;

            for (int i = 0; i < small.GetLength(0); i++)                    //Populating the cost matrix using the cost funtion c(n) = (.5^x)
            {
                for (int j = 0; j < small.GetLength(1); j++)
                {
                    cost[i, j] = Math.Pow(.5, j);
                }
            }

            start.setF_score(start.getG_score());   //Populate f(n) for start node; where f(n) = g(n) as there are no heuristics involved for uniform cost search strategy

            Node current = new Node();                                      //to keep track of current node


            //Initialize the frontier with the start node
            G.open_set.Enqueue(start);
            G.maxDepth++;

            while (G.open_set.isEmpty() == false)                           //loop until there are still some nodes in the frontier
            {
                current = G.open_set.Dequeue();                             //dequeue the lowest f(n) node from the frontier
                nodesExpanded++; 
                G.closed_set.Add(current.getPosition(), current);           //transfer it to the explored_set

                if (current.getPosition() == goal.getPosition())       //check if the current node is the goal state!
                {
                    solutionFlag = true;
                    break;
                }
                int val = current.getPosition();
               int  i = val / 10000;
               int  j = val % 10000;

                int x = i * 20;
                int y = j * 20;
                System.Threading.Thread.Sleep(10);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                updateWalkableNodes_cost(current, small);
                G.d++;//update the walkable nodes in the open_set, according to current
            }

            if (solutionFlag)
            {
                Console.WriteLine("Eureka!");
                backTrackThePath("closedSet", current.getPosition());

                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal)", current.getG_score());
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score());

                Console.WriteLine("Maximum depth of the frontier: {0}", G.maxDepth);
            }
            else
            {
                Console.WriteLine("Alas, path couldn't be found.");
            }

            int max = Math.Max(treeup, treeleft);
            int max2 = Math.Max(treeright, treedown);
            MessageBox.Show("Path length = " + G.path.ToString() + Environment.NewLine + "Nodes expanded = " + nodesExpanded.ToString() + Environment.NewLine + "Max Frontier =8" + Environment.NewLine + "Depth = " + Math.Max(max, max2).ToString());



            Console.WriteLine("max tree depth = {0} ", Math.Max(max, max2));
            Console.WriteLine("number of nodes expanded = {0} ", nodesExpanded);

            //testing the cost matrix
            Console.WriteLine("\ncost[{0}, {1}]", cost.GetLength(0), cost.GetLength(1));
            for (int i = 0; i < cost.GetLength(0); i++)
            {
                for (int j = 0; j < cost.GetLength(1); j++)
                {
                    Console.Write(cost[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //small = new int[,] {{ 1, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1 }};          //toy_set

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

            cost = new double[small.GetLength(0), small.GetLength(1)];                  //the cost matrix representing g(n)

            Node start = new Node(position(19, 39), 0, -1, -1);          //creating the start node
            Node goal = new Node(position(1, 1), -1, -1, -1);           //creating the goal node
            G.maxDepth = 0;                                             //resetting the maximum depth of the frontier

            int goal_x = goal.getPosition() / 10000;                      //x coordinate of Goal
            int goal_y = goal.getPosition() % 10000;                      //y coordinate of Goal
            bool solutionFlag = false;                                  //to keep track if solution to the maze has been found or not
            int nodesExpanded = 1;

            for (int i = 0; i < small.GetLength(0); i++)                    //Populating the cost matrix using the cost funtion c(n) = (.5^x)
            {
                for (int j = 0; j < small.GetLength(1); j++)
                {
                    cost[i, j] = Math.Pow(2, j);
                }
            }

            start.setF_score(start.getG_score());   //Populate f(n) for start node; where f(n) = g(n) as there are no heuristics involved for uniform cost search strategy

            Node current = new Node();                                      //to keep track of current node


            //Initialize the frontier with the start node
            G.open_set.Enqueue(start);
            G.maxDepth++;

            while (G.open_set.isEmpty() == false)                           //loop until there are still some nodes in the frontier
            {
                current = G.open_set.Dequeue();                             //dequeue the lowest f(n) node from the frontier
                nodesExpanded++; 
                G.closed_set.Add(current.getPosition(), current);           //transfer it to the explored_set

                if (current.getPosition() == goal.getPosition())       //check if the current node is the goal state!
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
                updateWalkableNodes_cost(current, small);                    //update the walkable nodes in the open_set, according to current
            }

            if (solutionFlag)
            {
                Console.WriteLine("Eureka!");
                backTrackThePath("closedSet", current.getPosition());

                Console.WriteLine("\nCoordinates | g(Goal) | parent | f(Goal)", current.getG_score());
                Console.WriteLine("({0}, {1}) | {2} | ({3}, {4}) | {5}", current.getPosition() / 10000, current.getPosition() % 10000, current.getG_score(), current.getParent() / 10000, current.getParent() % 10000, current.getF_score());

                Console.WriteLine("Maximum depth of the frontier: {0}", G.maxDepth);

            }
            else
            {
                Console.WriteLine("Alas, path couldn't be found.");
            }

            int max = Math.Max(treeup, treeleft);
            int max2 = Math.Max(treeright, treedown);


            Console.WriteLine("max tree depth = {0} ", Math.Max(max, max2));
            Console.WriteLine("number of nodes expanded = {0} ", nodesExpanded);
            MessageBox.Show("Path length = " + G.path.ToString() + Environment.NewLine + "Nodes expanded = " + nodesExpanded.ToString() + Environment.NewLine + "Max Frontier =16" + Environment.NewLine + "Depth = " + Math.Max(max, max2).ToString());



            //testing the cost matrix
            Console.WriteLine("\ncost[{0}, {1}]", cost.GetLength(0), cost.GetLength(1));
            for (int i = 0; i < cost.GetLength(0); i++)
            {
                for (int j = 0; j < cost.GetLength(1); j++)
                {
                    Console.Write(cost[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
       
    }
}
