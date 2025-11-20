using Coworking.Reservas.Api.DTOs;
using Coworking.Reservas.Api.Services;
using Coworking.Reservas.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Coworking.Reservas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly ReservaService _service;

    public ReservasController(ReservaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("usuario/{dni}")]
    [Authorize]
    public async Task<IActionResult> GetByUsuario(string dni)
    {
        var reservas = await _service.GetByUsuarioAsync(dni);
        return Ok(reservas);
    }

    [HttpGet("espacio/{espacioid:guid}")]
    [Authorize]
    public async Task<IActionResult> GetByEspacio(Guid espacioid)
    {
        var reservas = await _service.GetByEspacioAsync(espacioid);
        return Ok(reservas);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]ReservaDto dto)
    {
        try
        {
            var reserva = new Reserva
            {
                UsuarioDNI = dto.UsuarioDNI,
                EspacioId = dto.EspacioId,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Comentarios = dto.Comentarios
            };

            var result = await _service.Create(reserva);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id:guid}/confirmar")]
    [Authorize]
    public async Task<IActionResult> Confirmar(Guid id)
    {
        try
        {
            var r = await _service.ConfirmarReservaAsync(id);
            return Ok(r);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id:guid}/finalizar")]
    [Authorize]
    public async Task<IActionResult> Finalizar(Guid id)
    {
        try
        {
            var r = await _service.FinalizarReservaAsync(id);
            return Ok(r);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}