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
            return await _dbContext.Meals.ToListAsync();
        }

        public async Task<Meal> SaveMeal(Meal meal)
        {
            // add ingredients if not in database
            var notSavedIngredients = meal.Ingredients.Where(i =>
            {
                return _dbContext.Ingredients.Where(dI => dI.Name == i.Name).Count() > 0;
            });

            if (notSavedIngredients.Count() >= 1) await _dbContext.Ingredients.AddRangeAsync(notSavedIngredients);

            var savedMeal = (await _dbContext.AddAsync(meal)).Entity;

            await _dbContext.SaveChangesAsync();

            return savedMeal;
        }
    }
}