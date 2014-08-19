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
    public partial class submedium : Form
    {
        public submedium()
        {
            InitializeComponent();
        }
        public int position(int x, int y)
        {
            int result = 10000 * x + y;
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            G.maxf = 0;
            G.path = 0;
            G.count = 0;
            var small = new int[21, 28] {{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},//function to load the maze on to the interface
{1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1},
{1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1},
{1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
{1,2,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,2,1},
{1,2,2,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1},
{1,1,1,1,1,1,2,1,1,1,1,1,0,1,1,0,1,1,1,1,1,2,1,1,1,1,1,1},
{1,1,1,1,1,1,2,1,1,0,0,0,0,0,0,0,0,0,0,1,1,2,1,1,1,1,1,1},
{1,1,1,1,1,1,2,1,1,0,1,1,1,0,0,1,1,1,0,1,1,2,1,1,1,1,1,1},
{1,2,2,2,2,2,2,0,0,0,1,2,0,0,0,0,2,1,0,0,0,2,2,2,2,2,2,1},
{1,1,1,1,1,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,1,1,1,1,1},
{1,1,1,1,1,1,2,1,1,0,0,0,0,0,0,0,0,0,0,1,1,2,1,1,1,1,1,1},
{1,1,1,1,1,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,1,1,1,1,1},
{1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1},
{1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1},
{1,2,2,2,2,1,2,2,2,2,2,2,2,0,3,2,2,2,2,2,2,2,1,2,2,2,2,1},
{1,1,1,1,2,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,2,1,1,1,1},
{1,2,2,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1},
{1,2,1,1,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,1,1,2,1},
{1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
 };
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 28; j++)
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
                    if (small[i, j] == 2)
                    {
                        int length = j * 20;
                        int bredth = i * 20;
                        G.count++;

                        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
                        System.Drawing.Graphics formGraphics = this.CreateGraphics();
                        formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                        myBrush.Dispose();
                        formGraphics.Dispose();
                    }
                    if (small[i, j] == 3)
                    {
                        int length = j * 20;
                        int bredth = i * 20;


                        G.stx = i; G.sty = j;

                        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
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
            var small = new int[21, 28] {{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, //function to implement the multi dot eating in the maze
{1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1},
{1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1},
{1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
{1,2,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,2,1},
{1,2,2,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1},
{1,1,1,1,1,1,2,1,1,1,1,1,0,1,1,0,1,1,1,1,1,2,1,1,1,1,1,1},
{1,1,1,1,1,1,2,1,1,0,0,0,0,0,0,0,0,0,0,1,1,2,1,1,1,1,1,1},
{1,1,1,1,1,1,2,1,1,0,1,1,1,0,0,1,1,1,0,1,1,2,1,1,1,1,1,1},
{1,2,2,2,2,2,2,0,0,0,1,2,0,0,0,0,2,1,0,0,0,2,2,2,2,2,2,1},
{1,1,1,1,1,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,1,1,1,1,1},
{1,1,1,1,1,1,2,1,1,0,0,0,0,0,0,0,0,0,0,1,1,2,1,1,1,1,1,1},
{1,1,1,1,1,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,1,1,1,1,1},
{1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1},
{1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1},
{1,2,2,2,2,1,2,2,2,2,2,2,2,0,0,2,2,2,2,2,2,2,1,2,2,2,2,1},
{1,1,1,1,2,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,2,1,1,1,1},
{1,2,2,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1},
{1,2,1,1,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,1,1,2,1},
{1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
 };
            G.stack = new int[100000];
            for (G.i = 0; G.i < 100000; G.i++)
                G.stack[G.i] = 0;
            G.path = 318;
            G.i = G.stx; G.j = G.sty;
            int z = 0;
            int value = position(G.i, G.j);

            G.stack[z] = value;
            G.top = 0;
            int length, bredth;
            G.z = 1;

            int flag = 1;
            int current = value;
            G.d = 0;
            string p = "Path";
            for (int y = 0; y < G.count  ; y++)                                                                                 //recursively done untill all the food is eaten
            {
                p = p + "(" + G.i.ToString() + "," + G.j.ToString() + ")-->";
                
                G.i = current / 10000;
                G.j = current % 10000;
                G.z = 1;
                G.top = 0;
                for (int i = 0; i < 100000; i++)
                    G.stack[i] = 0;
                G.stack[0] = current;
                length = G.j * 20;
                bredth = G.i * 20;
                System.Threading.Thread.Sleep(50);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();




                for (; ; )
                {

                    G.d++;
                    int i = G.i;
                    int j = G.j;

                    flag = 0;
                    if (G.i != 0 && G.j != 0)
                    {
                        if (small[i, j - 1] == 2)
                        {
                                                                                                            //searches the surrounding node for everynode as to what is in the surrounding node
                            int sign = 1;
                            int d = position(i, j - 1);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;

                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i, j - 1] = 0;
                                flag++;
                                current = d;
                                small[i, j - 1] = 0;
                                break;

                            }
                        }
                        if (small[i, j - 1] == 0)
                        {
                            int sign = 1;
                            int d = position(i, j - 1);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;

                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;




                            }
                        }
                        if (small[i, j + 1] == 2)
                        {
                            int sign = 1;
                            int d = position(i, j + 1);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;
                                small[i, j + 1] = position(i, j);
                                flag++;
                                current = d;
                                small[i, j + 1] = 0;
                                break;
                            }
                        }
                        if (small[i, j + 1] == 0)
                        {
                            int sign = 1;
                            int d = position(i, j + 1);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;

                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;



                            }
                        }
                        if (small[i - 1, j] == 2)
                        {
                            int sign = 1;

                            int d = position(i - 1, j);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;
                                small[i - 1, j] = 0;
                                break;

                            }




                        }
                        if (small[i - 1, j] == 0)
                        {
                            int sign = 1;
                            int d = position(i - 1, j);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;

                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;



                            }
                        }

                        if (small[i + 1, j] == 2)
                        {
                            int sign = 1;
                            int d = position(i + 1, j);
                            for (int h = 0; h < G.z; h++)
                            {

                                if (G.stack[h] == d)
                                {
                                    sign = 0;
                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;
                                small[i + 1, j] = 0;
                                break;
                            }

                        }
                        if (small[i + 1, j] == 0)
                        {
                            int sign = 1;
                            int d = position(i + 1, j);
                            for (int h = 0; h < G.z; h++)
                            {
                                if (G.stack[h] == d)
                                {
                                    sign = 0;

                                }


                            }
                            if (sign == 1)
                            {
                                G.stack[G.z++] = d;

                                flag++;
                                current = d;



                            }
                        }
                        G.top++;
                        current = G.stack[G.top];

                        G.i = current / 10000;
                        G.j = current % 10000;
                    }
                }
                

            }
            MessageBox.Show(p + Environment.NewLine +"Nodes Expanded = " + G.d.ToString() + Environment.NewLine + "Path cost = " + G.path);//display the items in the messagebox whith the path cost
            
        }
    }
}
