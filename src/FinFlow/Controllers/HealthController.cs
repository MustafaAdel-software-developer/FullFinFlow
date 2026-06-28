using FinFlow.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly FinFlowDbContext _dbContext;

        public HealthController(FinFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var canConnect = await _dbContext.Database.CanConnectAsync();

            if (!canConnect)
            {
                return StatusCode(503, new
                {
                    status = "unhealthy",
                    database = "disconnected"
                });
            }

            return Ok(new
            {
                status = "healthy",
                database = "connected"
            });
        }
    }
}
