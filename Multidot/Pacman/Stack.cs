using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Stack
    {
        Stack<Node> stack;

        public void printStack()
        {
            //Console.WriteLine("Position | G_score | Parent | F_score | food | parentFood");

            foreach (Node node in stack)
            {
                Console.WriteLine(node.getPosition() + " | " + node.getG_score() + " | " + node.getParent() + " | " + node.getF_score() + "|" + node.getFood().Count + "|" + node.getParentFood().Count);
            }
        }

        public Stack()                          //default constructor
        {
            stack = new Stack<Node>();
        }

        public bool isEmpty()
        {
            if (stack.Count() == 0)
                return true;

            return false;
        }

        public void Push(Node node) 
        {
            stack.Push(node);
        }

        public Node Pop()
        {
            return stack.Pop();
        }

    }
}
