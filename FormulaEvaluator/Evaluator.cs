using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormulaEvaluator
{
    /// <summary>
    /// <author> Sangeun An </author>
    /// Evaluates a function using an infix algorithm.
    /// </summary>
    public static class Evaluator
    {

        public delegate double Lookup(string s);
        public static double Evaluate(string expression, Func<string, double> variableEvaluator)
        {
            Stack<string> operators = new Stack<string>();
            Stack<double> values = new Stack<double>();
            //periodically we need to keep operands in doubles for operations
            //like subtraction and division
            double op1 = 0;
            double op2 = 0;


            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
                                                                                                                            
            for (int i = 0; i < substrings.Length; i++)
            {
                //catch the occasional whitespace control character (empty space).
                if (substrings[i] == "")
                {
                    continue;
                }
                double num = 0;
                //read the substrings


                //parse out everything else
                if (double.TryParse(substrings[i], out num))
                {
                    //mult/div if necessary
                    //check stacks
                    if (values.Count > 0 && operators.Count > 0 && (operators.Peek() == "*" || operators.Peek() == "/"))
                    {

                        values.Push(Math(values.Pop(), operators.Pop(), num));
                    }
                    else
                    {
                        values.Push(num);
                    }

                }
                //if we had something with chars or symbols in it, we end up here
                else
                {
                    //Char.IsLetter(substrings[i][0]) || substrings[i][0].Equals("_")
                    if (Regex.IsMatch(substrings[i], @"[a-zA-Z]+\d+"))
                    {
                        double varval = variableEvaluator(substrings[i]);
                        if (values.Count > 0 && operators.Count > 0 && (operators.Peek() == "*" || operators.Peek() == "/"))
                        {
                            values.Push(Math(values.Pop(), operators.Pop(), varval));
                        }
                        else
                        {
                            values.Push(varval);
                        }
                    }



                    //catch operators
                    if (substrings[i] == "/" || substrings[i] == "*")
                    {
                        operators.Push(substrings[i]);
                    }
                    if (substrings[i] == "+" || substrings[i] == "-")
                    {
                        //if there are two nums and an operator
                        if (operators.Count > 0 && values.Count > 1 && (operators.Peek() == "+" || operators.Peek() == "-"))
                        {
                            op1 = values.Pop();
                            op2 = values.Pop();
                            values.Push(Math(op2, operators.Pop(), op1));
                        }
                        operators.Push(substrings[i]);

                    }
                    if (substrings[i] == "(")
                    {
                        operators.Push(substrings[i]);

                    }
                    if (substrings[i] == ")")
                    {
                        //note: we should never push ")" onto the opstack
                        //if there are two nums and an operator
                        if (operators.Count > 0 && values.Count > 1 && (operators.Peek() == "+" || operators.Peek() == "-"))
                        {
                            op1 = values.Pop();
                            op2 = values.Pop();
                            values.Push(Math(op2, operators.Pop(), op1));
                        }

                        //"Next, the top of the operator stack should be a (. Pop it."
                        if ((operators.Count == 0 || operators.Peek() != "("))
                        {
                            throw new ArgumentException("no matching parenthesis");
                        }
                        else
                        {
                            operators.Pop();
                        }
                        // if * or / is at the top of the operator stack,
                        if (values.Count > 1 && operators.Count > 0 && (operators.Peek() == "*" || operators.Peek() == "/"))
                        {
                            //get parts
                            op1 = values.Pop();
                            op2 = values.Pop();
                            values.Push(Math(op2, operators.Pop(), op1));
                        }


                    }

                }

            }//end for loop
             //report result
            if (values.Count == 1 && operators.Count == 0)
            {
                return values.Pop();
            }
            //leftover addition? 
            else
            {
                if (operators.Count == 1 && values.Count == 2)
                {

                    op1 = values.Pop();
                    op2 = values.Pop();
                    return Math(op2, operators.Pop(), op1);
                }
                else
                {
                    throw new ArgumentException("Error. Expression could not be simplified");
                }
            }


        }
        private static double Math(double left, string oprt, double right)
        {
            double result = 0;

            if (oprt == "*")
            {
                result = left * right;
            }

            else if (oprt == "/")
            {
                try
                {
                    result = left / right;
                }
                catch(DivideByZeroException)
                {
                    Console.WriteLine("You cannot divide by zero!");
                }
            }  

            else if (oprt == "+")
            {
                result = left + right;
            }

            else if (oprt == "-")
            {
                result = left - right;
            }

            return result;
        }
    }
}