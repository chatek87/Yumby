using Yumby.DataModels;

namespace Yumby.ConsoleUI;

public static class DisplayRecipeAction
{
    public static void DisplayRecipe(Recipe recipe)
    {
        Console.Clear();
        Console.WriteLine(recipe.Name);
        Console.WriteLine(" ");

        foreach (var ingredient in recipe.Ingredients)
        {
            Console.WriteLine($"{ingredient.Quantity} {ingredient.UnitOfMeasurement} {ingredient.Name}");
        }
        Console.WriteLine(" ");

        Console.WriteLine("Instructions: ");
        foreach (var instructionLine in recipe.Instructions)
        {
            Console.WriteLine(instructionLine);
        }
        Console.WriteLine(" ");

        Console.WriteLine($"Makes {recipe.ServingsYielded} servings.");

        WriteLine("\n\n\n");
        ConsoleUtils.WaitForKeyPress();
    }
}