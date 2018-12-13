using MexicanFood.Core.DomainService;
using MexicanFood.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MexicanFood.Core.Entities;

namespace MexicanFood.Infrastructure.Data.Repositories
{
    public class MealRepository : IRepository<Meal>
    {
        readonly MexicanFoodContext _ctx;

        public MealRepository(MexicanFoodContext ctx)
        {
            _ctx = ctx;
        }

        /**
         * Returns the meals in the database.
         */
        public IEnumerable<Meal> ReadAll()
        {
            return _ctx.Meals;
        }

        /**
         * Takes an int id and returns the first found meal with matching id or default,
         * including OrderLine objects where id matches MealId, linked to the the Order
         * that matches MealId.
         */
        public Meal ReadById(int id)
        {
            return _ctx.Meals
                .Include(m => m.OrderLines)
                .ThenInclude(ol => ol.Order)
                .FirstOrDefault(m => m.Id == id);
        }

        /**
         * Takes a Meal and attaches it to the local representation of the database,
         * then saves the changes to the local database representation to the actual
         * database.
         * Returns the Meal passed.
         */
        public Meal CreateEntity(Meal meal)
        {
            _ctx.Attach(meal).State = EntityState.Added;
            _ctx.SaveChanges();
            return meal;
        }

        /**
         * Takes a meal and updates the local representation of that meal, then saves
         * the changes to the actual database.
         * Returns the Meal passed.
         */
        public Meal UpdateEntity(Meal mealUpdate)
        {

            //Attach product so basic properties are updated
            _ctx.Attach(mealUpdate).State = EntityState.Modified;

            _ctx.SaveChanges();

            return mealUpdate;
        }

        /**
         * Takes an int id, finds the meal with matching id, saves it to a variable and
         * removes the meal with matching id in the local representation of the database,
         * then saves the changes to the actual database, then returns the meal saved to
         * the variable.
         */
        public Meal DeleteEntity(int id)
        {
            var remove = _ctx.Remove(new Meal {Id = id}).Entity;
            _ctx.SaveChanges();
            return remove;
        }
    }
}