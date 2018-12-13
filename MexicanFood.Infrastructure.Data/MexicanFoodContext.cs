using MexicanFood.Core.Entities;
using MexicanFood.Entities;
using Microsoft.EntityFrameworkCore;

namespace MexicanFood.Infrastructure.Data.Repositories
{
    public class MexicanFoodContext : DbContext
    {
        public MexicanFoodContext(DbContextOptions<MexicanFoodContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Creates a combined key with OrderId and MealId
            modelBuilder.Entity<OrderLine>()
                .HasKey(ol => new { ol.OrderId, ol.MealId });

            //Creates a binding between Order and OrderLines
            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Order)
                .WithMany(o => o.OrderLines)
                .HasForeignKey(ol => ol.OrderId);

            //Creates a binding between Meal and OrderLines
            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Meal)
                .WithMany(m => m.OrderLines)
                .HasForeignKey(ol => ol.MealId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }
		public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine>  OrderLines { get; set; }
    }
}