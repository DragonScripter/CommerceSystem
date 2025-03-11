using CommerceDAL;
using CommerceDAL.DAO;
using CommerceViewModels;
using Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Services.Services;

namespace CommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDAO _pDAO;
        private readonly StocksDAO _sDAO;
        private readonly UserDAO _uDAO;
        private readonly AuthService _authService;
        private readonly CommerceContext _context;

        //public ProductController(ProductDAO productDAO, StocksDAO stocksDAO)
        //{
        //    _pDAO = productDAO;
        //    _sDAO = stocksDAO;
        //}
        public ProductController(ProductDAO productDAO, StocksDAO stocksDAO, UserDAO userDAO, AuthService authService ,CommerceContext context)
        {
            _pDAO = productDAO;
            _sDAO = stocksDAO;
            _uDAO = userDAO;
            _authService = authService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return BadRequest(new { Message = "Email and password are required." });
            }
            var user = await _uDAO.GetByEmail(loginRequest.Email);
            if (user == null || !_authService.Verify(loginRequest.Password, user.PasswordHash)) 
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }
            var token = _authService.GenerateJwtToken(user);

            return Ok(new { Token = token, Message = "Login Sucessful!" });
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            { 
                ProductViewModel viewmodel = await new ProductViewModel(_pDAO, _sDAO).GetById(id);
                if (viewmodel == null)
                {
                    return NotFound();
                }
                return Ok(viewmodel);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                                 MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // Something went wrong
            }
        }


        [HttpPost("{productId}/UploadImage")]
        public async Task<IActionResult> UploadImage(int productId, [FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image file uploaded.");
            }

            try
            {
              
                using (var stream = new MemoryStream())
                {
                    await image.CopyToAsync(stream);
                    var imageData = stream.ToArray();  

                    

              
                    var product = await _context.Product.FindAsync(productId);
                    if (product == null)
                    {
                        return NotFound("Product not found.");
                    }

                    product.ImageData = imageData; 
             


                    _context.Product.Update(product);
                    await _context.SaveChangesAsync();

                    return Ok("Image uploaded and product updated successfully.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading image: {ex.Message}");
            }
        }
        private string SaveImage(byte[] imageData)
        {
            var fileName = Guid.NewGuid().ToString() + ".jpg";  
            var filePath = Path.Combine("wwwroot", "images", fileName);

           
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        
            System.IO.File.WriteAllBytes(filePath, imageData);

            return $"/images/{fileName}"; 
        }


    }
}
