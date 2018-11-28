using System.Collections.Generic;
using MexicanFood.Entities;

namespace MexicanFood.Core.ApplicationService
{
    public interface IMealService
    {
        List<Meal> GetMeals();

        Meal MealFoundById(int id);

        Meal CreateMeal(Meal meal);

        Meal UpdateMeal(int id, Meal mealUpdate);

        Meal DeleteMeal(int id);
    }
}