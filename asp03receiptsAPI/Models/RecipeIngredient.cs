namespace asp03receiptsAPI.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
        public required int Quantity { get; set; }
        public required string Unit { get; set; }
    }
}
