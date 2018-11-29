using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /*[Fact]
        public void CreateMealNameMissingThrowsException()
        {
            var mealRepository = new Mock<IRepository<Meal>>();
            
            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() { Id = 1 });

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                //Name = "testMeal",
                Ingredients = "pictureString",
                Description = "testDescription",
                Picture = "pictureString",
                Price = 10
            };

            Exception ex = Assert.Throws<InvalidDataException>(() => service.CreateMeal(meal));

            Assert.Equal("Meal needs a name", ex.Message);
        }*/

        [Fact]
        public void CreateMealIngredientMissingThrowsException()
        {
            var mealRepository = new Mock<IRepository<Meal>>();
            
            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() { Id = 1 });

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                Name = "testMeal",
                //Ingredients = "pictureString",
                Description = "testDescription",
                Picture = "pictureString",
                Price = 10
            };

            Exception ex = Assert.Throws<InvalidDataException>(() => service.CreateMeal(meal));

            Assert.Equal("Meal needs at least 1 ingredient", ex.Message);
        }

        [Fact]
        public void UpdateMealPriceMissingThrowsException()
        {
            var mealRepository = new Mock<IRepository<Meal>>();
            
            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() { Id = 1 });

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                Name = "testMeal",
                Ingredients = "pictureString",
                Description = "testDescription",
                Picture = "pictureString",
                //Price = 10
            };

            Exception ex = Assert.Throws<InvalidDataException>(() => service.UpdateMeal(1, meal));

            Assert.Equal("Meal needs a price", ex.Message);
        }

        [Fact]
        public void UpdateMealNameMissingThrowsException()
        {
            var mealRepository = new Mock<IRepository<Meal>>();
            
            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() { Id = 1 });

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                //Name = "testMeal",
                Ingredients = "pictureString",
                Description = "testDescription",
                Picture = "pictureString",
                Price = 10
            };

            Exception ex = Assert.Throws<InvalidDataException>(() => service.UpdateMeal(1, meal));

            Assert.Equal("Meal needs a name", ex.Message);
        }

        [Fact]
        public void UpdateMealIngredientMissingThrowsException()
        {
            var mealRepository = new Mock<IRepository<Meal>>();

            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() { Id = 1 });

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                Name = "testMeal",
                //Ingredients = "pictureString",
                Description = "testDescription",
                Picture = "pictureString",
                Price = 10
            };

            Exception ex = Assert.Throws<InvalidDataException>(() => service.UpdateMeal(1, meal));

            Assert.Equal("Meal needs at least 1 ingredient", ex.Message);
        }

        [Fact]
        public void CreateMealPriceMissingThrowsException()
        {
            var mealRepository = new Mock<IRepository<Meal>>();

            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() { Id = 1 });

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                Name = "testMeal",
                Ingredients = "pictureString",
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
            
            mealRepository.Setup(m => m.ReadAllEntities()).Returns(new List<Meal>());

            IMealService service = new MealService(mealRepository.Object);
           
            service.GetMeals(); //goes wrong here

            mealRepository.Verify(m => m.ReadAllEntities(), Times.Once);
        }

        [Fact]
        public void MealFoundByIdShouldCallMealRepositoryOnce()
        {
            var mealRepository = new Mock<IRepository<Meal>>();
            
            mealRepository.Setup(m => m.EntityFoundById(1));

            IMealService service = new MealService(mealRepository.Object);

            service.MealFoundById(1);

            mealRepository.Verify(m => m.EntityFoundById(1), Times.Once);
        }

        /*[Fact]
        public void CreateMealShouldCallMealRepositoryCreateEntityOnce()
        {
            var mealRepository = new Mock<IRepository<Meal>>();
            
            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() { Id = 1 });

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                Name = "testMeal",
                Ingredients = "pictureString",
                Description = "testDescription",
                Picture = "pictureString",
                Price = 10
            };

            service.CreateMeal(meal);

            mealRepository.Verify(m => m.CreateEntity(It.IsAny<Meal>()), Times.Once);
        }*/

        [Fact]
        public void UpdateMealShouldCallMealRepositoryCreateEntityOnce()
        {
            var mealRepository = new Mock<IRepository<Meal>>();
            
            mealRepository.Setup(m => m.EntityFoundById(It.IsAny<int>())).Returns(new Meal() { Id = 1 });

            IMealService service = new MealService(mealRepository.Object);

            var meal = new Meal()
            {
                Name = "testMeal",
                Ingredients = "pictureString",
                Description = "testDescription",
                Picture = "pictureString",
                Price = 10
            };

            service.UpdateMeal(1, meal);
           
            mealRepository.Verify(m => m.UpdateEntity(1, It.IsAny<Meal>()), Times.Once);
        }

        [Fact]
        public void DeleteMealShouldCallMealRepositoryOnce()
        {
            var mealRepository = new Mock<IRepository<Meal>>();
            
            mealRepository.Setup(m => m.DeleteEntity(1));

            IMealService service = new MealService(mealRepository.Object);

            service.DeleteMeal(1);

            mealRepository.Verify(m => m.DeleteEntity(1), Times.Once);
        }
    }
}