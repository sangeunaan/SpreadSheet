using FormulaEvaluator;


    void test1()
    {
        if (Evaluator.Evaluate("5+5") == 10)
        {
            Console.WriteLine("Happy Day!");
        }
    }

    void test1()
{
    if (Evaluator.Evaluate("5+5") == 10)
    {
        Console.WriteLine("Happy Day!");
    }
}

try
    {
        Evaluator.Evaluate(" -A- ");
    }
    catch (ArgumentException)
    {
        Console.WriteLine("Enter the appropriate expression!");
    }



test1();