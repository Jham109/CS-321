using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bst
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize the root node and tree object
            int i;
            Node root = null;
            BST tree = new BST();

            //prompt user for input and read in as a string
            Console.WriteLine("Insert a string of integers from range [0,100], each number seperated by a space");
            string ints = Console.ReadLine();

            //convert string into ints and read them into the tree
            foreach (string num in ints.Split(' '))
            {
                i = Convert.ToInt32(num);
                root = tree.insert(root, i);
            }

            //output the all of the information onto the screen
            tree.print(root);
            Console.Write('\n');

            int nodeNum = tree.count(root);
            Console.Write("Number of Nodes: ");
            Console.WriteLine(nodeNum);

            int treeLevel = tree.FindLevel(root);
            Console.Write("Levels: ");
            Console.WriteLine(treeLevel);

            int min = tree.MinLevels(root);
            Console.Write("Theoretical minimum levels of tree: ");
            Console.WriteLine(min);

            Console.ReadLine();

        }
    }

    class Node
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

    class BST
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
        public void print(Node root)
        {
            if (root == null)
            {
                return;
            }

            print(root.left);
            Console.Write(root.value);
            Console.Write(' ');
            print(root.right);
        }

        //function for counting the number of nodes in the tree
        public int count(Node root)
        {
            if (root == null)
            {
                return 0;
            }

            count(root.left);
            nodeCount++;
            count(root.right);
            return nodeCount;
        }

        //function for finding the Level of the tree
        public int FindLevel(Node root)
        { 
            int heightLeft = 0;
            int heightRight = 0;

            if(root == null)
            {
                return 0;
            }

            heightLeft = FindLevel(root.left);
            heightRight = FindLevel(root.right);
         
            if(heightLeft > heightRight)
            {
                return heightLeft + 1;
            }
            else
            {
                return heightRight +1;
            }
        }

        //function for finding the theoretical minimum amount of levels based on amount of nodes
        public int MinLevels(Node root)
        {
            if(root == null)
            {
                return 0;
            }
            return ((int)Math.Log(nodeCount, 2)+1);
        }

    }
}
