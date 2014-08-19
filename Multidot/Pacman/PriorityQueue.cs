using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Pacman
{
    class PriorityQueue
    {
        List<Node> queue;

        public Node Dequeue()
        {
            if (queue.Count == 0)
                return null;

            Node temp = queue.ElementAt(0);
            queue.RemoveAt(0);
            return temp;
        }

        public void Enqueue(Node node)
        {
            bool insertFlag = false;                //denotes whether the node has been inserted (or not)

            if (queue.Count == 0)                   //simply add the node if the PriorityQueue is empty
            {
                queue.Add(node);
            }
            else
            {
                for (int i = 0; i < queue.Count; i++)
                {
                    if (queue.ElementAt(i).getF_score() >= node.getF_score())
                    {
                        queue.Insert(i, node);
                        insertFlag = true; break;
                    }
                }

                if (insertFlag == false)
                {
                    queue.Add(node);                 //if node wasn't inserted anywhere, simply append it
                }
            }
        }

        public void printQueue()
        {
            Console.WriteLine("Position | G_score | Parent | F_score");

            foreach (Node node in queue)
            {
                Console.WriteLine(node.getPosition() + " | " + node.getG_score() + " | " + node.getParent() + " | " + node.getF_score());
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

            for (int i=0; i<queue.Count;i++)
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

        public List<Node> getQueue()
        {
            return queue;
        }

        public PriorityQueue()                          //default constructor
        {
            queue = new List<Node>();
        }

        public int indexOfComplex(Node node)            //returns the index where it finds this node in the queue, taking into account the (position+food list)
        {
            int index = -1;

            for (int i = 0; i < queue.Count; i++)
            {
                if (queue[i].getPosition()+ String.Join(",",queue[i].getFood()) == node.getPosition()+ String.Join(",",node.getFood()))
                {
                    index = i;
                }
            }
            return index;
        }

        public void printQueueComplex()
        {
            //Console.WriteLine("Position | G_score | Parent | F_score | food | parentFood");

            foreach (Node node in queue)
            {
                Console.WriteLine(node.getPosition() + " | " + node.getG_score() + " | " + node.getParent() + " | " + node.getF_score() + "|" + node.getFood().Count + "|" + node.getParentFood().Count);
            }
        }
    }
}
