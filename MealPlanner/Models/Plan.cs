
namespace MealPlanner.Models
{
	public class Plan
	{
		public int Id { get; set; }
		public int Length { get; set; }
		public ICollection<Meal> Meals { get; set; }
	}
}

