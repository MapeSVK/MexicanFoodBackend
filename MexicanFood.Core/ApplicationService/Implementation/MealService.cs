using System;
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
        
        public List<Meal> GetMeals()
        {
            return _mealRepository.ReadAll().ToList();
        }

        public Meal MealFoundById(int id)
        {
            return _mealRepository.ReadById(id);
        }

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

        public Meal DeleteMeal(int id)
        {
            return _mealRepository.DeleteEntity(id);
        }
    }
}