using Internal;
using System;

abstract class Animal {
    public string Name;
    public abstract void eat();
}

class Dog : Animal
{
    public Dog()
    {
        Name = "Dog";
    }

    public override void eat()
    {
        Console.WriteLine(Name + " is eating");
    }
}

class Cat : Animal
{
    public Cat()
    {
        Name = "Cat";
    }

    public override void eat()
    {
        Console.WriteLine(Name + " is eating");
    }
}

class Program
{
    static void Main()
    {
        Dog dog = new Dog();
        Cat cat = new Cat();

        dog.eat();
        cat.eat();
        Console.ReadLine();
    }
}