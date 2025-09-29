namespace asp03receiptsAPI.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public required string Title { get; set; }
        public ICollection<RecipeIngredient>? Content { get; set; }
    }
}
