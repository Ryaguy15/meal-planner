using MealPlanner.Models;

namespace MealPlanner.Respositories.Interfaces 
{
    public interface IMealRepository 
    {
        Task<IEnumerable<Meal>> GetMeals();
        Task<Meal> SaveMeal(Meal meal);
    }
}