using System;
using System.Collections.Generic;
using MexicanFood.Core.ApplicationService;
using MexicanFood.Entities;
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
        
        // GET api/values
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

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Meal> Get(int id)
        {
            if (id < 1)
                return BadRequest("Id must be greater then 0");

            return Ok(_mealService.MealFoundById(id));
        }

        // POST api/values
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Meal> Put(int id, [FromBody] Meal meal)
        {
            if (id < 1 || id != meal.Id)
                return BadRequest("Parameter Id and order ID must be the same");

            return Ok(_mealService.UpdateMeal(id, meal));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<Meal> Delete(int id)
        {
            return Ok("Meal with id: " + id);
        }
    }
}