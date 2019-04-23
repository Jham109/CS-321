using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTTraversal
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize the root node and tree object
            Node root; ;
            BST tree;
            string cont = "y";

            while (cont == "y")
            {
                //reinitialize the objects everytime the user wants a new tree
                tree = new BST();
                root = null;
                Random rand = new Random();

                //Insert 20 random ints between 0-100 into the tree
                for (int i = 0; i < 30; i++)
                {
                    root = tree.insert(root, rand.Next(0, 100));
                }

                //print out the tree recursively
                Console.WriteLine("Traversal of the tree using recursion");
                tree.RecPrint(root);
                Console.WriteLine();

                //print out the tree without recursion using a stack
                Console.WriteLine("Traversal of the tree with a stack and no recursion");
                tree.StackPrint(root);
                Console.WriteLine();

                //print without using a stack or recursion
                Console.WriteLine("Traversal of the tree without a stack and recursion");
                tree.HardPrint(root);
                Console.WriteLine();

                Console.WriteLine("New Tree?(y/n)");
                cont = Console.ReadLine();
            }

        }
    }

    /// <summary>
    /// Node object stores a value and its neighboring nodes for the tree
    /// </summary>
    public class Node
    {
        public int value;
        public Node right;
        public Node left;

        public Node(int newVal)
        {
            value = newVal;
            right = left = null;
        }
    }

    /// <summary>
    /// BST object previousforms different algorithms on the tree and constructs the tree
    /// </summary>
    public class BST
    {
        public int nodeCount = 0;
        public int levels = 0;

        public BST()
        {
        }

        //recursive funtion to insert a Node at into the BST
        public Node insert(Node root, int value)
        {
            if (root == null)
            {
                root = new Node(value);
            }
            else if (value == root.value)
            {
                return root;
            }
            else if (value > root.value)
            {
                root.right = insert(root.right, value);
            }
            else
            {
                root.left = insert(root.left, value);
            }
            return root;
        }

        //Prints the contents of the tree recursively IN ORDER
        public void RecPrint(Node root)
        {
            if (root == null)
            {
                return;
            }

            RecPrint(root.left);
            Console.Write(root.value);
            Console.Write(' ');
            RecPrint(root.right);
        }

        //Prints the contents of the tree using a Stack
        public void StackPrint(Node root)
        {
            Stack<Node> order = new Stack<Node>();
            Node current = root;

            while (current != null || order.Count != 0)
            {

                //Reach the left most node
                while(current != null)
                {
                    order.Push(current);
                    current = current.left;
                }

                //print out the left most item
                current = order.Pop();
                Console.Write(current.value);
                Console.Write(' ');

                current = current.right;

            }

        }

        //Prints out the contents of the tree without using a stack and recursion
        public void HardPrint(Node root)
        {
            Node current = root;
            Node previous;
            
            while(current != null)
            {
                //if no left child
                if(current.left == null)
                {
                    Console.Write(current.value);
                    Console.Write(' ');

                    current = current.right;
                }
                else
                {
                    previous = current.left;
                    while (previous.right != null && previous.right != current)
                    {
                        previous = previous.right;
                    }

                    if (previous.right == null)
                    {
                        previous.right = current;
                        current = current.left;
                    }

                    else
                    {
                        previous.right = null;
                        Console.Write(current.value);
                        Console.Write(' ');
                        current = current.right;
                    }
                }
            }
        }

      


    }

}
