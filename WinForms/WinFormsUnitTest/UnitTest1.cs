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
        List<int> distinct = new List<int>();
        Random rand = new Random();
        

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void LoadListTest()
        {  
            //load the list with integers
            integers = f.LoadList(integers, rand);

            //check if list was successfully loaded
            Assert.IsNotNull(integers[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void RemoveDuplicatesTest()
        {
            //load the list with integers
            integers = f.LoadList(integers, rand);

            //call RemoveDuplicates to remove all of the duplicates in the list
            distinct = f.RemoveDuplicates(integers);

            //test to see if the size of the lists are the same(they shouldnt be)
            Assert.AreNotEqual(distinct.Count, integers.Count);


        }

        /// <summary>
        /// 
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
