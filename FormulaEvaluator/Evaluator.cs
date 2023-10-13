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
        /// <summary>
        /// converts vars to numbers
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public delegate double Lookup(string s);
        /// <summary>
        ///evaluates a given expression down to a single number
        /// </summary>
        /// <remarks>
        ///		<para>variables should begin with a letter</para>
        ///		<para></para>
        ///		<para></para>
        /// </remarks>
        /// <exception cref="ArgumentException">Throws if division by zero occurs OR expression is missing open parantheses OR an entered variable is formatted incorrectly</exception>
        /// <param name="exp">the expression given by the user</param>
        /// <param name="variableEvaluator">function for putting integer values in place of variables</param>
        /// <returns>Long. the result of the expression</returns>
        /// <exception cref="ArgumentException">Thrown if  a variable can't be parsed
        /// or the given formula isn't formatted correctly</exception>
        /// <exception cref="DivideByZeroException"> thrown if division by zero oc curs</exception>
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

                    // ----------------catch variables--------------------\\
                    //treating substrings as a 2d array
                    //Regex varChecker = new Regex("/^m([0-9]*)$/");
                    //varChecker.
                    //	if first char is a letter, proceed with checking 
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
                            throw new ArgumentException("missing open parantheses");
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

        /// <summary>
        /// handy helper to do math as we go.
        /// </summary>
        /// <param name="left"> left operand</param>
        /// <param name="op"> operator</param>
        /// <param name="right"> right operand</param>
        /// <returns> result of math operation</returns>
        private static double Math(double left, string op, double right)
        {
            double result = 0;
            switch (op)
            {
                case "*":
                    result = left * right;
                    break;
                case "/":
                    if (right == 0)
                    {
                        throw new DivideByZeroException("error: divide by zero");
                    }
                    result = left / right;
                    break;
                case "+":
                    result = left + right;
                    break;
                case "-":
                    result = left - right;
                    break;
            }
            return result;
        }
    }
}