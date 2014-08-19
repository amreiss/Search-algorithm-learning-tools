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
    public partial class Chess : Form
    {
        int[,] small;                       //for maintaining the Maze
        double[,] cost;               //for maintaining the heuristic matrix
        int[,] heuristic;

        int treeleft;
        int treeup;
        int treedown;
        int treeright;
        int tree1;
        int tree2;
        int tree3;
        int tree4;
        public Chess()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            G.count = 0;                                                                            //to implement the loading and displaying the maze
            G.maxf = 0;
            G.path = 0;
            

            var small = new int[24, 27] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1 }, { 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            //goal 18,5
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 27; j++)
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
                }
            }
        
        G.stx = 3;
        G.sty = 20;
        G.enx = 21;
        G.eny = 4;

            

        }

            

            
           
        

        public int position(int x, int y)
        {
            int result = 10000 * x + y;
            return result;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var small = new int[25, 27] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 100, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1 }, { 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };

            G.stack = new int[100000];
            for (G.i = 0; G.i < 100000; G.i++)//function which does BFS to the knight to the goal
                G.stack[G.i] = 0;

            G.i = 3; G.j = 20;
            int z = 0;
            int value = position(G.i, G.j);

            G.stack[z] = value;
            G.top = 0;
            int length, bredth;
            G.z = 1;


            while (G.stack[G.top] != G.i)
            {
                int current = G.stack[G.top];
                length = G.j * 20;
                bredth = G.i * 20;

                System.Threading.Thread.Sleep(50);                                                                                          //recursive function done until the goal has been reachedd
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                if (current == 210004)
                {
                    MessageBox.Show("i have reached");                                                                          //display the nodes that have been traversed
                    break;
                }
                G.front = G.z - G.top;
                G.maxf = Math.Max(G.maxf, G.front);
                G.d++;
                G.i = current / 10000;
                G.j = current % 10000;
                int i = G.i;
                int j = G.j;
                {
                    if (G.i != 0 && G.j != 0)
                    {
                        if (small[i-2, j - 1] == 0)                                                                                     //check all the nodes that are surroundin the given node
                        {

                            int sign = 1;
                            int d = position(i-2, j - 1);
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i-2, j - 1] = position(i, j);

                            }





                        }
                        if (small[i-1, j -2] == 0)
                        {
                            int sign = 1;
                            int d = position(i-1, j -2);
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i-1, j -2] = position(i, j);
                            }
                        }
                        if (small[i - 1, j+2] == 0)
                        {
                            int sign = 1;

                            int d = position(i - 1, j+2);
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i - 1, j+2] = position(i, j);
                            }



                        }

                        if (small[i -2, j+1] == 0)
                        {
                            int sign = 1;
                            int d = position(i -2, j+1);
                            for (int h = 0; h < z; h++)
                            {

                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i-2, j+1] = position(i, j);
                            }

                        }
                    }

                    if (small[i+1, j - 2] == 0)
                    {

                        int sign = 1;
                        int d = position(i+1,j-2);
                        for (int h = 0; h < z; h++)
                        {
                            if (G.stack[h] == d)
                            {
                                sign = 0;
                            }


                        }
                        if (sign == 1)
                        {
                            G.stack[G.z++] = d;
                            small[i+1, j - 2] = position(i, j);

                        }
                    }
                            
                        if (small[i+1, j +2] == 0)
                        {

                            int sign = 1;
                            int d = position(i+1, j +2);
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i+1, j -+2] = position(i, j);


                            }
                        }
                            
                        if (small[i+2, j - 1] == 0)
                        {

                            int sign = 1;
                            int d = position(i+2, j - 1);
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i+2, j - 1] = position(i, j);

                            }
                        }
                            
                        if (small[i+2,j + 1] == 0)
                        {

                            int sign = 1;
                            int d = position(i+2, j + 1);
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i+2, j + 1] = position(i, j);

                            }
                        }
            

                    G.top++;
                }



               

            }
           int  l = 21;
           int  m = 4;
            for (int k = 0; k < 10000; k++)
            {
                
                int val = small[l, m];
                l = val / 10000;
                m = val % 10000;
                int x = l * 20;
                int y = m * 20;
                


                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);                                    //backtrack the solution and display the path to the goal state
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                G.path++;
                if (l == 3 && m == 20)
                {
                    break;
                }

        
        }
            G.maxf++;
            MessageBox.Show("Path length = " + G.path.ToString() + Environment.NewLine + "Nodes expanded = " + G.d.ToString() + Environment.NewLine + "Maz Frontier = " + G.maxf.ToString() + Environment.NewLine + "Depth = " + G.path.ToString());



















        }

        private void button4_Click(object sender, EventArgs e)
        {
            var small = new int[25, 27] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 100, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1 }, { 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            G.stack = new int[100000];                      //implement DFS for the  knight
            for (G.i = 0; G.i < 100000; G.i++)
                G.stack[G.i] = 0;


            G.i = 3; G.j = 20;
            int z = 0;
            int value = position(G.i, G.j);

            G.stack[z] = value;
            G.top = 0;
            int length, bredth;
            G.z = 0;



            while (G.stack[G.top] != G.i)
            {
                int current = G.stack[G.z];

                if (current == 210004)
                {
                    MessageBox.Show("i have reached");
                    break;
                }                                                               //recursively done until the goal has been found
                G.front = G.z - G.top;
                G.maxf = Math.Max(G.maxf, G.front);

                G.i = current / 10000;
                G.j = current % 10000;
                G.d++;

                {
                 int i = G.i;
                int j = G.j;
                {
                    if (G.i != 0 && G.j != 0)
                    {
                      

                        if (small[i-2, j - 1] == 0)
                        {

                            int sign = 1;                                               //search all the nodes surrounding the nodes
                            int d = position(i-2, j - 1);
                            for (int h = 0; h < z; h++)
                     
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i-2, j - 1] = position(i, j);

                            }





                        }
                        if (small[i-1, j -2] == 0)
                        {
                            int sign = 1;
                            int d = position(i-1, j -2);
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i-1, j -2] = position(i, j);
                            }
                        }
                        if (small[i - 1, j+2] == 0)
                        {
                            int sign = 1;

                            int d = position(i - 1, j+2);
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i - 1, j+2] = position(i, j);
                            }



                        }

                        if (small[i -2, j+1] == 0)
                        {
                            int sign = 1;
                            int d = position(i -2, j+1);
                            for (int h = 0; h < z; h++)
                            {

                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i-2, j+1] = position(i, j);
                            }

                        }
                    }

                    if (small[i+1, j - 2] == 0)
                    {

                        int sign = 1;
                        int d = position(i+1,j-2);
                        for (int h = 0; h < z; h++)
                        {
                            if (G.stack[h] == d)
                            {
                                sign = 0;
                            }


                        }
                        if (sign == 1)
                        {
                            G.stack[G.z++] = d;
                            small[i+1, j - 2] = position(i, j);

                        }
                    }
                            
                        if (small[i+1, j +2] == 0)
                        {

                            int sign = 1;
                            int d = position(i+1, j +2);
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i+1, j +2] = position(i, j);


                            }
                        }
                            
                        if (small[i+2, j - 1] == 0)
                        {

                            int sign = 1;
                            int d = position(i+2, j - 1);
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i+2, j - 1] = position(i, j);

                            }
                        }
                            
                        if (small[i+2,j + 1] == 0)
                        {

                            int sign = 1;
                            int d = position(i+2, j + 1);
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i+2, j + 1] = position(i, j);

                            }
                        }
            

                    G.z--;
                }



               

          
                }


                length = G.j * 20;
                bredth = G.i * 20;


                System.Threading.Thread.Sleep(10);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));                 //display all the nodes that have been traversed
                myBrush.Dispose();
                formGraphics.Dispose();
                if (current == position(G.enx, G.eny))
                {
                    MessageBox.Show("i have reached");
                    break;
                }

            }
           int  l = 21;
           int  m = 4;
            for (int k = 0; k < 10000; k++)
            {
                
                int val = small[l, m];
                l = val / 10000;
                m = val % 10000;
                int x = l * 20;
                int y = m * 20;
                

                System.Threading.Thread.Sleep(10);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);//backtrack the solution and display the path teavelled
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                if (small[l, m] == 100)
                {
                    break;
                }
                G.path++;



            }
            G.depth = G.path + 8;
            G.maxf++;
            MessageBox.Show("Path length = " + G.path.ToString() + Environment.NewLine + "Nodes expanded = " + G.d.ToString() + Environment.NewLine + "Maz Frontier = " + G.maxf.ToString() + Environment.NewLine + "Depth = " + G.depth.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {

            var small = new int[25, 27] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 100, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1 }, { 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            var nsmall = new int[25, 27] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 100, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1 }, { 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 }, { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
           
            for (int k = 0; k < 25; k++)    //implement the greegy solution
            {
                for (int l = 0; l < 27; l++)
                {
                    if (nsmall[k, l] == 0)
                    {
                        int val = (21 - k);
                        int val2 = (4 - l);
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

            
            G.stack = new int[100000];
            for (G.i = 0; G.i < 100000; G.i++)
                G.stack[G.i] = 100000000;
            int z = 0;

            G.i = 3; G.j = 21;

            int value = position1(G.i, G.j);

            G.stack[z] = value;
            G.top = 0;
            int length, bredth;
            G.z = 1;




            for (; ; )
            {
                int current = G.stack[0];
                G.front = G.z - G.top;
                G.maxf = Math.Max(G.maxf, G.front);                             //recursively done until the goal has been reached


                G.j = current % 100;

                G.i = current % 10000;
                G.i = G.i - G.j;
                G.i = G.i / 100;


                {
                    for (int l = 0; l < G.z; l++)
                    {


                        G.stack[l] = G.stack[l + 1];
                    }

                    G.z--;
                    int flag = newnode2(small, nsmall, G.stack, G.i, G.j, G.z);


                }


                length = G.j * 20;
                bredth = G.i * 20;

                System.Threading.Thread.Sleep(25);

                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();                                                       //display the nodes that hae been traversed
                formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                G.d++;
                if (current % 10000 == 2104)
                {
                    MessageBox.Show("i have reached");
                    break;
                }

            }
            G.i = 21;
            G.j = 4;

            for (int k = 0; k < 10000; k++)
            {
                int current = small[G.i, G.j];

                if (position1(G.i, G.j) == 321)
                {
                    break;
                }

                G.j = current % 100;

                G.i = current % 10000;
                G.i = G.i - G.j;
                G.i = G.i / 100;
                G.path++;
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();                                   //backtrack and find the path of the knight
                formGraphics.FillRectangle(myBrush, new Rectangle(G.j * 20, G.i * 20, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();


            }

            G.maxf++;
            MessageBox.Show("Path length = " + G.path.ToString() + Environment.NewLine + "Nodes expanded = " + G.d.ToString() + Environment.NewLine + "Maz Frontier = " + G.maxf.ToString() + Environment.NewLine + "Depth = " + G.path.ToString());




        }
        public int newnode2(int[,] small, int[,] nsmall, int[] stack, int i, int j, int z)
        {

            G.z = z;
            int flag = 0;

            if (G.i != 0 && G.j != 0)
            {

                if (small[i-2, j - 1] == 0)
                        {

                            int sign = 1;
                            int d = position1(i-2, j - 1);
                            d = nsmall[i-2, j - 1] * 100000 + d;                                    //function to search all the nodes surrounding the given nodes
                            for (int h = 0; h < z; h++)
                     
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i-2, j - 1] = position1(i, j);

                            }





                        }
                        if (small[i-1, j -2] == 0)
                        {
                            int sign = 1;
                            int d = position1(i-1, j -2);
                            d = nsmall[i-1, j - 2] * 100000 + d;
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i-1, j -2] = position1(i, j);
                            }
                        }
                        if (small[i - 1, j+2] == 0)
                        {
                            int sign = 1;

                            int d = position1(i - 1, j+2);
                            d = nsmall[i - 1, j + 2] * 100000 + d;
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i - 1, j+2] = position1(i, j);
                            }



                        }

                        if (small[i -2, j+1] == 0)
                        {
                            int sign = 1;
                            int d = position1(i -2, j+1);
                            d = nsmall[i - 2, j +1] * 100000 + d;
                            for (int h = 0; h < z; h++)
                            {

                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i-2, j+1] = position1(i, j);
                            }

                        }
                    }

                    if (small[i+1, j - 2] == 0)
                    {

                        int sign = 1;
                        int d = position1(i+1,j-2);
                        d = nsmall[i + 1, j - 2] * 100000 + d;
                        for (int h = 0; h < z; h++)
                        {
                            if (G.stack[h] == d)
                            {
                                sign = 0;
                            }


                        }
                        if (sign == 1)
                        {
                            G.stack[G.z++] = d;
                            small[i+1, j - 2] = position1(i, j);

                        }
                    }
                            
                        if (small[i+1, j +2] == 0)
                        {

                            int sign = 1;
                            int d = position1(i+1, j +2);
                            d = nsmall[i + 1, j + 2] * 100000 + d;
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i+1, j +2] = position1(i, j);


                            }
                        }
                            
                        if (small[i+2, j - 1] == 0)
                        {

                            int sign = 1;
                            int d = position1(i+2, j - 1);
                            d = nsmall[i +2 , j - 1] * 100000 + d;
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i+2, j - 1] = position1(i, j);

                            }
                        }
                            
                        if (small[i+2,j + 1] == 0)
                        {

                            int sign = 1;
                            int d = position1(i+2, j + 1);
                            d = nsmall[i + 2, j +1] * 100000 + d;
                            for (int h = 0; h < z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i+2, j + 1] = position1(i, j);

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

            if (small[current_x - 2, current_y - 1] == 0)             //check if the square above is walkable
            {
                if (!G.closed_set.Contains(position(current_x - 2, current_y - 1)))          //if the node hasn't been explored yet
                {
                    treeleft++;
                    tentative = new Node(position(current_x - 2, current_y - 1), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x - 2, current_y - 1)));

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

            if (small[current_x - 1, current_y - 2] == 0)         //check if the square below is walkable
            {
                if (!G.closed_set.Contains(position(current_x - 1, current_y - 2)))          //if the node hasn't been explored yet
                {
                    treeright++;
                    tentative = new Node(position(current_x - 1, current_y - 2), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x - 1, current_y - 2)));

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

            if (small[current_x - 1, current_y + 2] == 0)          //check if the square left is walkable
            {
                if (!G.closed_set.Contains(position(current_x - 1, current_y + 2)))          //if the node hasn't been explored yet
                {
                    treedown++;
                    tentative = new Node(position(current_x - 1, current_y + 2), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x - 1, current_y + 2)));

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

            if (small[current_x - 2, current_y + 1] == 0)          //check if the square right is walkable
            {
                if (!G.closed_set.Contains(position(current_x - 2, current_y + 1)))          //if the node hasn't been explored yet
                {
                    treeup++;
                    tentative = new Node(position(current_x - 2, current_y + 1), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x - 2, current_y + 1)));

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
            if (small[current_x + 1, current_y - 2] == 0)         //check if the square below is walkable
            {
                if (!G.closed_set.Contains(position(current_x + 1, current_y - 2)))          //if the node hasn't been explored yet
                {
                    tree1++;
                    tentative = new Node(position(current_x + 1, current_y - 2), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x + 1, current_y - 2)));

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
            if (small[current_x + 1, current_y + 2] == 0)         //check if the square below is walkable
            {
                if (!G.closed_set.Contains(position(current_x + 1, current_y + 2)))          //if the node hasn't been explored yet
                {
                    tree2++;
                    tentative = new Node(position(current_x + 1, current_y + 2), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x + 1, current_y + 2)));

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
            if (small[current_x + 2, current_y - 1] == 0)         //check if the square below is walkable
            {
                if (!G.closed_set.Contains(position(current_x + 2, current_y - 1)))          //if the node hasn't been explored yet
                {
                    tree3++;
                    tentative = new Node(position(current_x + 2, current_y - 1), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x + 2, current_y - 1)));

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
            if (small[current_x + 2, current_y + 1] == 0)         //check if the square below is walkable
            {
                if (!G.closed_set.Contains(position(current_x + 2, current_y + 1)))          //if the node hasn't been explored yet
                {
                    tree4++;
                    tentative = new Node(position(current_x + 2, current_y + 1), current.getG_score() + 1, current.getPosition(), (current.getG_score() + 1) + getHeuristicMeasure(position(current_x + 2, current_y + 1)));

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

        private void button3_Click(object sender, EventArgs e)
        {
            var small = new int[25, 27] { 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            { 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            { 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 },
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1 },
            { 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
            { 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 },
            { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1 },
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } ,
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            //goal 18,5


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
            tree1 = 1;
            tree2 = 1;
            tree3 = 1;
            tree4 = 1;

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
            int g =0; int h=0;
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
                int  i = val / 10000;
                 int j = val % 10000;
                 g = i;
                 h = j;
                 if (position(g, h) == position(G.enx, G.eny))
                 {
                     solutionFlag = true;
                     break;
                 }
                int x = i * 20;
                int y = j * 20;
                System.Threading.Thread.Sleep(10);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(y, x, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                updateWalkableNodes(current, small);                    //update the walkable nodes in the open_set, according to current

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
            int max3 = Math.Max(tree1, tree2);
            int max4 = Math.Max(tree3, tree4);
            int max5 = Math.Max(max, max2);
            int max6 = Math.Max(max3, max4);
        

            Console.WriteLine("max tree depth = {0} ", Math.Max(max5, max6));
            Console.WriteLine("number of nodes expanded = {0} ", nodesExpanded);
            MessageBox.Show("Path length = " + G.path.ToString() + Environment.NewLine + "Nodes expanded = " + nodesExpanded.ToString() + Environment.NewLine + "Max Frontier = " + G.maxf.ToString() + Environment.NewLine + "Depth = " + Math.Max(max5, max6).ToString());


            //testing the heuristics

            Console.WriteLine("\nheuristic[{0}, {1}]", small.GetLength(0), small.GetLength(1));
            for (int n = 0; n < small.GetLength(0); n++)
            {
                for (int m = 0; m < small.GetLength(1); m++)
                {
                    Console.Write(heuristic[n, m] + "\t");
                }
                Console.WriteLine();
            }
        
        }
    }
}
