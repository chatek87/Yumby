namespace Yumby2;

public class Recipe
{
    public Guid RecipeId { get; set; }
    public string? Name { get; set; }
    public List<Ingredient> Ingredients { get; set; } = null!;
    public List<string> Instructions { get; set; } = null!;
    public decimal ServingsYielded { get; set; }

}
