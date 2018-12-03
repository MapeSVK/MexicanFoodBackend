using System.Collections.Generic;
using MexicanFood.Entities;

namespace MexicanFood.Core.ApplicationService
{
	public interface IUserService
	{
		List<User> GetUsers();
	}
}
