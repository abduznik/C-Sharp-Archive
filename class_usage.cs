using System;
using System.Linq;

class person
{
    public string Fname;
    public string Lname;

    public person(string firstName, string lastName)
    {
        Fname = firstName;
        Lname = lastName;
    }
    public void printPerson()
    {
        Console.WriteLine(Fname + " " + Lname);
    }
    static void test()
    {
        Console.WriteLine("hello world");
    }
    static void Main(string[] args)
    {
        person p = new person("sisai","almo");
        //Console.WriteLine(p.Fname + "  " + p.Lname);
        p.printPerson();
        test();
        Console.ReadLine();
        
    }
}
