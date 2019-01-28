using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tree.UnitTests
{
    [TestClass]
    public class TreeTests
    {
        //Tests the function that counts the number of nodes in a bst
        [TestMethod]
        public void NodeCount()
        {
            //Initialize the Node and Bst object
            Node root = null;
            BST tree = new BST();
            int[] list = { 55, 22, 77, 88, 11, 22, 44, 77, 55, 99, 22 };

            //populate the tree with the integers and call function to get a result
            foreach (int i in list)
            {
                root = tree.insert(root, i);
            }
            int result = tree.count(root);

            //test the result to see if they are equal
            Assert.AreEqual(result, 7);
        }

        //tests the function that finds how many levels are in a bst
        [TestMethod]
        public void TreeLevel()
        {
            Node root = null;
            BST tree = new BST();
            int[] list = { 55, 22, 77, 88, 11, 22, 44, 77, 55, 99, 22 };

            foreach (int i in list)
            {
                root = tree.insert(root, i);
            }
            int result = tree.FindLevel(root);

            Assert.AreEqual(result, 4);
        }

        //tests the function that calculates the theoretical minumum of a tree
        [TestMethod]
        public void TreeMinLevels()
        {
            Node root = null;
            BST tree = new BST();
            int[] list = { 55, 22, 77, 88, 11, 22, 44, 77, 55, 99, 22 };

            foreach (int i in list)
            {
                root = tree.insert(root, i);
            }
            int result = tree.MinLevels(root);

            Assert.AreEqual(result, 3);
        }
    }
}
