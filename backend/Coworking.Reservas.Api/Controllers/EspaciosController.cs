using Coworking.Reservas.Api.DTOs;
using Coworking.Reservas.Api.Services;
using Coworking.Reservas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Reservas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EspaciosController : ControllerBase
{
    private readonly EspacioService _service;

    public EspaciosController(EspacioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var espacios = await _service.GetAll();
        return Ok(espacios);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EspacioDto dto)
    {
        var espacio = new Espacio
        {
            Nombre = dto.Nombre,
            Tipo = dto.Tipo,
            Capacidad = dto.Capacidad,
            Descripcion = dto.Descripcion
        };

        var result = await _service.Create(espacio);
        return Ok(result);
    }
}