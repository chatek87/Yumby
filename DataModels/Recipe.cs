namespace Yumby.DataModels;

public class Recipe
{
    public int RecipeId { get; set; }
    public string? Name { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public List<string> Instructions { get; set; } = new List<string>();
    public decimal ServingsYielded { get; set; }
}