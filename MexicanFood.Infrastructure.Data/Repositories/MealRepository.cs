﻿using MexicanFood.Core.DomainService;
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
         * Takes a Meal and checks if its OrderLine list is empty, if it is, it will
         * update the local representation of the meal, the Meal passed represented,
         * with the new data, then save the changes to the real database.
         * If OrderLines is not null, the method makes a copy of the OrderLines,
         * removes the OrderLines matching the Meals id, and adds the copied entries
         * to the local representation, then saves changes to the actual database.
         * Returns the Meal passed.
         * (The removal and adding is to avoid problems with the updated meals relations)
         */
        public Meal UpdateEntity(Meal mealUpdate)
        {
            var newOrderLines = new List<OrderLine>();

            if (mealUpdate.OrderLines != null)
            {
                //Clone orderlines to new location in memory, so they are not overridden on Attach
                newOrderLines = new List<OrderLine>(mealUpdate.OrderLines);
            }

            //Attach product so basic properties are updated
            _ctx.Attach(mealUpdate).State = EntityState.Modified;

            //Remove all orderlines with updated order information
            _ctx.OrderLines.RemoveRange(_ctx.OrderLines.Where(ol => ol.MealId == mealUpdate.Id));

            //Add all orderlines with updated order information
            foreach (var ol in newOrderLines)
            {
                _ctx.Entry(ol).State = EntityState.Added;
            }

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