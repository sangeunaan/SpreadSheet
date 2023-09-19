using FormulaEvaluator;
using System;

class Test_the_Evaluator_Console_App
{
    static void Main()
    {

        testAddition();
        testDivision();
        testSubtraction();
        testMultiplication();
        testDivideByZero();

    }

    public static void testAddition()
    {
        Console.WriteLine(Evaluator.Evaluate("3+3", null));
    }

    public static void testDivision()
    {
        Console.WriteLine(Evaluator.Evaluate("3/3", null));
    }

    public static void testSubtraction()
    {
        Console.WriteLine(Evaluator.Evaluate("3-3", null));
    }

    public static void testMultiplication()
    {
        Console.WriteLine(Evaluator.Evaluate("3*3", null));
    }

    public static void testDivideByZero()
    {
        Console.WriteLine(Evaluator.Evaluate("3/0", null));
    }
}



