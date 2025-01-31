using DemoElasticSearch.Api.Models;
using DemoElasticSearch.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoElasticSearch.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ElasticSearchService _elasticSearchService;

    public ClientesController(ElasticSearchService elasticSearchService)
    {
        _elasticSearchService = elasticSearchService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCliente([FromBody] Cliente cliente)
    {
        var result = await _elasticSearchService.IndexClienteAsync(cliente);
        return result ? Ok(cliente) : StatusCode(500, "Error indexing cliente.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCliente(Guid id)
    {
        var cliente = await _elasticSearchService.GetClienteByIdAsync(id);
        return cliente != null ? Ok(cliente) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClientes()
    {
        var clientes = await _elasticSearchService.GetAllClientesAsync();
        return Ok(clientes);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCliente(Guid id, [FromBody] Cliente cliente)
    {
        if (id != cliente.Id)
            return BadRequest("ID mismatch.");

        var result = await _elasticSearchService.UpdateClienteAsync(cliente);
        return result ? Ok(cliente) : StatusCode(500, "Error updating cliente.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(Guid id)
    {
        var result = await _elasticSearchService.DeleteClienteAsync(id);
        return result ? NoContent() : StatusCode(500, "Error deleting cliente.");
    }
}