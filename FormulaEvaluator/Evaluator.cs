/// <summary> /// <summary> 
/// Author:    Sangeun An
/// Partner:   None 
/// Date:      9/13/2023
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Sangeun An - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Sangeun An, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
///    [This is the library to perform the evaluating formula expression which is written in string datatype.] 
/// </summary>

/// <summary>
///   The function evaluates arithmetic expression by infix notation, which is built through two main stacks: value stack and the operator stack.
/// 
/// </summary>
/// <param name="t"> t represents the first elements from the expression. </param>
/// <param name="v1"> v1 represents the first element of the value stack </param>
/// <param name="v2"> v2 represents the second element of the value stack </param>
/// <returns> The function will return the value of the calculated value, which is the top element of the value stack. </returns>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace FormulaEvaluator
{
    public class Evaluator
    {
        public delegate int Lookup(String variable_name);
        public static int Evaluate(string expression, Lookup variableEvaluator)
        {
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            Stack<string> operators = new Stack<string>();
            Stack<int> value = new Stack<int>();

            foreach (string t in substrings)
            {
                bool isNumeric = int.TryParse(t, out int num);

                //Condition1: t in an integer
                if (isNumeric)
                {
                    if (value.Count > 0 && operators.Count() > 0 && operators.Peek() == "*")
                    {
                        int v1 = value.Pop();
                        operators.Pop();
                        int val = v1 * num;
                        value.Push(val);
                    }
                    else if (value.Count > 0 && operators.Count() > 0 && operators.Peek() == "/")
                    {
                        int v1 = value.Peek();
                        operators.Pop();
                        try
                        {
                            int val = v1 / num;
                            value.Pop();
                            value.Push(val);
                        }
                        catch (DivideByZeroException)
                        {
                            Console.WriteLine("division by 0");
                        }
                    }
                    else
                    {
                        value.Push(num);
                    }
                }

                //Condition2: t is + or -
                else if (t == "+" || t == "-")
                {
                    if (value.Count > 1 && operators.Count > 0)
                    {
                        int v1 = value.Pop();
                        int v2 = value.Pop();
                        string oper = operators.Pop();

                        if (oper == "+")
                        {
                            value.Push(v2 + v1);
                        }
                        else if (oper == "-")
                        {
                            value.Push(v2 - v1);
                        }
                        else
                        {
                            operators.Push(t);
                        }
                    }
                    else
                    {
                        operators.Push(t);
                    }

                }

                //Condition3: t is * or /
                else if (t == "*" || t == "/")
                {
                    operators.Push(t);
                }

                //Condigion4: t is (
                else if (t == "(")
                {
                    operators.Push(t);
                }

                //Condition5: t is )
                else if ((operators.Count > 0 && t == ")"))
                {
                    if ((value.Count() > 1 && operators.Peek() == "+") || (value.Count() > 1 && operators.Peek() == "-"))
                    {
                        int v1 = value.Pop();
                        int v2 = value.Pop();
                        string oper = operators.Pop();

                        if (oper == "+")
                        {
                            value.Push(v2 + v1);

                        }
                        else if (oper == "-")
                        {
                            value.Push(v2 - v1);
                        }
                    }

                    if (operators.Peek() == "(")
                    {
                        operators.Pop();
                    }
                    
                    if ((value.Count() > 1 && operators.Peek() == "*") || (value.Count() > 1 && operators.Peek() == "/"))
                    {
                        int v1 = value.Pop();
                        int v2 = value.Pop();
                        string oper = operators.Pop();

                        if (oper == "*")
                        {
                            value.Push(v2 * v1);
                        }
                        else if (oper == "/")
                        {
                            try
                            {
                                value.Push(v2 / v1);
                            }
                            catch (DivideByZeroException)
                            {
                                Console.WriteLine("division by 0");
                            }
                        }
                    } 
                }
            }
            //When the last token has been processed

            if (operators.Count == 1 && value.Count == 2)
            {
                if (operators.Peek() == "+")
                {
                    int v1 = value.Pop();
                    int v2 = value.Pop();
                    operators.Pop();
                    value.Push(v1 + v2);
                }

                else if (operators.Peek() == "-")
                {
                    int v1 = value.Pop();
                    int v2 = value.Pop();
                    operators.Pop();
                    value.Push(v2 - v1);
                }
            }
            return value.Peek();
        }

    }
}