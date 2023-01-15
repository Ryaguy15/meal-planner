using MealPlanner.DataSources;
using MealPlanner.Models;
using MealPlanner.Respositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MealPlanner.Respositories
{
    public class MealRepository: IMealRepository
    {
        public readonly MealPlanningContext _dbContext;

        public MealRepository(MealPlanningContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Meal>> GetMeals() 
        {
            var meals = await _dbContext.Meals.Include(m => m.Ingredients).ToListAsync();
            
            return meals;
        }

        public async Task<Meal> SaveMeal(Meal meal)
        {
        
            var savedMeal = (await _dbContext.AddAsync(meal)).Entity;

            await _dbContext.SaveChangesAsync();

            return savedMeal;
        }
    }
}