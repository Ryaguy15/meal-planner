
namespace MealPlanner.Models 
{
    public class Meal 
    {
        public int Id {get; set;}
        public string Name {get;set;}

        public virtual ICollection<Ingredient> Ingredients {get; set;}
    }
}