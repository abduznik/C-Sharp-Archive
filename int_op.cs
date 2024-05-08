using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter your first number:");
        string input1 = Console.ReadLine();
        int num1 = Convert.ToInt32(input1);

        Console.WriteLine("Enter your second number:");
        string input2 = Console.ReadLine();
        int num2 = Convert.ToInt32(input2);

        int kef = num1 * num2;
        int od = num1 + num2;
        int hiluk = num1 / num2;
        int pahot = num1 - num2;

        Console.WriteLine($"The kefel is {kef}");
        Console.WriteLine($"The od is {od}");
        Console.WriteLine($"The hiluk result is {hiluk}");
        Console.WriteLine($"The pahot is {pahot}");
    }
}
