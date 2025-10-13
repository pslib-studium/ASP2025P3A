using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp03receiptsAPI.Data;
using asp03receiptsAPI.Models;
using asp03receiptsAPI.Services.Options;
using Microsoft.Extensions.Options;
using asp03receiptsAPI.DTOs;

namespace asp03receiptsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesDbContext _context;
        private readonly ILogger<RecipesController> _logger;
        private readonly PaginationOptions _paginationOptions;

        public RecipesController(
            RecipesDbContext context, 
            ILogger<RecipesController> logger,
            IOptions<PaginationOptions> options
            )
        {
            _context = context;
            _logger = logger;
            _paginationOptions = options.Value;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<ListItems<Recipe>>> GetRecipes(
            string? search,
            string? title,
            int? page,
            int? size,
            SortedBy order = SortedBy.Id
            )
        {
            IQueryable<Recipe> query = _context.Recipes;
            // filter
            if (!String.IsNullOrEmpty(search))
            {
                query = query.Where(r => r.Title.StartsWith(search, StringComparison.OrdinalIgnoreCase));
            }
            if (!String.IsNullOrEmpty(title))
            {
                //query = query.Where(r => r.Title.Contains(title));
                query = query.Where(r => r.Title.ToLower().Contains(title.ToLower()));
            }
            // ordering
            query = order switch
            {
                SortedBy.Title => query.OrderBy(r => r.Title),
                SortedBy.Title_Desc => query.OrderByDescending(r => r.Title),
                SortedBy.Id_Desc => query.OrderByDescending(r => r.RecipeId),
                _ => query.OrderBy(r => r.RecipeId),
            };
            // pagination
            int total = query.Count();
            if (!page.HasValue) page = 1;
            query = query.Skip((page.Value - 1) * (size ?? _paginationOptions.DefaultPageSize));
            if (size.HasValue)
            {
                query = query.Take(size.Value);
            }
            return new ListItems<Recipe>
            {
                TotalCount = total,
                Items = await query.ToListAsync(),
                Page = page.Value,
                Size = size
            };
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.RecipeId)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/ingredinces")]
        public async Task<ActionResult<IEnumerable<RecipeIngredientDto>>> GetRecipeIngredients(int id)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.RecipeId == id);
            if (recipe == null)
            {
                return NotFound("recipe not found");
            }
            _logger.LogInformation("Recipe {id} found", id);
            await _context.Entry(recipe)
                .Collection(r => r.Content!)
                .Query()
                .Include(ri => ri.Ingredient)
                .LoadAsync();
            var result = recipe.Content!
                .Select(ri => new RecipeIngredientDto(
                    ri.RecipeId,
                    ri.IngredientId,
                    ri.Ingredient.Name,
                    ri.Quantity,
                    ri.Unit))
                .ToList();
            return Ok(result);
        }

        //[HttpPost("{id}/ingrediences/{ingredientId}")]
        [HttpPost("{id}/ingrediences")]
        public async Task<ActionResult<RecipeIngredient>>
            AddIngredientToRecipe(int id, [FromBody] AddIngredientToRecipeDto ing)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound("recipe not found");
            }
            var ingredient = await _context.Ingredients.FindAsync(ing.IngredientId);
            if (ingredient == null)
            {
                return NotFound("ingredient not found");
            }
            var ingredientInRecipe = await _context.Set<RecipeIngredient>()
                .FindAsync(id, ing.IngredientId);
            if (ingredientInRecipe != null)
            {
                return Conflict("ingredient already in recipe");
            }

            var recipeIngredient = new RecipeIngredient
            {
                RecipeId = id,
                IngredientId = ing.IngredientId,
                Quantity = ing.Quantity,
                Unit = ing.Unit
            };

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRecipeIngredients", new { id = recipeIngredient.RecipeId }, recipeIngredient);
        }

        [HttpDelete("{id}/ingrediences")]
        public async Task<IActionResult>
            RemoveIngredientFromRecipe(int id, [FromBody] int ingredientId)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound("recipe not found");
            }
            var ingredient = await _context.Ingredients.FindAsync(ingredientId);
            if (ingredient == null)
            {
                return NotFound("ingredient not found");
            }
            var ingredientInRecipe = await _context.Set<RecipeIngredient>()
                .FindAsync(id, ingredientId);
            if (ingredientInRecipe == null)
            {
                return NotFound("ingredient not in recipe");
            }
            _context.Set<RecipeIngredient>().Remove(ingredientInRecipe);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeId == id);
        }
    }

    public record AddIngredientToRecipeDto(
        int IngredientId,
        int Quantity,
        string Unit);
    public record RecipeIngredientDto(
        int RecipeId,
        int IngredientId,
        string IngredientName,
        int Quantity,
        string Unit);

    public enum SortedBy
    {
        Id,
        Id_Desc,
        Title,
        Title_Desc,    
    }
}
