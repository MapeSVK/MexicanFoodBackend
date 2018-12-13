using MexicanFood.Core.Entities;
using MexicanFood.Entities;
using MexicanFood.Infrastructure.Data.Repositories.Helpers;
using System;

namespace MexicanFood.Infrastructure.Data.Repositories
{
    public class DBInitializer : IDBInitializer
    {
        private IAuthenticationHelper _authenticationHelper;

        public DBInitializer(IAuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }

        public void SeedDb(MexicanFoodContext ctx)
        {
            var mealRepository = new MealRepository(ctx);

            var meal1 = ctx.Meals.Add(new Meal()
            {
                Name = "meal1",
                Description = "description1",
                Ingredients = "ingredient1",
                Picture = "https://www.streetfoodesbjerg.dk/assets/La-bamba/9acea7d518/1__FitWzEyMDAsNzAwXQ.jpg",
                Price = 10
            }).Entity;

            var meal2 = ctx.Meals.Add(new Meal()
            {
                Name = "meal2",
                Description = "description2",
                Ingredients = "ingredient1",
                Picture = "PictureLink",
                Price = 20
            }).Entity;

            var order1 = ctx.Orders.Add(new Order()
            {
                OrderedDateAndTime = DateTime.Now,
                PickUpDateAndTime = DateTime.Now
            }).Entity;

            ctx.OrderLines.Add(new OrderLine
            {
                Meal = meal1,
                Order = order1
            });

            ctx.OrderLines.Add(new OrderLine
            {
                Meal = meal2,
                Order = order1
            });

            string password = "password";
            byte[] passwordHashAdmin, passwordSAltAdmin;
            
            _authenticationHelper.CreatePasswordHash(password, out passwordHashAdmin, out passwordSAltAdmin);

            ctx.Users.Add(new User
            {
                Username = "Admin",
                PasswordHash = passwordHashAdmin,
                PasswordSalt = passwordSAltAdmin,
                IsAdmin = true
            });

            ctx.SaveChanges();
        }
    }
}