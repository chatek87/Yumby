namespace Yumby.DataModels;

public class Recipe
{
    public int RecipeId { get; set; }
    public string? Name { get; set; }
    public List<Ingredient> Ingredients { get; set; } = null!;
    public List<string> Instructions { get; set; } = null!;
    public decimal ServingsYielded { get; set; }

}