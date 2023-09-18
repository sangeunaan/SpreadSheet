using FormulaEvaluator;
using System;

class Test_the_Evaluator_Console_App
{
    static void Main()
    {
        //test1();
        test2();
    }

    public static void test1()
    {
        if(Evaluator.Evaluate("5+5") == 10)
        {
            Console.WriteLine("Happy Day!"); 
        }

    }

    public static void test2()
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

