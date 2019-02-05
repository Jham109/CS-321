using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinForms;
using System.Collections.Generic;

namespace WinFormsUnitTest
{
    [TestClass]
    public class WinFormsTests
    {
        //initialize the form object and other variables for testing
        Form1 f = new Form1();
        List<int> integers = new List<int>();
        Random rand = new Random();
        

        /// <summary>
        /// tests to see if the LoadList function loads all of the integers in the list;
        /// </summary>
        [TestMethod]
        public void LoadListTest()
        {  
            //load the list with integers
            integers = f.LoadList(integers, rand);

            //check if list was successfully loaded
            Assert.IsNotNull(integers[0]);
            //check to see if list contains 10,000 integers
            Assert.AreEqual(integers.Count, 10000);
        }

        /// <summary>
        /// tests the Task1 method which removes duplicates by using a HashSet
        /// </summary>
        [TestMethod]
        public void Task1Test()
        {
            //load the list with integers
            integers = f.LoadList(integers, rand);

            //call RemoveDuplicates to remove all of the duplicates in the list
            String distinct = f.Task1(integers);

            //test to see if the size of the lists are the same(they shouldnt be)
            Assert.AreNotEqual(distinct, integers.Count.ToString());
        }

        /// <summary>
        /// tests the Task2 method which removes duplicates while keeping O(1) storage complexity
        /// </summary>
        [TestMethod]
        public void Task2Test()
        {
            //load the list with integers
            integers = f.LoadList(integers, rand);

            //call RemoveDuplicates to remove all of the duplicates in the list
            String distinct = f.Task2(integers);

            //test to see if the size of the lists are the same(they shouldnt be)
            Assert.AreNotEqual(distinct, integers.Count.ToString());
        }

        /// <summary>
        /// tests the task3 method which removes duplicates while keeping O(n) time complexity and O(1) storage complexity
        /// </summary>
        [TestMethod]
        public void Task3Test()
        {
            //load the list with integers
            integers = f.LoadList(integers, rand);

            //call RemoveDuplicates to remove all of the duplicates in the list
            String distinct = f.Task3(integers);

            //test to see if the size of the lists are the same(they shouldnt be)
            Assert.AreNotEqual(distinct, integers.Count.ToString());
        }

        /// <summary>
        /// tests all methods to see if they output the same thing
        /// </summary>
        [TestMethod]
        public void AllTasksAreEqualTest()
        {
            //load the list 
            integers = f.LoadList(integers, rand);

            //call each of the tasks and set the outputs as strings
            string t1 = f.Task1(integers);
            string t2 = f.Task2(integers);
            string t3 = f.Task3(integers);

            //test to see if they all output the same thing (they should)
            Assert.AreEqual(t1, t2);
            Assert.AreEqual(t2, t3);
            Assert.AreEqual(t3, t1);
        }
    }
}
