using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cpts321
{
    public class ExpressionTree
    {
        private Stack<Node> Tree = new Stack<Node>();
        private Dictionary<string, double> variables = new Dictionary<string, double>();
        private List<string> operators = new List<string>();


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

        public void SetVariable(string variableName, double variableValue)
        {
            variables[variableName] = variableValue;
        }        public double Evaluate()
        {
            return 0;
        }        private List<string> loadIntList()
        {
            List<string> integers = new List<string>();

            for(int i =0; i<10; i++)
            {
                integers.Add(i.ToString());
            }

            return integers;
        }        private List<string> ConvertPostfix(List<string> parsed)
        {
            List<string> postFix = new List<string>(); 
            Stack<string> vars = new Stack<string>();

            foreach (string variable in parsed)
            {
                if(!operators.Contains(variable))
                {
                    postFix.Add(variable);
                }
                else
                {
                    if (vars.Count == 0)
                    {
                        vars.Push(variable);
                    }
                    else
                    {
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

            while(vars.Count != 0)
            {
                postFix.Add(vars.Pop());
            }

            return postFix;
        }        private int Priority(string x)
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
        }        private List<string> toList(string expression)
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

    public abstract class Node
    {
        
    }

    public class NumNode : Node
    {
        public double Value { get; set; }
    }

    public class VarNode : Node
    {
        public string Name { get; set; }
    }

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
