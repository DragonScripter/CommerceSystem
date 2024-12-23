using CommerceDAL.DAO;
using CommerceDAL.Entities;
using CommerceDAL.Repository.Implementation;
using CommerceDAL.Repository.Interface;
using CommerceViewModels;

namespace CommerceWebsite.Helpers
{
    public static class Helper
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Product>, CommerceRepository<Product>>();
            services.AddScoped<IRepository<Stocks>, CommerceRepository<Stocks>>();
            services.AddScoped<IRepository<Users>, CommerceRepository<Users>>();
        }

        public static void AddDAOs(this IServiceCollection services)
        {
            services.AddScoped<ProductDAO>();
            services.AddScoped<StocksDAO>();
        }

        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddScoped<ProductViewModel>();
        }
    }
}
