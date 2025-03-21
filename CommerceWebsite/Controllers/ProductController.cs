using CommerceDAL;
using CommerceDAL.DAO;
using CommerceViewModels;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using CommerceDAL.Entities;
using Microsoft.AspNetCore.Identity;
using CommerceWebsite.Models;

namespace CommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDAO _pDAO;
        private readonly StocksDAO _sDAO;
        private readonly UserDAO _uDAO;
        private readonly OrderDAO _oDAO;
        private readonly AuthService _authService;
        private readonly CommerceContext _context;
        

        public ProductController(ProductDAO productDAO, StocksDAO stocksDAO, UserDAO userDAO,
            AuthService authService ,CommerceContext context, OrderDAO orderDAO)
        {
            _pDAO = productDAO;
            _sDAO = stocksDAO;
            _uDAO = userDAO;
            _oDAO = orderDAO;
            _authService = authService;
            _context = context;
        }

        [HttpGet("order/{Id}")]
        public async Task<IActionResult> GetAllOrders(int Id)
        {
            var orders = await _oDAO.GetById(Id);

            if (orders == null)
            {
                return NotFound(new { Message = "No orders found for this user." });
            }

            var orderViewModels = orders.Select(o => new OrderViewModel
            {
                OrderId = o.Id,
                CustomerId = o.CustomerId,
                CustomerName = o.CustomerName,
                OrderStatus = o.OrderStatus,
                TotalPrice = o.TotalPrice,
                PaymentStatus = o.PaymentStatus,
                Date = o.Date,
                OrderCompletion = o.OrderCompletion
            }).ToList();

            return Ok(orderViewModels);
        }
        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest orderRequest)
        {
            if (orderRequest == null || orderRequest.Products.Count == 0)
            {
                return BadRequest(new { Message = "Invalid order request." });
            }

            var user = await _uDAO.GetById(orderRequest.CustomerId);
            if (user == null)
            {
                return BadRequest(new { Message = "User not found." });
            }

            foreach (var product in orderRequest.Products)
            {
                var productInDb = await _pDAO.GetById(product.ProductId);
                if (productInDb == null)
                {
                    return BadRequest(new { Message = $"Product with ID {product.ProductId} not found." });
                }

                var stock = await _sDAO.GetByProductId(product.ProductId);
                if (stock == null || stock.Quanity < product.Quantity)
                {
                    return BadRequest(new { Message = $"Not enough stock for product {product.Name}" });
                }

                stock.Quanity -= product.Quantity;
                await _sDAO.Update(stock);

                var order = new Orders
                {
                    CustomerId = orderRequest.CustomerId,
                    CustomerName = $"{user.FirstName} {user.LastName}",
                    ProductId = product.ProductId, 
                    StockId = stock.Id, 
                    OrderStatus = "Pending",
                    PaymentStatus = "Paid",
                    Date = DateTime.UtcNow,
                    TotalPrice = product.Price * product.Quantity,
                    ProductDescription = $"{productInDb.Name} - {product.Quantity} pcs",
                    StockQuantity = product.Quantity,
                    OrderCompletion = null
                };

                await _oDAO.Add(order);
            }

            return Ok(new { Message = "Order placed successfully!" });
        }






        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (string.IsNullOrWhiteSpace(registerModel.Email) || string.IsNullOrWhiteSpace(registerModel.Password))
            {
                return BadRequest(new { Message = "Email and password are required." });
            }

            var existingUser = await _uDAO.GetByEmail(registerModel.Email);
            if (existingUser != null)
            {
                return BadRequest(new { Message = "User with this email already exists." });
            }


            var passwordHasher = new PasswordHasher<Users>();
            var hashedPassword = passwordHasher.HashPassword(null, registerModel.Password);

            
            var customUser = new Users
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                PasswordHash = hashedPassword 
            };

     
            await _uDAO.Add(customUser);

           
            var token = _authService.GenerateJwtToken(customUser);

            return Ok(new { Token = token, Message = "User registered successfully!" });
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
