using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tree.UnitTests
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void NodeCount()
        {
            Node root = null;
            BST tree = new BST();
            int[] list = { 55, 22, 77, 88, 11, 22, 44, 77, 55, 99, 22 };

            foreach (int i in list)
            {
                root = tree.insert(root, i);
            }
            int result = tree.count(root);

            Assert.AreEqual(result, 7);
        }

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
