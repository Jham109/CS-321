using System;
using Cpts321;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpTreeTests
{
    [TestClass]
    public class UnitTest1
    {
        

        /// <summary>
        /// Tests the evalutate method of an expreesion tree initialized with all constants
        /// should return a double representing the evalutated expression 
        /// </summary>
        [TestMethod]
        public void TestConstants()
        {
            //initialize the expression tree with a basic expression
            ExpressionTree Tree = new ExpressionTree("3+3+4");

            // the new evaluted expression should equal 10 
            Assert.AreEqual(10, Tree.Evaluate());
        }

        /// <summary>
        /// Tests the evalutate and setVariable method of an expreesion tree initialized with all constants
        /// should return a double representing the evalutated expression 
        /// </summary>
        [TestMethod]
        public void TestVariables()
        {
            //initialize the expression tree with a basic expression
            ExpressionTree Tree = new ExpressionTree("a+b");

            // the default values of variables are 0, so the expression should evaluate to 0 before any variable is set
            Assert.AreEqual(0, Tree.Evaluate());

            // set each variable to a value using the setVariable method
            Tree.SetVariable("a", 2);
            Tree.SetVariable("b", 2);

            // the new evaluted expression should equal 4 if each variable was set correctly
            Assert.AreEqual(4, Tree.Evaluate());
        }
    }
}
