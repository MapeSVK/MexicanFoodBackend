using MexicanFood.Core.Entities;
using MexicanFood.Entities;
using Microsoft.EntityFrameworkCore;

namespace MexicanFood.Infrastructure.Data.Repositories
{
    public class MexicanFoodContext : DbContext
    {
        public MexicanFoodContext(DbContextOptions<MexicanFoodContext> opt) : base(opt)
        {
        }

        //Might need a Order value in meal to make a one to many relation
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<>()
                .HasOne( => )
                .WithMany( => )
                .OnDelete(DeleteBehavior.SetNull);
        }
        */

        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}