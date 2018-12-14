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

		/**
		 * Returns the user from the database.
		 */
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
	}
}
