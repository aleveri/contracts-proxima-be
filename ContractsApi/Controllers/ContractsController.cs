using ContractsApi.Application.UseCases;
using ContractsApi.Domain.Entities;
using ContractsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractsApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController(ContractService service) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<Contract>>>> GetAll()
    {
        try
        {
            var contracts = await service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Contract>>.Ok(contracts));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<IEnumerable<Contract>>.Fail("Error interno del servidor.", ex.Message));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<Contract>>> Get(int id)
    {
        try
        {
            var contract = await service.GetByIdAsync(id);
            return Ok(ApiResponse<Contract>.Ok(contract));
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ApiResponse<Contract>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<Contract>.Fail("Error interno del servidor.", ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<Contract>>> Create([FromBody] Contract contract)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(contract.Dni))
                return BadRequest(ApiResponse<Contract>.Fail("DNI es requerido."));
            if (contract.TarifaId <= 0)
                return BadRequest(ApiResponse<Contract>.Fail("Tarifa inválida."));
            if (contract.FechaContratacion > DateTime.UtcNow)
                return BadRequest(ApiResponse<Contract>.Fail("Fecha de contratación no puede ser futura."));

            await service.CreateAsync(contract);
            return CreatedAtAction(nameof(Get), new { id = contract.Id }, ApiResponse<Contract>.Ok(contract));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<Contract>.Fail("Error interno al crear el contrato.", ex.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Update(int id, [FromBody] Contract contract)
    {
        try
        {
            var existing = await service.GetByIdAsync(id);
            contract.Id = id;
            await service.UpdateAsync(contract);
            return Ok(ApiResponse<bool>.Ok(true));
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ApiResponse<object>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.Fail("Error interno al actualizar el contrato.", ex.Message));
        }
    }
}