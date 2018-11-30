using MexicanFood.Core.DomainService;
using MexicanFood.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MexicanFood.Infrastructure.Data.Repositories
{
    public class MealRepository : IRepository<Meal>
    {
        readonly MexicanFoodContext _ctx;

        public MealRepository(MexicanFoodContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Meal> ReadAllEntities()
        {
            return _ctx.Meals;
        }

        public Meal EntityFoundById(int id)
        {
            return _ctx.Meals.FirstOrDefault(meal => meal.Id == id);
        }

        public Meal CreateEntity(Meal meal)
        {
            _ctx.Attach(meal).State = EntityState.Added;
            _ctx.SaveChanges();
            return meal;
        }

        public Meal UpdateEntity(Meal mealUpdate)
        {
            _ctx.Attach(mealUpdate).State = EntityState.Modified;
            //_ctx.Entry(mealUpdate).Reference(o => o.Order).IsModified = true;
            _ctx.SaveChanges();
            return mealUpdate;
        }

        public Meal DeleteEntity(int id)
        {
            var removed = _ctx.Remove(new Meal {Id = id}).Entity;
            _ctx.SaveChanges();
            return removed;
        }
    }
}