using Microsoft.AspNetCore.Mvc;
using bancoKrt.Repositories;
using bancoKrt.Models; // Necessário para o modelo Usuario

namespace bancoKrt.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                await _usuarioRepository.Adicionar(usuario);
                return Ok("Usuário cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{documento}/limite")]
        public async Task<IActionResult> BuscarLimite(string documento)
        {
            try
            {
                var limite = await _usuarioRepository.BuscarLimite(documento);
                return Ok(new { Documento = documento, LimitePix = limite });
            }
            catch (Exception ex)
            {
                return NotFound($"Erro: {ex.Message}");
            }
        }

        [HttpPut("{documento}/limite")]
        public async Task<IActionResult> AlterarLimite(string documento, [FromBody] string novoLimite)
        {
            try
            {
                await _usuarioRepository.AlterarLimite(documento, novoLimite);
                return Ok("Limite atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpDelete("{documento}")]
        public async Task<IActionResult> DeletarUsuario(string documento)
        {
            try
            {
                await _usuarioRepository.Deletar(documento);
                return Ok("Usuário removido com sucesso.");
            }
            catch (Exception ex)
            {
                return NotFound($"Erro: {ex.Message}");
            }
        }

        [HttpPost("{documento}/transacao")]
        public async Task<IActionResult> RealizarTransacao(string documento, [FromBody] decimal valorTransacao)
        {
            try
            {
                var sucesso = await _usuarioRepository.RealizarTransacao(documento, valorTransacao);
                if (sucesso)
                    return Ok("Transação aprovada.");
                else
                    return BadRequest("Transação negada. Limite insuficiente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
    }
}
