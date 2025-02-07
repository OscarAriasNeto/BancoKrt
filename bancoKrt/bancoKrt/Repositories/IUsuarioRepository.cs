using bancoKrt.Models;

namespace bancoKrt.Repositories
{
    public interface IUsuarioRepository
    {
        Task Adicionar(Usuario usuario);
        Task Atualizar(Usuario usuario);
        Task<Usuario?> Buscar(string documento);
        Task Deletar(string documento);
        Task<string?> BuscarLimite(string documento);
        Task AlterarLimite(string documento, string novoLimite);
        Task<bool> RealizarTransacao(string documento, decimal valorTransacao);
    }
}
