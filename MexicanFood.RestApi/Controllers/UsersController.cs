using System;
using System.Collections.Generic;
using MexicanFood.Core.ApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace MexicanFood.RestApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
        readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/user    
        [HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
            try
            {
                return Ok(_userService.GetUsers());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

		// GET api/user/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "You cant get specific users";
		}

		// POST api/user
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/user/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/user/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
