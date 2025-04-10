using System;
using System.Collections.Generic;

class Coctails
{
    public string Name;
    public byte Ingredients;
    public List<int> Gradus;
    public List<int> PriceIngredients;
    public static int CoctailCount;

    public Coctails()
    {
        Name = "name";
        Ingredients = 0;
        Gradus = new List<int>();
        PriceIngredients = new List<int>();
        CoctailCount++;
    }

    public Coctails(string name, byte ingredientCount, List<int> gradus, List<int> price)
    {
        Name = name;
        Ingredients = ingredientCount;
        Gradus = gradus;
        PriceIngredients = price;
        CoctailCount++;
    }

    public static Coctails InputCoctail()
    {
        string name;
        while (true)
        {
            Console.WriteLine("Enter name of cocktail:");
            string nameInput = Console.ReadLine()!;
            if (nameInput.Length <= 0)
            {
                Console.WriteLine("Invalid input. Name cannot be empty.");
                continue;
            }
            name = nameInput;
            break;
        }

        byte ingredientCount;
        while (true)
        {
            Console.WriteLine("Enter amount of ingredients (max 3):");
            string l = Console.ReadLine()!;
            if (byte.TryParse(l, out ingredientCount))
            {
                if (ingredientCount < 1 || ingredientCount > 3)
                {
                    Console.WriteLine("Invalid input. Must be a number between 1 and 3.");
                    continue;
                }
            }
            break;
        }

        List<int> gradus = new();
        for (int i = 0; i < ingredientCount; i++)
        {
            while (true)
            {
                Console.Write($"Enter alcohol degree of ingredient {i + 1} (1–20): ");
                string input = Console.ReadLine()!;
                if (int.TryParse(input, out int f))
                {
                    if (f >= 1 && f <= 20)
                    {
                        gradus.Add(f);
                        break;
                    }
                }
                Console.WriteLine("Invalid input. Must be a number between 1 and 20.");
            }
        }

        List<int> price = new();
        for (int i = 0; i < ingredientCount; i++)
        {
            while (true)
            {
                Console.Write($"Enter price of ingredient {i + 1} (1-200): ");
                string t = Console.ReadLine()!;
                if (int.TryParse(t, out int g))
                {
                    if (g >= 1 && g <= 200)
                    {
                        price.Add(g);
                        break;
                    }
                }
                Console.WriteLine("Invalid input. Must be a number between 1 and 200.");
            }
        }

        return new Coctails()
        {
            Name = name,
            Ingredients = ingredientCount,
            Gradus = gradus,
            PriceIngredients = price,
        };
    }

    public Coctails Copy()
    {
        return new Coctails()
        {
            Name = this.Name,
            Ingredients = this.Ingredients,
            Gradus = new List<int>(this.Gradus),
            PriceIngredients = new List<int>(this.PriceIngredients)
        };
    }

    public static void client(double money, Coctails coctail)
    {
        int sumgradus = 0;
        foreach (var g in coctail.Gradus)
        {
            sumgradus += g;
        }
        int sum = 0;
        foreach (var p in coctail.PriceIngredients)
        {
            sum += p;
        }
        if (money < sum)
        {
            Console.WriteLine("Not enough money to buy the cocktail.");
        }
        else
        {
            if (sumgradus > 10)
            {
                Console.WriteLine("Plotnaia otrijka");
            }
            else
            {
                if (sumgradus < 10)
                {
                    Console.WriteLine("Slaboia otrijechka");
                }
            }
        }
    }

    public void PrintInfo()
    {

        Console.WriteLine($"Name: {this.Name}");
        Console.WriteLine($"Ingredients: {this.Ingredients}");
        Console.Write("Gradus: ");
        foreach (var g in this.Gradus)
        {
            Console.Write($"{g} ");
        }
        Console.WriteLine();
        Console.Write("Price: ");
        foreach (var p in this.PriceIngredients)
        {
            Console.Write($"{p} ");
        }
        Console.WriteLine();
        Console.WriteLine();
    }
    public int TotalPrice()
    {
        int sum = 0;
        foreach (var p in PriceIngredients)
            sum += p;
        return sum;
    }
}

class Program
{
    static void Main()
    {
        Random rand = new Random();

        List<Coctails> bar = new List<Coctails>()
        {
            new Coctails(name: "test", ingredientCount: 2, gradus: new List<int> { 1, 2 }, price: new List<int> { 1, 2 }),
            new()
        };

        Coctails randCoctail= new Coctails();
            randCoctail.Name = "Random coctail ";
            randCoctail.Ingredients = (byte)rand.Next(1, 4);
            randCoctail.Gradus = new List<int>();
            randCoctail.PriceIngredients = new List<int>();
            for (int j = 0; j < randCoctail.Ingredients; j++)
            {
                randCoctail.Gradus.Add(rand.Next(1, 21));
                randCoctail.PriceIngredients.Add(rand.Next(1, 201));
            }
        bar.Add(randCoctail);

        bar.Add(Coctails.InputCoctail());
        foreach (var c in bar)
        {
            c.PrintInfo();
        }
        int totalPrice = 0;
        foreach (var c in bar)
        {
            totalPrice += c.TotalPrice();
        }
        Console.WriteLine($"Total price of all cocktails: {totalPrice}");
        Console.WriteLine("client1");
        Coctails.client(150, bar[0]);
        Console.WriteLine("client3");
        Coctails.client(300, bar[bar.Count - 1]);

        Console.WriteLine($"Total number of created cocktails: {Coctails.CoctailCount}");
    }
}