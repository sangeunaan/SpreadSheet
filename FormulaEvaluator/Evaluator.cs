/// <summary> 
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
///    [... and of course you should describe the contents of the file in broad terms here ...] 
/// </summary>

/// <summary>
///   The function does ....  You should be aware of the following edge cases ....
/// 
/// </summary>
/// <param name="x"> x represents ... </param>
/// <param name="y"> y represents ... </param>
/// <returns> The value ABC will be returned representing ... </returns>


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
        //public delegate int Lookup(String variable_name);
        public static int Evaluate(string expression)
        {
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");


            Stack<string> operators = new Stack<string>();
            Stack<int> value = new Stack<int>();

            int ans = 0; 

            foreach (string t in substrings)
            {
                bool isNumeric = int.TryParse(t, out int num);

                //Condition1: t in an integer
                if (isNumeric)
                {

                    if (value.Count == 0)
                    {
                        Console.WriteLine("Your value stack is empty !");
                    }

                    else if (value.Count > 0 && operators.Count() > 0 && operators.Peek() == "*")
                    {
                        int v1 = value.Pop();
                        operators.Pop();
                        value.Push(v1 * num);
                    }
                    else if (value.Count > 0 && operators.Count() > 0 && operators.Peek() == "/")
                    {
                        int v1 = value.Pop();
                        operators.Pop();

                        try
                        {
                            value.Push(v1 / num);
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
                else if ((operators.Count>0 && t == "+" ) || (operators.Count>0 && t == "-"))
                {
                    if(value.Count>1)
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

                        operators.Push(t);
                    }
                    else
                    {
                        Console.WriteLine("Your value stack contains fewer than 2 values.");
                    }

                }

                //Condition3: t is * or /
                else if ((operators.Count > 0 && t == "*") || (operators.Count >0 && t == "/"))
                {
                    operators.Push(t);
                }

                //Condigion4: t is (
                else if ((operators.Count >0 && t == "("))
                {
                    operators.Push(t);
                }

                //Condition5: t is )
                else if ((operators.Count > 0 && t == ")"))
                {
                    if(value.Count()>1)
                    {
                        if (operators.Peek() == "+" || operators.Peek() == "-")
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

                        else if (operators.Peek() == "(")
                        {
                            operators.Pop();
                        }

                        else if (operators.Peek() == "*" || operators.Peek() == "/")
                        {
                            int v1 = value.Pop();
                            int v2 = value.Pop();
                            string oper = operators.Pop();

                            if (oper == "*")
                            {
                                operators.Pop();
                                value.Push(v2 * v1);
                            }
                            else if (oper == "/")
                            {
                                operators.Pop();
                                value.Push(v2 / v1);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Your value stack contains fewer than 2 values.");
                    }
                    
                }
            }

            //When the last token has been processed
            if (operators.Count == 0 && value.Count > 0)
            {
                ans = value.Peek();
                value.Pop();
            }

            else if (operators.Count > 0 && value.Count > 0)
            {
                if (operators.Peek() == "+")
                {
                    int v1 = value.Pop();
                    int v2 = value.Pop();
                    operators.Pop();
                    ans = v2 + v1;
                }

                else if (operators.Peek() == "-")
                {
                    int v1 = value.Pop();
                    int v2 = value.Pop();
                    operators.Pop();
                    ans = v2 - v1;
                }
            }
            return ans;
        } 
       
    }
}
