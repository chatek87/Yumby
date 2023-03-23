using Yumby.DataModels;

namespace Yumby.ConsoleUI;

public static class CreateNewRecipeAction
{
    public static Recipe CreateNewRecipe()
    {
        var myRecipe = new Recipe();

        Console.Write("Recipe name: ");
        string? upperCaseRecipeName = Console.ReadLine();
        myRecipe.Name = upperCaseRecipeName?.ToUpper();
        Console.Clear();

        string prompt1 = $"<< {myRecipe.Name} ingredients >>";
        string[] options1 = { "add another ingredient", "done adding ingredients" };
        Console.WriteLine(prompt1);
        myRecipe.Ingredients = new List<Ingredient>();
        myRecipe.Ingredients.Add(RecipeActions.AddNewIngredient());
        //myRecipe.Ingredients.Add(myRecipe.AddNewIngredient());  // ***TODO: this functionality needs to be moved to RecipeService class, and hooked up to corresponding 
        // methods in the RecipeRepository/IRecipeRepository. 
        // SAME goes for AddInstructionsLine();
        var menu1 = new Menu(prompt1, options1);
        int selectionIndex1 = menu1.Run();
        while (selectionIndex1 == 0)
        {
            Console.Clear();
            Console.WriteLine(prompt1);
            foreach (Ingredient ingredient in myRecipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.Name} added to recipe!");
            }
            myRecipe.Ingredients.Add(RecipeActions.AddNewIngredient());
            //myRecipe.Ingredients.Add(myRecipe.AddNewIngredient());
            selectionIndex1 = menu1.Run();
        }
        Console.Clear();

        string prompt2 = $"<< {myRecipe.Name} instructions >> ";
        string[] options2 = { "add another line", "finish" };
        Console.WriteLine(prompt2);
        Console.WriteLine("(type one line at a time, followed by \"ENTER\")");
        myRecipe.Instructions = new List<string>();
        myRecipe.Instructions.Add(RecipeActions.AddInstructionsLine());
        //myRecipe.Instructions.Add(myRecipe.AddInstructionsLine());
        var menu2 = new Menu(prompt2, options2);
        int selectionIndex2 = menu2.Run();
        while (selectionIndex2 == 0)
        {
            Console.Clear();
            Console.WriteLine(prompt2);
            foreach (string instructionLine in myRecipe.Instructions)
            {
                Console.WriteLine(instructionLine);
            }
            myRecipe.Instructions.Add(RecipeActions.AddInstructionsLine());
            //myRecipe.Instructions.Add((myRecipe.AddInstructionsLine()));
            selectionIndex2 = menu2.Run();
        }
        Console.Clear();

        decimal myRecipeServingsYielded;
        do Console.Write($"<< {myRecipe.Name} servings >> \nEnter number of servings yielded (must be numerical value): ");
        while (!decimal.TryParse(Console.ReadLine(), out myRecipeServingsYielded));
        myRecipe.ServingsYielded = myRecipeServingsYielded;
        if (myRecipe.ServingsYielded == 0)
        {
            myRecipe.ServingsYielded = 1;
            Console.WriteLine("Servings yielded set to 1 by default");
        }
        Console.Clear();

        Console.WriteLine($"{myRecipe.Name} Added!");
        return myRecipe;
    }
}