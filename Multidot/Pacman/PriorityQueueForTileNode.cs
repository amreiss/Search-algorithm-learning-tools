using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multidot
{
    class PriorityQueueForTileNode
    {
        List<TileNode> queue;

        public TileNode Dequeue()
        {
            if (queue.Count == 0)
                return null;

            TileNode temp = queue.ElementAt(0);
            queue.RemoveAt(0);
            return temp;
        }

        public void Enqueue(TileNode TileNode)
        {
            bool insertFlag = false;                //denotes whether the TileNode has been inserted (or not)

            if (queue.Count == 0)                   //simply add the TileNode if the PriorityQueue is empty
            {
                queue.Add(TileNode);
            }
            else
            {
                for (int i = 0; i < queue.Count; i++)
                {
                    if (queue.ElementAt(i).getF_score() >= TileNode.getF_score())
                    {
                        queue.Insert(i, TileNode);
                        insertFlag = true; break;
                    }
                }

                if (insertFlag == false)
                {
                    queue.Add(TileNode);                 //if TileNode wasn't inserted anywhere, simply append it
                }
            }
        }

        public void printQueue()
        {
            Console.WriteLine("Position | G_score | Parent | F_score");

            foreach (TileNode TileNode in queue)
            {
                Console.WriteLine(TileNode.getPosition() + " | " + TileNode.getG_score() + " | " + TileNode.getParent() + " | " + TileNode.getF_score());
            }
        }

        public bool isEmpty()
        {
            if (queue.Count() == 0)
                return true;

            return false;
        }

        public int indexOf(int position)                //returns the index where it finds position in the queue
        {
            int index = -1;

            for (int i = 0; i < queue.Count; i++)
            {
                if (queue[i].getPosition() == position)
                {
                    index = i;
                }
            }
            return index;
        }

        public void removeAt(int index)
        {
            queue.RemoveAt(index);
        }

        public List<TileNode> getQueue()
        {
            return queue;
        }

        public PriorityQueueForTileNode()                          //default constructor
        {
            queue = new List<TileNode>();
        }

        public int indexOfComplex(TileNode TileNode)            //returns the index where it finds this TileNode in the queue, taking into account the (position+food list)
        {
            int index = -1;

            for (int i = 0; i < queue.Count; i++)
            {
                if (queue[i].getPosition() + String.Join(",", queue[i].getTileList()) == TileNode.getPosition() + String.Join(",", TileNode.getTileList()))
                {
                    index = i;
                }
            }
            return index;
        }

        public void printQueueComplex()
        {
            //Console.WriteLine("Position | G_score | Parent | F_score | food | parentFood");

            foreach (TileNode TileNode in queue)
            {
                Console.WriteLine(TileNode.getPosition() + " | " + TileNode.getG_score() + " | " + TileNode.getParent() + " | " + TileNode.getF_score() + "|" + String.Join(",", TileNode.getTileList()) + "|" + String.Join(",", TileNode.getParentTileList()));
            }
        }
    }
}
