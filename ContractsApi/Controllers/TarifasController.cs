using ContractsApi.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace ContractsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifasController(TarifaService tarifaService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tarifas = await tarifaService.GetAllTarifasAsync();
                return Ok(new
                {
                    success = true,
                    content = tarifas
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    errors = new[] { "Error al obtener las tarifas.", ex.Message }
                });
            }
        }
    }
}
