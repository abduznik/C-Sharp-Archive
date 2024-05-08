using System;

class Person
{
    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }
}

class Grandpa : Person
{
    public Grandpa(string name) : base(name)
    {
    }

    public void PrintFamily()
    {
        Console.WriteLine(Name + " is saba.");
    }
}

class Dad : Person
{
    public Dad(string name) : base(name)
    {
    }

    public void PrintFamily(Grandpa grandpa)
    {
        Console.WriteLine(Name + " is the dad. " + grandpa.Name + " is the saba.");
    }
}

class Girl : Person
{
    public Girl(string name) : base(name)
    {
    }

    public void PrintFamily(Dad dad, Grandpa grandpa)
    {
        Console.WriteLine(Name + " is a girl. " + dad.Name + " is her dad. " + grandpa.Name + " is her saba.");
    }
}

class Boy : Person
{
    public Boy(string name) : base(name)
    {
    }

    public void PrintFamily(Dad dad, Grandpa grandpa)
    {
        Console.WriteLine(Name + " is a boy. " + dad.Name + " is his dad. " + grandpa.Name + " is his saba.");
    }
}

class Program
{
    static void Main()
    {
        Grandpa grandpa = new Grandpa("Saul");
        Dad dad = new Dad("Tomer");
        Girl girl = new Girl("Orin");
        Boy boy = new Boy("Sisai");

        grandpa.PrintFamily();
        dad.PrintFamily(grandpa);
        girl.PrintFamily(dad, grandpa);
        boy.PrintFamily(dad, grandpa);
        Console.ReadLine();
    }
}
