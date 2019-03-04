using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cpts321
{
    public class ExpressionTree
    {
        private Node root;
        private Dictionary<string, double> variables = new Dictionary<string, double>();
        private List<string> operators = new List<string>();


        public ExpressionTree(string expression)
        {
            List<string> parsedExpression = toList(expression);
            string postFixExpression = ConvertPostfix(parsedExpression);
            Console.WriteLine(postFixExpression);

        }

        public void SetVariable(string variableName, double variableValue)
        {
            variables[variableName] = variableValue;
        }        public double Evaluate()
        {
            return 0;
        }        private string ConvertPostfix(List<string> parsed)
        {
            string postFix = null;
            Stack<string> vars = new Stack<string>();

            foreach (string variable in parsed)
            {
                if(!operators.Contains(variable))
                {
                    postFix += variable;
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
                            postFix += vars.Pop();
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
                postFix += vars.Pop();
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
        double val = 0;
        public NumNode(double num)
        {
            val = Convert.ToInt32(num);
        }

        public double Value
        {
            get{ return val; }
        }
    }

    public class VarNode : Node
    {
        public string Name { get; set; }
    }

    public class OpNode : Node
    {
        public Node left;
        public Node right;
        public char value;

        public OpNode(char op)
        {
            value = op;
            left = null;
            right = null;
        }

        
    }
}
