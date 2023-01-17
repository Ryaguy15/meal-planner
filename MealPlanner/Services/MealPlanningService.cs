using System;
using MealPlanner.Models;
using MealPlanner.Repositories.Interfaces;

namespace MealPlanner.Services
{
	public class MealPlanningService
	{
        private readonly IMealRepository _mealRepo;

        public MealPlanningService(IMealRepository mealRepo)
		{
			_mealRepo = mealRepo;
		}

		public async Task<Plan> CreateNewMealPlan(int length, Plan? lastPlan)
		{
			var mealsNotInLastPlan = await _mealRepo.GetItems(m => !lastPlan?.Meals.Contains(m) ?? true);
			var plan = new Plan();
			plan.Meals = new List<Meal>();

			if (length >= mealsNotInLastPlan.Count())
			{
				plan.Meals = mealsNotInLastPlan.ToList();
			}

			// grab length amount of random meals
			while (plan.Meals.Count() < length)
			{
				var randomEntry = new Random().Next(mealsNotInLastPlan.Count() - 1);

				// did we already grab that meal before
				var meal = mealsNotInLastPlan.ElementAt(randomEntry);
				if (plan.Meals.Any(m => m.Id == meal.Id)) continue;

				plan.Meals = plan.Meals.Append(meal).ToList();

			}

			return plan;
		}
	}
}

