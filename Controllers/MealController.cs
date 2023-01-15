using MealPlanner.Models;
using MealPlanner.Respositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanner.Controllers 
{

    [ApiController]
    [Route("[controller]")]
    public class MealController : ControllerBase
    {

        private readonly IMealRepository _repo;
        public MealController(IMealRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public  async Task<ActionResult<IEnumerable<Meal>>> GetMeals() 
        {
            var meals = await _repo.GetMeals();
            return new OkObjectResult(meals);
            
        }

        [HttpPost]
        public async Task<ActionResult<Meal>> SaveMeal([FromBody] Meal mealToSave)
        {

            var savedMeal = await _repo.SaveMeal(mealToSave);
            return new OkObjectResult(savedMeal);
        }

        
    }
}