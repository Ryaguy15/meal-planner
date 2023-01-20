


using MealPlanner.Models;
using MealPlanner.Repositories.Interfaces;
using MealPlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanner.Controllers 
{
    [Route("plan")]
    public class PlanController : ControllerBase 
    {
        private readonly IPlanRepository _planRepo;
        private readonly MealPlanningService _service;

        public PlanController(IPlanRepository planRepo, MealPlanningService mealPlanService)
        {
            _planRepo = planRepo;
            _service = mealPlanService;

        }

        [HttpGet("today")]
        public async Task<ActionResult<Plan>> TodaysActivePlan()
        {
            var today = DateTime.UtcNow;
            var noPlans = await _planRepo.Count() < 1;
            if (noPlans) return new NoContentResult();
            var lastedPlan = await _planRepo.GetTopItemFromSort((a, b) => a.StartDate.CompareTo(b));

            var lastDayInPlan = lastedPlan.StartDate.AddDays(lastedPlan.Length);
            if (lastDayInPlan.CompareTo(today) == -1) return new NotFoundObjectResult("No plans active today");
            return Ok(lastedPlan);
        }

        [HttpPost]
        public async Task<ActionResult<Plan>> SavePlan([FromBody] int length)
        {
            var yesterday = DateTime.UtcNow.AddDays(-1);
            var planActiveOnYesterday = await _planRepo.GetTopItemFromSort((a, b) => a.StartDate.CompareTo(b));

            var plan = await _service.CreateNewMealPlan(length, planActiveOnYesterday);

            return await _planRepo.SaveItem(plan);

        }
    }
}