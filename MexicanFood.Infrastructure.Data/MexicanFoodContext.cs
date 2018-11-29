using MexicanFood.Core.Entities;
using MexicanFood.Entities;
using Microsoft.EntityFrameworkCore;

namespace MexicanFood.Infrastructure.Data.Repositories
{
    public class MexicanFoodContext : DbContext
    {
        public MexicanFoodContext(DbContextOptions<MexicanFoodContext> opt) : base(opt) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}