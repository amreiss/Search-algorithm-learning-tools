using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    public class Node
    {
        int position, parent;
        double g_score, f_score;

        //getters and setters
        public void setPosition(int position)
        {
            this.position = position;
        }

        public int getPosition()
        {
            return this.position;
        }

        public void setG_score(double g_score)
        {
            this.g_score = g_score;
        }

        public double getG_score()
        {
            return this.g_score;
        }

        public void setParent(int parent)
        {
            this.parent = parent;
        }

        public int getParent()
        {
            return this.parent;
        }

        public void setF_score(double f_score)
        {
            this.f_score = f_score;
        }

        public double getF_score()
        {
            return this.f_score;
        }

        public void reset()                         //for resetting the node
        { 
            position = -1;
            g_score = -1;
            parent = -1;
            f_score = -1;
        }

        public Node()                               //overriding the default constructor
        {
        }

        public Node(int position, double g_score, int parent, double f_score)                 //constructor method
        {
            this.position = position;
            this.g_score = g_score;
            this.parent = parent;
            this.f_score = f_score;
        }
    }
}
