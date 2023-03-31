using Yumby.DataModels;

namespace Yumby.ConsoleUI;

public static class ShoppingListAction
{
    //Single Responsibility Principle:
    //This class's only purpose is to handle the Shopping List functionality the program provides
    public static void DisplayShoppingList(Recipe recipe)
    {
        Console.Clear();
        Console.WriteLine($"Shopping list for {recipe.Name}\nServes {recipe.ServingsYielded}");
        Console.WriteLine(" ");

        foreach (var ingredient in recipe.Ingredients)
        {
            Console.WriteLine($"{ingredient.Quantity} {ingredient.UnitOfMeasurement} {ingredient.Name}");
        }
        WriteLine("\n\n");
        ConsoleUtils.WaitForKeyPress();
    }
}