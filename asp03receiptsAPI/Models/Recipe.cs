using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace asp03receiptsAPI.Models
{
    public class Recipe
    {
        [Key] // data annotation to specify primary key
        public int RecipeId { get; set; }
        [StringLength(maximumLength: 200)] // validation attribute to limit string length
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<RecipeIngredient>? Content { get; set; }
    }
}
