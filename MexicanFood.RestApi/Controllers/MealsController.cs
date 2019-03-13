using System;
using System.Collections.Generic;
using MexicanFood.Core.ApplicationService;
using MexicanFood.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MexicanFood.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        readonly IMealService _mealService;

        public MealsController(IMealService mealService)
        {
            _mealService = mealService;
        }
        
        /**
         * Returns a list of Meals.
         */
        // GET api/meals
        [HttpGet]
        public ActionResult<List<Meal>> Get()
        {
            try
            {
                return Ok(_mealService.GetMeals());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /**
         * Takes an int id, checks that the id is greater then 0,
         * if it is not, returns BadRequest. Otherwise requests the
         * service for a Meal with the passed id.
         */
        // GET api/meals/5
        [HttpGet("{id}")]
        public ActionResult<Meal> Get(int id)
        {
            if (id < 1)
                return BadRequest("Id must be greater than 0");

            return Ok(_mealService.GetMealById(id));
        }

        /**
         * Requests the service to create the Meal created from the
         * passed [FromBody].
         * Returns the created Meal.
         */
        // POST api/meals
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Meal> Post([FromBody] Meal meal)
        {
            try
            {
                return Ok(_mealService.CreateMeal(meal));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /**
         * Takes an int id and creates a Meal from the passed [FromBody],
         * checks if the id or the created meals id are greater then 0, if either
         * are not, returns BadRequest. otherwise requests the service to update
         * the Meal created.
         * Returns the Meal created.
         */
        // PUT api/meals/5

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Meal> Put(int id, [FromBody] Meal meal)
        {
            if (id < 1 || id != meal.Id)
                return BadRequest("Parameter id and meal Id must be the same");

            return Ok(_mealService.UpdateMeal(id, meal));
        }

        /**
         * Takes an int id and requests the service to delete the specified meal.
         * Returns the Meal to be deleted.
         */
        // DELETE api/meals/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Meal> Delete(int id)
        { 
            var mDelete = _mealService.DeleteMeal(id);
            
            return Ok(mDelete);
        }
    }
}