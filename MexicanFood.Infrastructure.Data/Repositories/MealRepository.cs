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

        public IEnumerable<Meal> ReadAll()
        {
            return _ctx.Meals;
        }

        public Meal ReadById(int id)
        {
            return _ctx.Meals
                .Include(m => m.OrderLines)
                .ThenInclude(ol => ol.Order)
                .FirstOrDefault(m => m.Id == id);
        }

        public Meal CreateEntity(Meal meal)
        {
            _ctx.Attach(meal).State = EntityState.Added;
            _ctx.SaveChanges();
            return meal;
        }

        public Meal UpdateEntity(Meal mealUpdate)
        {

            //Attach product so basic properties are updated
            _ctx.Attach(mealUpdate).State = EntityState.Modified;

            _ctx.SaveChanges();

            return mealUpdate;
        }

        public Meal DeleteEntity(int id)
        {  
            var remove = _ctx.Remove(new Meal { Id = id }).Entity;
            _ctx.SaveChanges();
            return remove;
        }
    }
}