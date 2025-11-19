using Coworking.Reservas.Domain.Entities;
using Coworking.Reservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Coworking.Reservas.Domain.Enums;

namespace Coworking.Reservas.Api.Auth;
 
public class AuthService
{
    private readonly ReservasDbContext _context;
    private readonly TokenService _tokenService;
 
    public AuthService(ReservasDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
 
    public async Task<string> Register(RegisterDto dto)
    {
        // validar usuario existente
        if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
            throw new Exception("El email ya está registrado.");
 
        var usuario = new Usuario
        {
            DNI = dto.DNI,
            Nombre = dto.Nombre,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Rol = RolUsuario.Usuario
        };
 
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
 
        return _tokenService.GenerateToken(usuario);
    }
 
    public async Task<string> Login(LoginDto dto)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == dto.Email);
 
        if (usuario == null)
            throw new Exception("Email o contraseña incorrectos.");
 
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            throw new Exception("Email o contraseña incorrectos.");
 
        return _tokenService.GenerateToken(usuario);
    }
}