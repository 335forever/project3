using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCongViec.Models;
using QuanLyCongViec.Models.Core;
using QuanLyCongViec.Models.DTO;
using System.Security.Claims;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly QLCV_DBCONTEXT _dbContext;

    public OrderController(QLCV_DBCONTEXT dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("")]
    [Authorize(Roles = "Admin,Saler")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO model)
    {
        if (ModelState.IsValid)
        {
            var creatorId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            var order = new Order 
            { 
                Title = model.Title, 
                Description = model.Description,
                EstimatedAt = model.EstimatedAt,
                Price = model.Price,
                CreatorId = Guid.Parse(creatorId),
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            
            return Created();
        }

        return BadRequest(ModelState);
    }


}
