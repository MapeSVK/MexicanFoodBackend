using System.Collections.Generic;
using MexicanFood.Core.DomainService;
using MexicanFood.Entities;

namespace MexicanFood.Infrastructure.Data.Repositories
{
	public class UserRepository : IRepository<User>
	{
		readonly MexicanFoodContext _ctx;
		public UserRepository(MexicanFoodContext ctx)
		{
			_ctx = ctx;
		}
		
		public IEnumerable<User> ReadAllEntities()
		{
			return _ctx.Users;
		}

		public User EntityFoundById(int id)
		{
			throw new System.NotImplementedException();
		}

		public User CreateEntity(User entity)
		{
			throw new System.NotImplementedException();
		}

		public User UpdateEntity(User entity)
		{
			throw new System.NotImplementedException();
		}

		public User DeleteEntity(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}
