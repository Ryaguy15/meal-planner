using Moq;

using MealPlanner.Controllers;
using MealPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using MealPlanner.Repositories.Interfaces;

namespace MealPlannerTest;

public class MealControllerTest
{
    private readonly Mock<IMealRepository> repo;
    private readonly MealController controller;

    public MealControllerTest()
    {
        this.repo = new Mock<IMealRepository>();
        this.controller = new MealController(this.repo.Object);
    }

    [Fact]
    public async void TestGetMeals()
    {
        var fakeMeal = new Meal()
        {
            Id = 1,
            Name = "UT"
        };

        repo.Setup(r => r.GetItems()).ReturnsAsync(new List<Meal> { fakeMeal });

        var result = (await controller.GetMeals()).Result as OkObjectResult;
        var mealsGotten = result.Value as IEnumerable<Meal>;
        Assert.NotNull(mealsGotten);
        Assert.Equal("UT", mealsGotten.First().Name);

    }
}
