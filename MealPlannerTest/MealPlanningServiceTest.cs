using System;
using MealPlanner.Models;
using MealPlanner.Repositories.Interfaces;
using MealPlanner.Services;
using Moq;

namespace MealPlannerTest
{
	public class MealPlanningServiceTest
	{
        private readonly Mock<IMealRepository> mealRepo;
        private readonly MealPlanningService service;

        public MealPlanningServiceTest()
		{
			mealRepo = new Mock<IMealRepository>();
			service = new MealPlanningService(mealRepo.Object);
		}


		private List<Meal> GenerateTestMeals()
		{
			return new List<Meal>
			{
				new Meal
				{
					Id = 1
				},
				new Meal
				{
					Id = 2
				},
				new Meal
				{
					Id = 3
				}
			};
		}

		[Fact]
		public async void GeneratesMealNotInLastPlan()
		{
			var meals = GenerateTestMeals();
			var lastPlan = new Plan();
			lastPlan.Meals = new List<Meal> { meals.First() };

			mealRepo.Setup(mr => mr.GetItems(It.IsAny<Func<Meal,bool>>())).ReturnsAsync(meals.Skip(1));
			var result = await service.CreateNewMealPlan(2, lastPlan);
			Assert.Equal(2, result.Meals.Count);
			Assert.DoesNotContain<Meal>(meals.First(), result.Meals);

		}
	}
}

