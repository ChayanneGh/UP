using Microsoft.AspNetCore.Mvc;
using UP.Application.Service;
using UP.Infrastructure.Entities;

namespace UP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NiveleController(NiveleSrv service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Nivele entity)
        => await service.Create(entity) ? Ok() : NotFound();

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await service.Read());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entity = await service.ReadOne(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => await service.Delete(id) ? Ok() : NotFound();

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Nivele entity)
        => await service.Update(id, entity) ? Ok() : NotFound();
}
