using System.Collections.Generic;
using System.IO;
using System.Linq;
using MexicanFood.Core.DomainService;
using MexicanFood.Entities;

namespace MexicanFood.Core.ApplicationService.Implementation
{
    public class MealService : IMealService
    {
        private readonly IRepository<Meal> _mealRepository;

        public MealService(IRepository<Meal> mealRepository)
        {
            _mealRepository = mealRepository;
        }
        
        /**
         * Calls the repository to return all meals and converts the IEnumerable to
         * a list.
         */
        public List<Meal> GetMeals()
        {
            return _mealRepository.ReadAll().ToList();
        }

        /**
         * Takes an int id and calls the repository to return a Meal with the
         * specified id.
         */
        public Meal GetMealById(int id)
        {
            return _mealRepository.ReadById(id);
        }

        /**
         * Checks if the Meal passed has a name, ingredients and a price, before
         * passing it to the repository to be created in the database.
         * Returns the Meal passed.
         */
        public Meal CreateMeal(Meal meal)
        {
            if (string.IsNullOrEmpty(meal.Name))
                throw new InvalidDataException("Meal needs a name");
            
            if (meal.Ingredients == null)
                throw new InvalidDataException("Meal needs at least 1 ingredient");
            
            if (meal.Price == 0)
                throw new InvalidDataException("Meal needs a price");
            
            _mealRepository.CreateEntity(meal);
            
            return meal;
        }

        /**
         * Checks if the Meal passed has a name, ingredients and a price, before
         * passing it to the repository to update the meal with the matching id
         * of the passed Meal.
         * Returns the Meal passed.
         */
        public Meal UpdateMeal(int id, Meal mealUpdate)
        {
            if (string.IsNullOrEmpty(mealUpdate.Name))
                throw new InvalidDataException("Meal needs a name");
            
            if (mealUpdate.Ingredients == null)
                throw new InvalidDataException("Meal needs at least 1 ingredient");
            
            if (mealUpdate.Price == 0)
                throw new InvalidDataException("Meal needs a price");
            
            return _mealRepository.UpdateEntity(mealUpdate);
        }

        /**
         * Takes an int id, returns the Meal with the specified id, and requests
         * the repository to delete it.
         */
        public Meal DeleteMeal(int id)
        {
            return _mealRepository.DeleteEntity(id);
        }
    }
}