using Coworking.Reservas.Api.DTOs;
using Coworking.Reservas.Api.Services;
using Coworking.Reservas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Reservas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _service;

    public UsuariosController(UsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _service.GetAll();
        return Ok(usuarios);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UsuarioDto dto)
    {
        var usuario = new Usuario
        {
            DNI = dto.DNI,
            Nombre = dto.Nombre,
            Email = dto.Email,
            PasswordHash = dto.Password
        };

        var result = await _service.Create(usuario);
        return Ok(result);
    }
}