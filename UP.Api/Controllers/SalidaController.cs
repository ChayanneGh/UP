﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UP.Application.Service;
using UP.Infrastructure.Entities;

namespace UP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalidaController(EntradaSrv entradaSrv) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Entrada entrada)
    {
        if (!await entradaSrv.Create(entrada)) return NotFound();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var Lista = await entradaSrv.Read();

        if (Lista is null) return NotFound();

        return Ok(Lista);
    }

    [HttpGet("{EntradaId}")]
    public async Task<IActionResult> GetById(int EntradaId)
    {
        var entrada = await entradaSrv.ReadOne(EntradaId);

        if (entrada is null) return NotFound();

        return Ok(entrada);
    }

    [HttpDelete("{EntradaId}")]
    public async Task<IActionResult> Delete(int EntradaId)
    {
        if (!await entradaSrv.Delete(EntradaId)) return NotFound();

        return Ok();
    }

    [HttpPut("{EntradaId}")]
    public async Task<IActionResult> Put(int EntradaId, [FromBody] Entrada entrada)
    {
        if (!await entradaSrv.Update(EntradaId, entrada)) return NotFound();

        return Ok();
    }
}