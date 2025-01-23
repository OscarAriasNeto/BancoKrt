using bancoKrt;

namespace CrudDynamoDB.Repositories
{
    public interface IUsuarioRepository
    {
        Task Adicionar(Usuario usuario);
        Task Atualizar(Usuario usuario);
        Task<Usuario?> Buscar(string documento, string agencia);
        Task Deletar(string documento, string agencia);
        Task<string?> BuscarLimite(string documento, string agencia);
        Task AlterarLimite(string documento, string agencia, string novoLimite);
        Task<bool> RealizarTransacao(string documento, string agencia, decimal valorTransacao);
    }
}
