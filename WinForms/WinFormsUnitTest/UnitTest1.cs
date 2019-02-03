using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinForms;
using System.Collections.Generic;

namespace WinFormsUnitTest
{
    [TestClass]
    public class WinFormsTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Task1Test()
        {  
            //initialize the form object and other variables for testing
            Form1 f = new Form1();
            List<int> integers = new List<int>();
            Random rand = new Random();

            //load the list and check if loaded
            integers = f.LoadList(integers, rand);
            Assert.IsNotNull(integers);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Task2Test()
        {
            //initialize the form object and other variables for testing
            Form1 f = new Form1();
            List<int> integers = new List<int>();
            Random rand = new Random();

            //load the list and check if loaded
            integers = f.LoadList(integers, rand);
            Assert.IsNotNull(integers);


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Task3Test()
        {
            //initialize the form object and other variables for testing
            Form1 f = new Form1();
            List<int> integers = new List<int>();
            Random rand = new Random();

            //load the list and check if loaded
            integers = f.LoadList(integers, rand);
            Assert.IsNotNull(integers);
        }
    }
}
