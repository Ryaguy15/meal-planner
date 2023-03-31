using MealPlanner.DataSources;
using MealPlanner.Models;
using MealPlanner.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MealPlanner.Respositories
{
    public class MealRepository : IMealRepository
    {
        public readonly MealPlanningContext _dbContext;

        public MealRepository(MealPlanningContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Count()
        {
            return await _dbContext.Meals.CountAsync();
        }

        public async Task<Meal> GetTopItemFromSort(Func<Meal, Meal, int> filter, bool getBottom = false)
        {
            var comparer = Comparer<Meal>.Create((a, b) => filter(a, b));
            var orderedMeals = _dbContext.Meals.Order(comparer);

            return getBottom ? await orderedMeals.LastAsync() : await orderedMeals.FirstAsync();

        }

        public async Task<IEnumerable<Meal>> GetItems()
        {
            var meals = await _dbContext.Meals.Include(m => m.Ingredients).ToListAsync();

            return meals;
        }

        public async Task<IEnumerable<Meal>> GetItems(Func<Meal, bool> filter)
        {
            //TODO This won't work. Linq can't covert this to sql internally
            return await _dbContext.Meals.Where(m => filter(m)).ToListAsync();
        }


        public async Task<Meal> SaveItem(Meal itemToSave)
        {
            var savedMeal = (await _dbContext.AddAsync(itemToSave)).Entity;

            await _dbContext.SaveChangesAsync();

            return savedMeal;
        }

        public async Task<Meal?> GetItemByUniqueValue(Func<Meal, bool> filter)
        {
            return (await _dbContext.Meals.Include(m => m.Ingredients).ToListAsync())
                .Where(m => filter(m))
                .FirstOrDefault();

        }

        public async Task<Meal> UpdateItem(Meal updatedItem)
        {
            var mealToUpdate = await _dbContext.Meals.FindAsync(updatedItem.Id);
            if (mealToUpdate != null)
            {
                _dbContext.Meals.Entry(mealToUpdate).CurrentValues.SetValues(updatedItem);
                await _dbContext.SaveChangesAsync();
                return updatedItem;
            }

            throw new KeyNotFoundException($"Could not find meal with id: {updatedItem.Id}");

        }
    }
}