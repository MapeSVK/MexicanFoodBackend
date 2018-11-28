using System;
using System.IO;
using MexicanFood.Core.ApplicationService;
using MexicanFood.Core.ApplicationService.Implementation;
using MexicanFood.Core.DomainService;
using MexicanFood.Entities;
using Moq;
using Xunit;

namespace MexicanFood.XUnitTest
{
    public class MealServiceTest
    {
        [Fact]
        public void CreateMealNameMissingThrowsException()
        {
            var mealRepository = new Mock<IRepository<Meal>>();

            //Need to understand this better
            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() {Id = 1});

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                //Name = "testMeal",
                Ingredients = new string[]
                {
                    "testIngredient1",
                    "testIngredient2"
                },
                Description = "testDescription",
                Picture = "pictureString",
                Price = 10
            };

            Exception ex = Assert.Throws<InvalidDataException>(() => service.CreateMeal(meal));

            Assert.Equal("Meal needs a name", ex.Message);
        }

        [Fact]
        public void CreateMealIngredientMissingThrowsException()
        {
            var mealRepository = new Mock<IRepository<Meal>>();

            //Need to understand this better
            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() {Id = 1});

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                Name = "testMeal",
                /*
                Ingredients = new string[]
                {
                    "testIngredient1",
                    "testIngredient2"
                },
                */
                Description = "testDescription",
                Picture = "pictureString",
                Price = 10
            };

            Exception ex = Assert.Throws<InvalidDataException>(() => service.CreateMeal(meal));

            Assert.Equal("Meal needs at least 1 ingredient", ex.Message);
        }

        [Fact]
        public void CreateMealPriceMissingThrowsException()
        {
            var mealRepository = new Mock<IRepository<Meal>>();

            //Need to understand this better
            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() {Id = 1});

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                Name = "testMeal",
                Ingredients = new string[]
                {
                    "testIngredient1",
                    "testIngredient2"
                },
                Description = "testDescription",
                Picture = "pictureString",
                //Price = 10
            };

            Exception ex = Assert.Throws<InvalidDataException>(() => service.CreateMeal(meal));

            Assert.Equal("Meal needs a price", ex.Message);
        }

        [Fact]
        public void GetMealsShouldCallMealRepositoryOnce()
        {
            var mealRepository = new Mock<IRepository<Meal>>();

            //Need to understand this better
            mealRepository.Setup(m => m.ReadAllEntities());

            IMealService service = new MealService(mealRepository.Object);

            service.GetMeals();

            mealRepository.Verify(m => m.EntityFoundById(1), Times.Once);
        }
        
        [Fact]
        public void MealFoundByIdShouldCallMealRepositoryOnce()
        {
            var mealRepository = new Mock<IRepository<Meal>>();

            //Need to understand this better
            mealRepository.Setup(m => m.EntityFoundById(1));

            IMealService service = new MealService(mealRepository.Object);

            service.MealFoundById(1);

            mealRepository.Verify(m => m.EntityFoundById(1), Times.Once);
        }

        [Fact]
        public void CreateMealShouldCallMealRepositoryCreateEntityOnce()
        {
            var mealRepository = new Mock<IRepository<Meal>>();

            //Need to understand this better
            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() {Id = 1});

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                Name = "testMeal",
                Ingredients = new string[]
                {
                    "testIngredient1",
                    "testIngredient2"
                },
                Description = "testDescription",
                Picture = "pictureString",
                //Price = 10
            };

            service.CreateMeal(meal);

            mealRepository.Verify(m => m.CreateEntity(It.IsAny<Meal>()), Times.Once);
        }

        //Need a updateCalledOnceTest but needs to resolve generic issue

        [Fact]
        public void DeleteMealShouldCallMealRepositoryOnce()
        {
            var mealRepository = new Mock<IRepository<Meal>>();

            //Need to understand this better
            mealRepository.Setup(m => m.DeleteEntity(1));

            IMealService service = new MealService(mealRepository.Object);

            service.DeleteMeal(1);

            mealRepository.Verify(m => m.DeleteEntity(1), Times.Once);
        }
    }
}