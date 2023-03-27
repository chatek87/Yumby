namespace Yumby.DataModels;

public class Ingredient
{
    public int RecipeId { get; set; }
    public string? Name { get; set; }
    public decimal Quantity { get; set; }
    public string? UnitOfMeasurement { get; set; }
}
