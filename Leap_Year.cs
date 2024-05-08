using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter your year:");
        string input1 = Console.ReadLine();
        int year = Convert.ToInt32(input1);
        
        if ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0))
        {
            Console.WriteLine("Leap year compatible!");
        }
        else
        {
            Console.WriteLine("Not a leap year.");
        }

    }
    
}
