using Yumby.DataModels;

namespace Yumby.ConsoleUI;

public static class RecipeActions
{
    //TODO: rename below method to "AddInstructions()
    //type out a step (string step[])
    //   (would you like to add another step?)   (y/n)
    //      if y, go back ^
    //    if n, finalize the object.
    public static string? AddInstructionsLine()
    {
        string? instructionsLine = Console.ReadLine();
        return instructionsLine;
    }

    public static Ingredient AddNewIngredient()
    {
        var ingredient = new Ingredient();

        Console.Write("Enter ingredient name: ");
        string? lowerCaseIngredientName = Console.ReadLine();
        ingredient.Name = lowerCaseIngredientName?.ToLower();

        decimal ingredientQuantity;
        do Console.Write("Enter ingredient quantity (must be numerical value): ");
        while (!decimal.TryParse(Console.ReadLine(), out ingredientQuantity));
        ingredient.Quantity = ingredientQuantity;
        if (ingredient.Quantity == 0)
        {
            ingredient.Quantity = 1;
            Console.WriteLine($"Ingredient quantity set to 1 by default");
        }

        Console.Write("Enter ingredient unit: ");
        ingredient.Unit = Console.ReadLine();

        return ingredient;

    }
}
