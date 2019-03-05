using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cpts321
{
    /// <summary>
    /// Takes an expression and creates a tree that can be evaluated 
    /// </summary>
    public class ExpressionTree
    {
        private Stack<Node> Tree = new Stack<Node>();
        private Dictionary<string, double> variables = new Dictionary<string, double>();
        private List<string> operators = new List<string>();

        /// <summary>
        /// constructor of the tree that constructs the tree based of the input expression string
        /// </summary>
        /// <param name="expression"></param>
        public ExpressionTree(string expression)
        {
            List<string> postFixExpression = ConvertPostfix(toList(expression));
            List<string> integers = loadIntList();
            double number; 

            foreach(string op in postFixExpression)
            {
                // a constant or a variable
                if(!operators.Contains(op))
                {
                    // a constant
                    if (double.TryParse(op, out number))
                    {
                        NumNode node = new NumNode();
                        node.Value = number;
                        Tree.Push(node);
                    }
                    // a variable
                    else
                    {
                        VarNode node = new VarNode();
                        variables.Add(op, 0);
                        node.Name = op;
                        Tree.Push(node);
                    }
                    
                }
                // its an operator
                else
                {
                    OpNode node = new OpNode(op);

                    if (Tree.Count != 0)
                    {
                        node.Right = Tree.Pop();
                        node.Left = Tree.Pop();
                    }
                    else
                    {
                        Console.WriteLine("The expression is invalid");
                    }
                    Tree.Push(node);
                }
            }

        }

        /// <summary>
        /// method to set a value to a variable in the expression string, stores value in the variables dictionary
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="variableValue"></param>
        public void SetVariable(string variableName, double variableValue)
        {
            variables[variableName] = variableValue;
        }

        // Precondition: n is non-null
        private double Evaluate(Node node)
        {
            // try to evaluate the node as a constant
            // the "as" operator is evaluated to null 
            // as opposed to throwing an exception
            NumNode constantNode = node as NumNode;
            if (null != constantNode)
            {
                return constantNode.Value;
            }

            // as a variable
            VarNode variableNode = node as VarNode;
            if (null != variableNode)
            {
                return variables[variableNode.Name];
            }

            // it is an operator node if we came here
            OpNode operatorNode = node as OpNode;
            if (null != operatorNode)
            {
                // but which one?
                switch (operatorNode.value)
                {
                    case "+":
                        return Evaluate(operatorNode.Left) + Evaluate(operatorNode.Right);
                    case "-":
                        return Evaluate(operatorNode.Left) - Evaluate(operatorNode.Right);
                    case "*":
                        return Evaluate(operatorNode.Left) * Evaluate(operatorNode.Right);
                    case "/":
                        return Evaluate(operatorNode.Left) / Evaluate(operatorNode.Right);
                    case "^":
                        return Math.Pow(Evaluate(operatorNode.Left), Evaluate(operatorNode.Right));
                    default: // if it is not any of the operators that we support, throw an exception:
                        throw new NotSupportedException(
                            "Operator " + operatorNode.value.ToString() + " not supported.");
                }
            }

            throw new NotSupportedException();
        }

        public double Evaluate()
        {
            return Evaluate(Tree.Peek());
        }        /// <summary>
        /// method that loads integers 0-9 into a list as strings
        /// </summary>
        /// <returns></returns>        private List<string> loadIntList()
        {
            List<string> integers = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                integers.Add(i.ToString());
            }

            return integers;
        }

        /// <summary>
        /// Converts a list of operators and operands into postfix order from infix order
        /// </summary>
        /// <param name="parsed"></param>
        /// <returns></returns>        private List<string> ConvertPostfix(List<string> parsed)
        {
            List<string> postFix = new List<string>(); 
            Stack<string> vars = new Stack<string>();

            foreach (string variable in parsed)
            {
                // if a variable or constant then add to the output list
                if(!operators.Contains(variable))
                {
                    postFix.Add(variable);
                }

                // its an operator
                else
                {
                    // if stack is empty then add operator to the stack
                    if (vars.Count == 0)
                    {
                        vars.Push(variable);
                    }
                    else
                    {
                        // if the priority is higher than the operator on the stack then pop and push current operator onto stack
                        while (Priority(vars.Peek()) >= Priority(variable) && Priority(variable) != -1)
                        {
                            postFix.Add(vars.Pop());
                            if (vars.Count == 0)
                            {
                                break;
                            }
                        }
                        vars.Push(variable);
                    }
                }
            }
            // add the rest of the operators from the stack into the output list
            while(vars.Count != 0)
            {
                postFix.Add(vars.Pop());
            }

            return postFix;
        }        // assigns priorty to each of the operators in the priority that we want to evaluate them at        private int Priority(string x)
        {
            if (x == "+" || x == "-")
            {
                return 1;
            }
            if (x == "*" || x == "/")
            {
                return 2;
            }
            else
            {
                return -1;
            }
        }        /// <summary>
        /// converts the expression string into a list containing each operator/operand as a string for easier postfix conversion
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>        private List<string> toList(string expression)
        {
            string variable = null;
            List<string> parsedExpression = new List<string>();

            foreach (char var in expression)
            {

                if (var == '+' || var == '-' || var == '*' || var == '/')
                {  
                    parsedExpression.Add(variable);
                    parsedExpression.Add(var.ToString());
                    operators.Add(var.ToString());
                    variable = null;
                }
                else
                {
                    variable += var;
                }
            }
            if (variable != null)
            {
                parsedExpression.Add(variable);
            }

            return parsedExpression;
        }
    }

    /// <summary>
    /// base node class
    /// </summary>
    public abstract class Node
    {
        
    }

    /// <summary>
    /// constant node class that stores doubles 
    /// </summary>
    public class NumNode : Node
    {
        public double Value { get; set; }
    }

    /// <summary>
    /// variable node class that stores variable names from the expression string
    /// </summary>
    public class VarNode : Node
    {
        public string Name { get; set; }
    }


    /// <summary>
    /// operator node class that stores the operators used in the expression string 
    /// </summary>
    public class OpNode : Node
    { 
        public OpNode(string op)
        {
            value = op;
            Left = null;
            Right = null;
        }

        public string value { get; set; }

        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}
