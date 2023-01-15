
namespace MealPlanner.Models
{
	public class Plan
	{
		public int Id { get; set; }
		public int Length { get; set; }
		public virtual ICollection<Meal> Meals { get; set; }
	}
}

