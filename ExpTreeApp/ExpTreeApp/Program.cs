using Cpts321;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTreeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = 1;
            string expression = "A1+B1+C1";
            ExpressionTree Tree = new ExpressionTree(expression);

            // main loop of the program 
            while (input != 4)
            {
                Console.Write("Current Expression: ");
                Console.WriteLine(expression);
                menu();
                Console.Write("Enter a command: ");
                input = Convert.ToInt32(Console.ReadLine());
                
                switch (input)
                {
                    case 1: // Creates a new expression tree with a new expression
                        Console.Write("Enter a new Expression: ");
                        expression = Console.ReadLine();
                        Tree = new ExpressionTree(expression);
                        break;

                    case 2: // assign values and names to variables in the expression
                        Console.Write("Enter a variable name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter a value: ");
                        int value = Convert.ToInt32(Console.ReadLine());
                        Tree.SetVariable(name, value);
                        break;

                    case 3: // evaluate the current expression in the tree
                        Console.WriteLine(Tree.Evaluate());
                        break;
                }
            }

        }

        /// <summary>
        /// Prints out the menu that prompts the user for what to do next
        /// </summary>
        static void menu()
        {
            Console.WriteLine("*************MENU*************");
            Console.WriteLine("1. Enter an expression string");
            Console.WriteLine("2. Set a variable value");
            Console.WriteLine("3. Evaluate the expression");
            Console.WriteLine("4. Quit");
            Console.WriteLine("******************************");
        }
    }
}
