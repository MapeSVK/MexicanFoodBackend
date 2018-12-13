using System.Collections.Generic;
using System.Linq;
using MexicanFood.Core.DomainService;
using MexicanFood.Entities;

namespace MexicanFood.Core.ApplicationService.Implementation
{
	public class UserService: IUserService
	{
		private readonly IRepository<User> _userRepository;

		public UserService(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}
		
		/**
		 * Requests the repository to return the user from the
		 * database.
		 */
		public List<User> GetUsers()
		{
			return _userRepository.ReadAll().ToList();
		}
	}
}
