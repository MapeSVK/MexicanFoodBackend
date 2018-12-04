namespace MexicanFood.Infrastructure.Data.Repositories
{
    public interface IDBInitializer
    {
        void SeedDb(MexicanFoodContext ctx);
    }
}