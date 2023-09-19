using FormulaEvaluator;
using System;

class Test_the_Evaluator_Console_App
{
    static void Main()
    {

        test1();

        try
        {
            Evaluator.Evaluate("2/0");
        }
        catch(DivideByZeroException)
        {
            Console.WriteLine("You cannot divide by zero.");
        }


        try
        {
            Evaluator.Evaluate("((2+3)*2");
        }
        catch(Exception)
        {
            Console.WriteLine("Yor expression has extra parenthesis.");
        }

        

    }


    public static void test1()
    {
        Console.WriteLine(Evaluator.Evaluate("((2+2)*3+19)*2"));
    }

}



/*
try
    {
        Evaluator.Evaluate(" -A- ");
    }
    catch (ArgumentException)
    {
        Console.WriteLine("Enter the appropriate expression!");
    }
*/

