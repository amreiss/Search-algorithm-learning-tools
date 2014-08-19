using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Queue
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
            queue.Add(node);
        }

        public void printQueue()
        {
            Console.WriteLine("Position | G_score | Parent | F_score");

            foreach (Node node in queue)
            {
                Console.WriteLine(node.getPosition() + " | " + node.getG_score() + " | " + node.getParent() + " | " + node.getF_score());
            }
        }

        public Queue()                          //default constructor
        {
            queue = new List<Node>();
        }
    }
}
