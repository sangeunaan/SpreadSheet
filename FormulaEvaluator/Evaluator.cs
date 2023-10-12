﻿/// <summary> /// <summary> 
/// Author:    Sangeun An
/// Partner:   None 
/// Date:      10/9/2023
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

// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

// Implementation by Basil Vetas
// CS 3500 Fall 2014
// Date: September 22, 2014

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FormulaEvaluator
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax; variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and "y"; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    /// <author>Basil Vetas</author>
    /// <date>September 23rd 2014</date>
    public class Formula
    {
        // variable to hold the Formula tokens
        private List<string> tokens;

        // variable to keep track of normalized variables
        private HashSet<string> normalized_vars;

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>       
        public Formula(String formula) : this(formula, s => s, s => true)
        {
        }

        // Note that => is the lambda operator.  It is used in lambda expressions to separate the 
        // input variables on the left side with the lambda body on the right side. A lambda 
        // expression is an anonymous function that we are using to create delegates. The first 
        // parameter s => s is the normalizer delegate, the second s => true is the validator delegate.  

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in this class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {
            // if there is not at least one token throw and error
            if ((formula == String.Empty) || ReferenceEquals(formula, null))
                throw new FormulaFormatException("Your formula is null or empty, try adding characters in your string");

            // initialize tokens
            tokens = new List<string>(GetTokens(formula));

            // keep track of normalized variables
            normalized_vars = new HashSet<string>();

            // these two variables keep track of the number of parentheses in a formula
            int open_parens = 0;
            int close_parens = 0;

            double number; // used to store parsed variables into tokens

            // check that first token is number, variable, or opening paren
            string first_token = tokens.First<string>();
            string last_token = tokens.Last<string>();

            if (!(first_token.Equals("("))) // if first token is not an opening paren
                if (!(Double.TryParse(first_token, out number)))   // and if not parseable into a floating point
                    if (!(IsVariable(first_token))) // and if not a proper variable then throw an exception
                        throw new FormulaFormatException("The first token of your formula must be a number, variable, or opening parentheses");

            if (!(last_token.Equals(")"))) // if last token is not a closing paren
                if (!(Double.TryParse(last_token, out number)))   // and if not parseable into a floating point
                    if (!(IsVariable(last_token))) // and if not a proper variable then throw an exception
                        throw new FormulaFormatException("The last token of your formula must be a number, variable, or opening parentheses");

            string previous_token = ""; // keep track of the previous token in the formula during foreach loop

            // verify that the only tokens are (, ), +, -, *, /, variables, and floating point numbers
            for (int i = 0; i < tokens.Count; i++)
            {
                string t = tokens[i];

                // check if the token is a parentheses
                if (t.Equals("("))
                {
                    open_parens++;  // increment open count                                       
                }
                else if (t.Equals(")"))
                {
                    close_parens++; // increment close count
                    if (close_parens > open_parens) // close parens cannot exceed open parens
                        throw new FormulaFormatException("The number of closing parentheses cannot exceed the number of opening parentheses");
                } // otherwise check if it is an operator
                else if (t.Equals("+") || t.Equals("-") || t.Equals("*") || t.Equals("/"))
                { } // otherwise check if it is a floating point number
                else if (Double.TryParse(t, out number))
                {
                    string temp = number.ToString();
                    tokens[i] = temp;

                } // otherwise check if it is a proper variable
                else if (IsVariable(t))
                { } // otherwise it must be an improper format, so throw an exception
                else throw new FormulaFormatException("At least one of your tokens is an invalid character");

                // if the token immediately follows an open paren or operator, check that it is a number, variable, or opening paren
                // else if the token immediately follows a number, variable, or close paren, check that it is an operator or closing paren 
                if (previous_token.Equals("(") || previous_token.Equals("+") || previous_token.Equals("-") || previous_token.Equals("*") || previous_token.Equals("/"))
                {
                    if (!(Double.TryParse(t, out number) || IsVariable(t) || t.Equals("(")))
                        throw new FormulaFormatException("A token or open paren must be followed by a number, variable, or open paren");
                }
                else if (previous_token.Equals(")") || Double.TryParse(previous_token, out number) || IsVariable(previous_token))
                {
                    if (!(t.Equals(")") || t.Equals("+") || t.Equals("-") || t.Equals("*") || t.Equals("/")))
                        throw new FormulaFormatException("A number, variable, or close paren must be followed by an openin");
                }

                previous_token = t; // let this token become the 'previous token' for the next one in the loop            

            } // CLOSE foreach loop

            // make sure # of open and close parens are equal
            if (close_parens != open_parens)
                throw new FormulaFormatException("The number of opening and closing parentheses in your formula are not equal");

            // If we get all the way through without an exception, it should be a valid formula
            // then we want to normalize the formula and validate it according to the delegate parameters
            for (int i = 0; i < tokens.Count; i++)
            {
                string v = tokens[i];
                if (IsVariable(v))   // if token 'v' is a variable
                {
                    if (!(IsVariable(normalize(v)))) // if the normalized variable is not valid, throw and exception
                        throw new FormulaFormatException("A normalized variable does not have a valid variable format");

                    if (!(isValid(normalize(v))))
                        throw new FormulaFormatException("A normalized variable does not meet the isValid criteria for variables");
                    else // if it is a valid variable, replace the old variable with the normalized one
                    {
                        tokens[i] = normalize(v);

                        // add a key, value pair from the old value to the new one
                        normalized_vars.Add(tokens[i]);
                    }

                }
            }

        } // CLOSE Formula Constructor

        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            // string[] holding the formula tokens
            string[] substrings = tokens.Cast<string>().ToArray<string>();

            Stack<double> values = new Stack<double>(); // changed to double
            Stack<String> operators = new Stack<String>();

            for (int j = 0; j < substrings.Length; j++)
            {
                String t = substrings[j]; // current token
                double number; // will hold parsed double value before casting to int

                // check if t is an operator
                if (t.Equals("*") || t.Equals("/") || t.Equals("("))
                {
                    operators.Push(t);
                }
                else if (t.Equals("+") || t.Equals("-") || t.Equals(")"))
                {
                    String top = "";
                    if (operators.Count > 0)
                        top = operators.Peek();
                    if (top.Equals("+") || top.Equals("-"))
                    {
                        //if (values.Count < 2)
                        //    throw new ArgumentException();

                        double first_val = values.Pop();    // changed to double
                        double second_val = values.Pop();   // changed to double
                        String operation = operators.Pop();
                        double result;  // changed to double
                        // run the operation on values, check if it is + or -
                        if (operation.Equals("+"))
                            result = second_val + first_val;
                        else
                            result = second_val - first_val;
                        values.Push(result);
                    }

                    if (t.Equals(")"))
                    {
                        String new_top = "";
                        if (operators.Count > 0)
                            new_top = operators.Peek();

                        //if (!(new_top.Equals("(")))
                        //    throw new ArgumentException();

                        operators.Pop(); // should be a "("

                        if (operators.Count > 0)
                            new_top = operators.Peek();

                        if (new_top.Equals("*") || new_top.Equals("/"))
                        {
                            //if (values.Count < 2)
                            //    throw new ArgumentException();

                            double first_val = values.Pop();    // changed to double
                            double second_val = values.Pop();   // changed to double
                            String operation = operators.Pop();
                            double result;  // changed to double                          
                            // run the operation on values, check if it is * or /
                            if (operation.Equals("*"))
                                result = second_val * first_val;
                            else
                            {
                                if (first_val == 0)
                                    return new FormulaError("Error: Divide by Zero");
                                result = second_val / first_val;
                            }
                            values.Push(result);
                        }
                    }
                    else // if t is not a ")", push it onto the stack
                        operators.Push(t);
                }
                // if t not an operator, check if it is an integer or variable
                else if (Double.TryParse(t, out number)) // if it is parsable, will return true
                {
                    double token = number; // number will hold the token value as a double

                    String top = "";
                    if (operators.Count > 0)
                        top = operators.Peek();

                    if (top.Equals("*") || top.Equals("/"))
                    {
                        double first_val = values.Pop();    // changed to double
                        String operation = operators.Pop();
                        double result; // changed to double
                        if (operation.Equals("*"))
                            result = first_val * token;
                        else
                        {
                            if (token == 0) // changed from PS1 to return an error
                                return new FormulaError("Error: Divide by Zero");
                            result = first_val / token;
                        }
                        values.Push(result);
                    }
                    else
                        values.Push(token);
                }
                else // otherwise t must be a variable
                {
                    // check if t is a proper variable or if it is an illegal form throw exception
                    for (int i = 0; i < t.Length; i++)
                    {
                        // if the var candidate is not a letter or digit, throw exception
                        char current = t[i];
                        //if (!(char.IsLetter(current)) && !(char.IsDigit(current)))
                        //    throw new ArgumentException();

                        // if first character is not a letter, throw exception
                        //if ((i == 0) && !(char.IsLetter(current)))                            
                        //    throw new ArgumentException();

                        // once a digit is reached, if any of the subsequent chars are not digits, throw exception
                        if (char.IsDigit(current))
                        {
                            for (int k = i; k < t.Length; k++)
                            {
                                char next = t[k];
                                //if (!(char.IsDigit(next)))
                                //    throw new ArgumentException();
                            }
                        }

                        // if loop reaches the end and there is no digit, throw exception
                        //if((i == t.Length-1) && !(char.IsDigit(current)))
                        //    throw new ArgumentException();

                    } // end variable check

                    // *** Double check this loopup function i donno if its right *** // changed to loopup from variableEvaluator
                    double token;
                    try
                    {
                        token = lookup(t); // lookup value of t to use as the token value
                    }
                    catch (ArgumentException e)
                    {
                        return new FormulaError("Error: Your Variable is Undefined");
                    }

                    String top = "";
                    if (operators.Count > 0)
                        top = operators.Peek();

                    if (top.Equals("*") || top.Equals("/"))
                    {
                        double first_val = values.Pop();    // changed to double
                        String operation = operators.Pop();
                        double result; // changed to double
                        if (operation.Equals("*"))
                            result = first_val * token;
                        else
                        {
                            if (token == 0) // changed from PS1 to return an error
                                return new FormulaError("Error: Divide by Zero");
                            result = first_val / token;
                        }
                        values.Push(result);
                    }
                    else
                        values.Push(token);

                } // end 'if-else statement'

            } // end 'for loop'

            if (operators.Count == 0) // if operators stack is empty
            {
                // There should only be one value left on the values stack
                //if (values.Count != 1)
                //    throw new ArgumentException();
                return values.Pop();
            }
            else
            {
                // There should be exactly one operator left on the stack, and it should
                // be either + or -. There should be exactly two values in the value stack.
                //if((operators.Count != 1) || (values.Count != 2))
                //    throw new ArgumentException();

                double first_val = values.Pop();    // changed to double
                double second_val = values.Pop();   // changed to double
                String operation = operators.Pop();
                double result;  // changed to double
                if (operation.Equals("+"))
                    result = second_val + first_val;
                else
                    result = second_val - first_val;
                return result;
            }
        }

        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            HashSet<string> copy = new HashSet<string>(normalized_vars);
            return copy;
        }

        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            string formula = "";
            for (int i = 0; i < tokens.Count; i++)
            {
                formula += tokens[i];
            }
            return formula;
        }

        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens, which are compared as doubles, and variable tokens,
        /// whose normalized forms are compared as strings.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object obj)
        {
            // if 'obj' is null or is not a Formula         
            if (ReferenceEquals(obj, null) || (obj.GetType() != this.GetType()))
                return false;   // then we can't evaluate the objects for equality

            Formula formula = (Formula)obj; // cast the obj to a formula once we know it is a formula type

            // then for each token, if the string don't match, return false
            for (int i = 0; i < this.tokens.Count; i++)
            {
                // current tokens in each formula
                string this_current = this.tokens[i];
                string formula_current = formula.tokens[i];

                // will hold parsable numbers
                double this_number;
                double formula_number;

                // if the current tokens are both parsable as numbers, then store them as numbers and compare them
                if ((Double.TryParse(this_current, out this_number)) && (Double.TryParse(formula_current, out formula_number)))
                {
                    if (this_number != formula_number)  // if the numbers aren't equal, return false
                        return false;
                }
                else // don't need to check if variables since they should have already been normalized
                {
                    if (!(this_current.Equals(formula_current)))
                        return false;
                }
            }

            return true;    // otherwise, the tokens are equals
        }

        /// <summary>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return true.  If one is
        /// null and one is not, this method should return false.
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            // checks if either one or both formulas is null and returns accordingly
            if ((ReferenceEquals(f1, null)) && (ReferenceEquals(f2, null)))
                return true;
            else if ((ReferenceEquals(f1, null)) && !(ReferenceEquals(f2, null)))
                return false;
            else if (!(ReferenceEquals(f1, null)) && (ReferenceEquals(f2, null)))
                return false;

            // if neither are null then we check according to .Equals method
            return (f1.Equals(f2));

        }

        /// <summary>
        /// Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return false.  If one is
        /// null and one is not, this method should return true.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            if (!(f1 == f2))
                return true;

            return false;
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            // get the formula as a string and use the hash code for that string
            int hash_code = this.ToString().GetHashCode();
            return hash_code;
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        } // CLOSE GetToken method

        /// <summary>
        /// Private helper method to check if a given token is a proper variable
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static Boolean IsVariable(String token)
        {
            // if it is a proper variable return true, else return false
            if (Regex.IsMatch(token, @"[a-zA-Z_](?: [a-zA-Z_]|\d)*", RegexOptions.Singleline))
                return true;
            else return false;
        }

    } // CLOSE Formula class

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message) : base(message)
        {
        }
    }

    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason) : this()
        {
            Reason = reason;
        }

        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
    }

} // CLOSE SpreadsheetUtilities namespace
