using GerenciamentoDeUsuario.Application.DTOs;
using GerenciamentoDeUsuario.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeUsuario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            await _service.RegisterAsync(dto);
            return Ok("Usuário criado com sucesso");
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _service.GetByIdAsync(id);
            return Ok(user);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto dto)
        {
            await _service.UpdateUserAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _service.DeactivateAsync(id);
            return NoContent();
        }

        [HttpPatch("password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            await _service.ChangePasswordAsync(dto);
            return NoContent();

        }
    }
}
