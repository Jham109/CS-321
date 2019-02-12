//Joseph Cunningham - 11511536
using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotePadApp;

namespace NotepadUnit.Tests
{
    [TestClass]
    public class FibonacciTextReaderTests
    {
        [TestMethod]
        public void ReadlineTest()
        {
            //initialize variables 
            FibonacciTextReader fib = new FibonacciTextReader(50);
            string line = fib.ReadLine();
            BigInteger fib50 = 7778742049;

            Assert.IsNotNull(fib);// check to see if initialized correctly

            
            for (int i = 0; i < 50; i++)
            {
                line = fib.ReadLine();
            }

            Assert.AreEqual(line, "50: " + fib50.ToString()); // check to see if it outputs the right thing for sequence 50


            fib.Close();
         
        }
    }
}
