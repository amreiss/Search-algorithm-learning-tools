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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBFS_Click(object sender, EventArgs e)
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
            int length, bredth;
            G.z = 1;


            while (G.stack[G.top] != G.i)
            {
                int current = G.stack[G.top];
                length = G.j * 20;
                bredth = G.i * 20;

                System.Threading.Thread.Sleep(100);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();


                G.i = current / 10000;
                G.j = current % 10000;
                {
                    newnode(small, G.stack, G.i, G.j, G.z);
                    G.top++;
                }


                
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

        public int position(int x, int y)
        {
            int result = 10000 * x + y;
            return result;
        }
        public int position1(int x, int y)
        {
            int result = 100 * x + y;
            
            return result;
        }
       
        public void newnode( int[,] small,int[] stack,int i, int j,int z)
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
            int length, bredth;
            G.z = 0;
            


            while (G.stack[G.top] != G.i)
            {
                int current = G.stack[G.z];
                //small[3, 11] = value;


                G.i = current / 10000;
                G.j = current % 10000;

                {
                    int flag = newnode1(small, G.stack, G.i, G.j, G.z);
                    G.z--;
                }


                length = G.j * 20;
                bredth = G.i * 20;

                System.Threading.Thread.Sleep(100);
                pictureBox1.Location = new Point(length, bredth);
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

        private void button2_Click(object sender, EventArgs e)
        {
            var small = new int[10, 22] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            var nsmall = new int[10,22] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1 }, { 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            
            for (int k = 0; k < 10; k++)
            {
                for (int l = 0; l < 22; l++)
                {
                    if (nsmall[k, l] == 0)
                    {
                        int val = (8 - k);
                        int val2=(1 - l);
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
            
            G.i = 3; G.j = 11;
            G.stack = new int[100000];
            for (G.i = 0; G.i < 100000; G.i++)
                G.stack[G.i] = 100000000;
            int z = 0;

            G.i = 3; G.j = 11;
           
            int value = position1(G.i, G.j);

            G.stack[z] = value;
            G.top = 0;
            int length, bredth;
            G.z = 0;




            for(;;)
            {
                int current = G.stack[0];


                
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
                    int flag = newnode2(small,nsmall, G.stack, G.i, G.j, G.z);

                    
                }


                length = G.j * 20;
                bredth = G.i * 20;

                System.Threading.Thread.Sleep(100);
                System.Threading.Thread.Sleep(100);
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(length, bredth, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();
                if (current%10000 == 801)
                {
                    MessageBox.Show("i have reached");
                    break;
                }

            }
            G.i = 8;
            G.j = 1;
            
            for (int k = 0; k < 10000; k++)
            {
                int current = small[G.i, G.j];
                
                if (small[G.i, G.j] == 0)
                {
                    break;
                }
                
                G.j = current % 100;

                G.i = current % 10000;
                G.i = G.i - G.j;
                G.i = G.i / 100;
                
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                formGraphics.FillRectangle(myBrush, new Rectangle(G.j*20,G.i*20, 20, 20));
                myBrush.Dispose();
                formGraphics.Dispose();


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







    }
}