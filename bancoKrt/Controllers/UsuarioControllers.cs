using Microsoft.AspNetCore.Mvc;
using CrudDynamoDB.Repositories;
using bancoKrt;

namespace CrudDynamoDB.Controllers
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

        [HttpGet("{documento}/{agencia}/limite")]
        public async Task<IActionResult> BuscarLimite(string documento, string agencia)
        {
            try
            {
                var limite = await _usuarioRepository.BuscarLimite(documento, agencia);
                return Ok(new { Documento = documento, Agencia = agencia, LimitePix = limite });
            }
            catch (Exception ex)
            {
                return NotFound($"Erro: {ex.Message}");
            }
        }

        [HttpPut("{documento}/{agencia}/limite")]
        public async Task<IActionResult> AlterarLimite(string documento, string agencia, [FromBody] string novoLimite)
        {
            try
            {
                await _usuarioRepository.AlterarLimite(documento, agencia, novoLimite);
                return Ok("Limite atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpDelete("{documento}/{agencia}")]
        public async Task<IActionResult> DeletarUsuario(string documento, string agencia)
        {
            try
            {
                await _usuarioRepository.Deletar(documento, agencia);
                return Ok("Usuário removido com sucesso.");
            }
            catch (Exception ex)
            {
                return NotFound($"Erro: {ex.Message}");
            }
        }

        [HttpPost("{documento}/{agencia}/transacao")]
        public async Task<IActionResult> RealizarTransacao(string documento, string agencia, [FromBody] decimal valorTransacao)
        {
            try
            {
                var sucesso = await _usuarioRepository.RealizarTransacao(documento, agencia, valorTransacao);
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
