using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.InMemory;
using CommerceDAL.DAO;
using CommerceDAL;
using CommerceDAL.Entities;
using CommerceDAL.Repository.Implementation;
using CommerceDAL.Repository.Interface;
namespace CommerceTests
{
    public class DAOtests
    {
        private readonly ProductDAO _pDAO;
        private readonly StocksDAO _sDAO;

        //public DAOtests()
        //{
        //    // set the service
        //    var services = new ServiceCollection();

        //    // in mem database
        //    services.AddDbContext<CommerceContext>(options =>
        //        options.UseInMemoryDatabase("TestCommerceDb"));

        //    services.AddScoped<IRepository<Product>, CommerceRepository<Product>>();
        //    services.AddScoped<IRepository<Stocks>, CommerceRepository<Stocks>>();

        //    // just adding them to service
        //    services.AddScoped<ProductDAO>();
        //    services.AddScoped<StocksDAO>();

           
        //    var serviceProvider = services.BuildServiceProvider();


        //    _pDAO = serviceProvider.GetRequiredService<ProductDAO>();
        //    _sDAO = serviceProvider.GetRequiredService<StocksDAO>();

        //    // Seeding
        //    SeedDatabase(serviceProvider);
        //}


        //private void SeedDatabase(ServiceProvider serviceProvider)
        //{
        //    var context = serviceProvider.GetRequiredService<CommerceContext>();

        //    context.Product.AddRange(new Product
        //    {
        //        Id = 1,
        //        Name = "Product 1",
        //        Description = "Sample product description",
        //        Price = 100,
        //        Timer = new byte[] { 1, 2, 3 },
        //        ImageData = Array.Empty<string>()
        //    });

        //    context.Stocks.AddRange(new Stocks
        //    {
        //        Id = 1,
        //        ProductId = 1,
        //        Quanity = 50
        //    });

        //    context.SaveChanges();
        //}

        //[Fact]
        //public async Task GetAllTest()
        //{

        //    var allProductsVms = await _pDAO.GetAll();
        //    Assert.True(allProductsVms.Count > 0);
        //}
    }
}
