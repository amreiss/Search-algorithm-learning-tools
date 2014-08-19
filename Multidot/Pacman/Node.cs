using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    public class Node
    {
        int position, g_score, parent, f_score;
        List<int> food, parentFood;                     //for tracking 'snapshots of food spots' of current and parent node respectively; has position(x, y) type data

        //getters and setters
        public void setPosition(int position)
        {
            this.position = position;
        }

        public int getPosition()
        {
            return this.position;
        }

        public void setG_score(int g_score)
        {
            this.g_score = g_score;
        }

        public int getG_score()
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

        public void setF_score(int f_score)
        {
            this.f_score = f_score;
        }

        public int getF_score()
        {
            return this.f_score;
        }

        public void setFood(List<int> food)
        {
            this.food = food;
        }
        
        public List<int> getFood()
        {
            return food;
        }

        public void setParentFood(List<int> parentFood)
        {
            this.parentFood = parentFood;
        }

        public List<int> getParentFood()
        {
            return parentFood;
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
            food = new List<int>();
            parentFood = new List<int>();
        }

        public Node(int position, int g_score, int parent, int f_score)                 //constructor method
        {
            this.position = position;
            this.g_score = g_score;
            this.parent = parent;
            this.f_score = f_score;
        }

        public Node(int position, int g_score, int parent, int f_score, List<int> food, List<int> parentFood)       //constructor method
        {
            this.position = position;
            this.g_score = g_score;
            this.parent = parent;
            this.f_score = f_score;
            this.food = food;
            this.parentFood = parentFood;
        }

        public bool Equals(Node node)
        {
            if (position.Equals(node.getPosition()) && g_score.Equals(node.getG_score()) && parent.Equals(node.getParent()) && f_score.Equals(node.getF_score()))
            {
                if (food == null && node.getFood() == null)
                {
                    if (parentFood == null && node.getParentFood() == null)
                    {
                        return true;
                    } else
                    {
                        if (parentFood.Equals(node.getParentFood()))
                            return true;
                    }
                } else {
                    if (food.Equals(node.getFood()))
                    {
                        if (parentFood == null && node.getParentFood() == null)
                        {
                            return true;
                        }
                        else
                        {
                            if (parentFood.Equals(node.getParentFood()))
                                return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}