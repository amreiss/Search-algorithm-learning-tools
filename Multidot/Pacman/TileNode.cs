using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multidot
{
    public class TileNode
    {
        int position, g_score, parent, f_score;
        List<int> tileList, parentTileList;                     //for tracking 'snapshots of tiles' of current and parent node respectively; has position(x, y) type data

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

        public void setTileList(List<int> tileList)
        {
            this.tileList = tileList;
        }

        public List<int> getTileList()
        {
            return tileList;
        }

        public void setParentTileList(List<int> parentTileList)
        {
            this.parentTileList = parentTileList;
        }

        public List<int> getParentTileList()
        {
            return parentTileList;
        }

        public TileNode()                               //overriding the default constructor
        {
            tileList = new List<int>();
            parentTileList = new List<int>();
        }

        public TileNode(int position, int g_score, int parent, int f_score)                 //constructor method
        {
            this.position = position;
            this.g_score = g_score;
            this.parent = parent;
            this.f_score = f_score;
        }

        public TileNode(int position, int g_score, int parent, int f_score, List<int> tileList, List<int> parentTileList)       //constructor method
        {
            this.position = position;
            this.g_score = g_score;
            this.parent = parent;
            this.f_score = f_score;
            this.tileList = tileList;
            this.parentTileList = parentTileList;
        }

        public bool Equals(TileNode node)
        {
            if (position.Equals(node.getPosition()) && g_score.Equals(node.getG_score()) && parent.Equals(node.getParent()) && f_score.Equals(node.getF_score()))
            {
                if (tileList == null && node.getTileList() == null)
                {
                    if (parentTileList == null && node.getParentTileList() == null)
                    {
                        return true;
                    }
                    else
                    {
                        if (parentTileList.Equals(node.getParentTileList()))
                            return true;
                    }
                }
                else
                {
                    if (tileList.Equals(node.getTileList()))
                    {
                        if (parentTileList == null && node.getParentTileList() == null)
                        {
                            return true;
                        }
                        else
                        {
                            if (parentTileList.Equals(node.getParentTileList()))
                                return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
