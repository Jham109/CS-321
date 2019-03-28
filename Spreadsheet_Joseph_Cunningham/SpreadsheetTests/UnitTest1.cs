using System;
using ExpressionEvaluator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpreadsheetTests
{
    [TestClass]
    public class ExpressionEvaluatorTests
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

        /// <summary>
        /// Tests to see if the expression tree can evalute an expression with parenthesis and different operators
        /// </summary>
        [TestMethod]
        public void TestParenthesis()
        {
            //initialize the tree with parenthesis
            ExpressionTree Tree = new ExpressionTree("(1+2)*3");

            //initialize the tree without parenthesis
            ExpressionTree Tree2 = new ExpressionTree("1+2*3");

            //test to see if the parenthesis tree evaluates correctly
            Assert.AreEqual(9, Tree.Evaluate());

            //test to make sure that the 2 trees dont equal (they shouldnt)
            Assert.AreNotEqual(Tree.Evaluate(), Tree2.Evaluate());

        }
    }

}
