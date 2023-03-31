using MealPlanner.Models;
using MealPlanner.Repositories.Interfaces;
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
            var meals = await _repo.GetItems();
            return new OkObjectResult(meals);
            
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Meal>> GetMealById([FromRoute] int id)
        {
            return Ok(await _repo.GetItemByUniqueValue(meal => meal.Id == id));
        }


        [HttpPost]
        public async Task<ActionResult<Meal>> SaveMeal([FromBody] Meal mealToSave)
        {

            var savedMeal = await _repo.SaveItem(mealToSave);
            return new OkObjectResult(savedMeal);
        }

        [HttpPut]
        public async Task<ActionResult<Meal>> UpdateMeal([FromBody] Meal mealToUpdate)
        {
            if (mealToUpdate.Id == null)
            {
                return new UnprocessableEntityObjectResult("Can't update meal with null id");
            }

            try
            {
                return await _repo.UpdateItem(mealToUpdate);
            }
            catch (KeyNotFoundException notFound)
            {
                return NotFound(notFound.Message);
            }

        }

        
    }
}