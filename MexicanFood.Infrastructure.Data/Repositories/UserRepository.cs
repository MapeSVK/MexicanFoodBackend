using System.Collections.Generic;
using MexicanFood.Core.DomainService;
using MexicanFood.Entities;

namespace MexicanFood.Infrastructure.Data.Repositories
{
	public class UserRepository : IRepository<User>
	{
		public IEnumerable<User> ReadAllEntities()
		{
			throw new System.NotImplementedException();
		}

		public User EntityFoundById(int id)
		{
			throw new System.NotImplementedException();
		}

		public User CreateEntity(User entity)
		{
			throw new System.NotImplementedException();
		}

		public User UpdateEntity(int id, User entity)
		{
			throw new System.NotImplementedException();
		}

		public User DeleteEntity(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}
