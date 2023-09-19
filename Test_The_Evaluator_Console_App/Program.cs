using FormulaEvaluator;
using System;

class Test_the_Evaluator_Console_App
{
    static void Main()
    {

        try
        {
            Evaluator.Evaluate("2/0", null);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("You cannot divide by zero.");
        }


        try
        {
            Evaluator.Evaluate("((2+3)*2", null);
        }
        catch(Exception)
        {
            Console.WriteLine("Yor expression has extra parenthesis.");
        }

        

    }

}



