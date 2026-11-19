using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UP.Application.Services;
using UP.Infrastructure.Entities;

namespace UP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalidaController(SalidaSrv salidaSrv) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Salida salida)
    {
        if (!await salidaSrv.Create(salida)) return NotFound();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var Lista = await salidaSrv.Read();

        if (Lista is null) return NotFound();

        return Ok(Lista);
    }

    [HttpGet("{SalidaId}")]
    public async Task<IActionResult> GetById(int SalidaId)
    {
        var salida = await salidaSrv.ReadOne(SalidaId);

        if (salida is null) return NotFound();

        return Ok(salida);
    }

    [HttpDelete("{SalidaId}")]
    public async Task<IActionResult> Delete(int SalidaId)
    {
        if (!await salidaSrv.Delete(SalidaId)) return NotFound();

        return Ok();
    }

    [HttpPut("{SalidaId}")]
    public async Task<IActionResult> Put(int SalidaId, [FromBody] Salida salida)
    {
        if (!await salidaSrv.Update(SalidaId, salida)) return NotFound();

        return Ok();
    }
}