namespace asp03receiptsAPI.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public required string Name { get; set; }
        public ICollection<RecipeIngredient>? Content { get; set; }
    }
}
