using CommerceDAL.DAO;
using CommerceViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace CommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDAO _pDAO;
        private readonly StocksDAO _sDAO;

        public ProductController(ProductDAO productDAO, StocksDAO stocksDAO)
        {
            _pDAO = productDAO;
            _sDAO = stocksDAO;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                ProductViewModel viewmodel = new(_pDAO, _sDAO);
                List<ProductViewModel> allProducts = await viewmodel.GetAll();
                return Ok(allProducts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }
    }
}
