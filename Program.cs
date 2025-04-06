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
            if (string.IsNullOrWhiteSpace(nameInput))
            {
                Console.WriteLine("Name cannot be empty.");
                continue;
            }
            name = nameInput;
            break;
        }

        byte ingredientCount;
        while (true)
        {
            Console.WriteLine("Enter amount of ingredients (max 3):");
            if (!byte.TryParse(Console.ReadLine(), out ingredientCount) || ingredientCount < 1 || ingredientCount > 3)
            {
                Console.WriteLine("Invalid input. Must be a number between 1 and 3.");
                continue;
            }
            break;
        }

        List<int> gradus = new();
        for (int i = 0; i < ingredientCount; i++)
        {
            while (true)
            {
                Console.Write($"Enter alcohol degree of ingredient {i + 1} (1-20): ");
                if (int.TryParse(Console.ReadLine(), out int t) && t >= 1 && t <= 20)
                {
                    gradus.Add(t);
                    break;
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
                if (int.TryParse(Console.ReadLine(), out int t) && t >= 1 && t <= 200)
                {
                    price.Add(t);
                    break;
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
        Console.WriteLine($"Coctail count: {CoctailCount}");
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
        Console.WriteLine("\n");
    }

    public static string CompareComplexity(Coctails a, Coctails b)
    {
        if (a.Ingredients > b.Ingredients)
            return $"{a.Name} is more complex than {b.Name}";
        else if (a.Ingredients < b.Ingredients)
            return $"{b.Name} is more complex than {a.Name}";
        else
            return $"{a.Name} and {b.Name} have the same complexity";
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
            new ("test", 2, new List<int> { 1, 2 }, new List<int> { 1, 2 }),
            new()
        };

        for (int i = 0; i < bar.Count - 1; i++)
        {
            bar[i].Name = $"Random{i + 1}";
            bar[i].Ingredients = (byte)rand.Next(1, 4);
            bar[i].Gradus = new List<int>();
            bar[i].PriceIngredients = new List<int>();
            for (int j = 0; j < bar[i].Ingredients; j++)
            {
                bar[i].Gradus.Add(rand.Next(1, 21));
                bar[i].PriceIngredients.Add(rand.Next(1, 201));
            }
        }

        Console.WriteLine("Random cocktails:");
        foreach (var c in bar)
        {
            c.PrintInfo();
        }

        // Последний коктейль — пользовательский (poetomy - 1)
        bar[bar.Count - 1] = Coctails.InputCoctail();
        bar[bar.Count - 1].PrintInfo();

        // Сравнение сложности (ингредиентов)
        Console.WriteLine("Proverka na slojnost cokteilia:");
        for (int i = 0; i < bar.Count - 1; i++)
        {
            Console.WriteLine(Coctails.CompareComplexity(bar[i], bar[bar.Count - 1]));
        }

        int totalPrice = 0;
        foreach (var c in bar)
        {
            totalPrice += c.TotalPrice();
        }
        Console.WriteLine($"Total price of all cocktails: {totalPrice}");
        Console.WriteLine("client1");
        Coctails.client(150, bar[0]);
        if (bar.Count > 2){
        Console.WriteLine("client2");
            Coctails.client(80, bar[1]);
        }
        Console.WriteLine("client3");
        Coctails.client(300, bar[bar.Count - 1]);

        Console.WriteLine($"\nTotal number of created cocktails: {Coctails.CoctailCount}");
    }
}