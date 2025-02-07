using Microsoft.AspNetCore.Mvc; // Para ActionResult, OkObjectResult, BadRequestObjectResult, etc.
using Moq; // Para Mock
using bancoKrt.Controllers; // Para UsuarioController
using bancoKrt.Repositories; // Para IUsuarioRepository
using bancoKrt.Models; // Para Usuario
using System.Threading.Tasks; // Para Task

using Xunit;

namespace bancoKrt.Tests.Controllers
{
    public class UsuarioControllerTests
    {
        private readonly Mock<IUsuarioRepository> _mockRepo;
        private readonly UsuarioController _controller;

        public UsuarioControllerTests()
        {
            // Inicializa o mock do repositório e a controller
            _mockRepo = new Mock<IUsuarioRepository>();
            _controller = new UsuarioController(_mockRepo.Object);
        }

        [Fact]
        public async Task AdicionarUsuario_ShouldReturnOkResult()
        {
            // Arrange
            var usuario = new Usuario { Documento = "123" }; // Apenas documento
            _mockRepo.Setup(repo => repo.Adicionar(usuario))
                     .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AdicionarUsuario(usuario);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Usuário cadastrado com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task BuscarLimite_ShouldReturnOkResult()
        {
            // Arrange
            var documento = "123";
            var limite = "1000";
            _mockRepo.Setup(repo => repo.BuscarLimite(documento))
                     .ReturnsAsync(limite);

            // Act
            var result = await _controller.BuscarLimite(documento);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(limite, okResult.Value);
        }

        [Fact]
        public async Task AlterarLimite_ShouldReturnOkResult()
        {
            // Arrange
            var documento = "123";
            var novoLimite = "2000";
            _mockRepo.Setup(repo => repo.AlterarLimite(documento, novoLimite))
                     .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AlterarLimite(documento, novoLimite);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Limite atualizado com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task DeletarUsuario_ShouldReturnOkResult()
        {
            // Arrange
            var documento = "123";
            _mockRepo.Setup(repo => repo.Deletar(documento))
                     .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeletarUsuario(documento);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Usuário removido com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task RealizarTransacao_ShouldReturnOkResult()
        {
            // Arrange
            var documento = "123";
            var valorTransacao = 500m;
            _mockRepo.Setup(repo => repo.RealizarTransacao(documento, valorTransacao))
                     .ReturnsAsync(true);

            // Act
            var result = await _controller.RealizarTransacao(documento, valorTransacao);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Transação aprovada.", okResult.Value);
        }

        [Fact]
        public async Task RealizarTransacao_ShouldReturnBadRequest()
        {
            // Arrange
            var documento = "123";
            var valorTransacao = 1500m;
            _mockRepo.Setup(repo => repo.RealizarTransacao(documento, valorTransacao))
                     .ReturnsAsync(false);

            // Act
            var result = await _controller.RealizarTransacao(documento, valorTransacao);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Transação negada. Limite insuficiente.", badRequestResult.Value);
        }
    }
}
