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

        // GET api/meals/5
        [HttpGet("{id}")]
        public ActionResult<Meal> Get(int id)
        {
            if (id < 1)
                return BadRequest("Id must be greater than 0");

            return Ok(_mealService.GetMealById(id));
        }

        // POST api/meals
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

        // PUT api/meals/5
        [HttpPut("{id}")]
        public ActionResult<Meal> Put(int id, [FromBody] Meal meal)
        {
            if (id < 1 || id != meal.Id)
                return BadRequest("Parameter id and meal Id must be the same");

            return Ok(_mealService.UpdateMeal(id, meal));
        }

        // DELETE api/meals/5
        [HttpDelete("{id}")]
        public ActionResult<Meal> Delete(int id)
        { 
            var mDelete = _mealService.DeleteMeal(id);
            
            return Ok(mDelete);
        }
    }
}