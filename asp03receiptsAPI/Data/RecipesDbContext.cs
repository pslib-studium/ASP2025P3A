using asp03receiptsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace asp03receiptsAPI.Data
{
    public class RecipesDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public RecipesDbContext(DbContextOptions options) : base(options)
        {
        }

        protected RecipesDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            int michanaVajickaId = 1;
            int vajickoId = 1;
            Recipe michanaVajicka = new()
            {
                RecipeId = michanaVajickaId,
                Title = "Míchaná vajíčka"
            };
            Ingredient vajicko = new()
            {
                IngredientId = vajickoId,
                Name = "Vajíčko"
            };

            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasData(michanaVajicka);
            });
            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasData(vajicko);
            });
            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.HasData(new RecipeIngredient
                {
                    RecipeId = michanaVajickaId,
                    IngredientId = vajickoId,
                    Quantity = 3,
                    Unit = "ks"
                });
            });
        }
    }
}
