using CommerceDAL.DAO;
using CommerceViewModels;
namespace CommerceTests
{
    public class DAOtests
    {
        private readonly ProductDAO _pDAO;
        private readonly StocksDAO _sDAO;

        public DAOtests(ProductDAO productDAO, StocksDAO stocksDAO) 
        {
            _pDAO = productDAO;
            _sDAO = stocksDAO;
        }
        [Fact]
        public async Task GetAllTest()
        {
            List<ProductViewModel> allProductsVms;
            ProductViewModel vm = new(_pDAO, _sDAO);
            allProductsVms = await vm.GetAll();
            Assert.True(allProductsVms.Count > 0);
        }
    }
}
