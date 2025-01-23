using Amazon.DynamoDBv2.DataModel;
using bancoKrt;

namespace CrudDynamoDB.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDynamoDBContext _context;

        public UsuarioRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task Adicionar(Usuario usuario)
        {
            await _context.SaveAsync(usuario);
        }

        public async Task Atualizar(Usuario usuario)
        {
            await _context.SaveAsync(usuario);
        }

        public async Task<Usuario?> Buscar(string documento, string agencia)
        {
            return await _context.LoadAsync<Usuario>(documento, agencia);
        }

        public async Task Deletar(string documento, string agencia)
        {
            await _context.DeleteAsync<Usuario>(documento, agencia);
        }

        public async Task<string?> BuscarLimite(string documento, string agencia)
        {
            var usuario = await Buscar(documento, agencia);

            if (usuario == null)
                throw new Exception("Cliente não encontrado.");

            return usuario.LimitePix;
        }

        public async Task AlterarLimite(string documento, string agencia, string novoLimite)
        {
            var usuario = await Buscar(documento, agencia);

            if (usuario == null)
                throw new Exception("Cliente não encontrado.");

            usuario.LimitePix = novoLimite;
            await Atualizar(usuario);
        }

        public async Task<bool> RealizarTransacao(string documento, string agencia, decimal valorTransacao)
        {
            var usuario = await Buscar(documento, agencia);

            if (usuario == null)
                throw new Exception("Cliente não encontrado.");

            decimal limiteAtual = decimal.Parse(usuario.LimitePix);

            if (valorTransacao > limiteAtual)
            {
                Console.WriteLine("Transação negada. Limite insuficiente.");
                return false;
            }

            usuario.LimitePix = (limiteAtual - valorTransacao).ToString("F2");
            await Atualizar(usuario);
            Console.WriteLine("Transação aprovada. Novo limite: " + usuario.LimitePix);
            return true;
        }
    }
}
