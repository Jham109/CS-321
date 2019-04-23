using System;
using BSTTraversal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TraversalTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class TreeTests
        {
            //Tests the function that counts the number of nodes in a bst
            [TestMethod]
            public void RefPrintTest()
            {
                //Initialize the Node and Bst object
                Node root = null;
                BST tree = new BST();
                Random rand = new Random();

                //Insert 20 random ints between 0-100 into the tree
                for (int i = 0; i < 30; i++)
                {
                    root = tree.insert(root, rand.Next(0, 100));
                }
                tree.RecPrint(root);


                //test the result to see if they are equal
                Assert.IsNotNull(root);
            }

            //tests the function that finds how many levels are in a bst
            [TestMethod]
            public void StackPrintTest()
            {
                //Initialize the Node and Bst object
                Node root = null;
                BST tree = new BST();
                Random rand = new Random();

                //Insert 20 random ints between 0-100 into the tree
                for (int i = 0; i < 30; i++)
                {
                    root = tree.insert(root, rand.Next(0, 100));
                }
                tree.StackPrint(root);


                //test the result to see if they are equal
                Assert.IsNotNull(root);
            }

            //tests the function that calculates the theoretical minumum of a tree
            [TestMethod]
            public void HardPrintTest()
            {
                //Initialize the Node and Bst object
                Node root = null;
                BST tree = new BST();
                Random rand = new Random();

                //Insert 20 random ints between 0-100 into the tree
                for (int i = 0; i < 30; i++)
                {
                    root = tree.insert(root, rand.Next(0, 100));
                }
                tree.HardPrint(root);


                //test the result to see if they are equal
                Assert.IsNotNull(root);
            }
        }

    }
}
