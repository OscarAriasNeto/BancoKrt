using Amazon.DynamoDBv2.DataModel;
using bancoKrt.Models;
using System;
using System.Threading.Tasks;

namespace bancoKrt.Repositories
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

        public async Task<Usuario?> Buscar(string documento)
        {
            return await _context.LoadAsync<Usuario>(documento);
        }

        public async Task Deletar(string documento)
        {
            await _context.DeleteAsync<Usuario>(documento);
        }

        public async Task<string?> BuscarLimite(string documento)
        {
            var usuario = await Buscar(documento);

            if (usuario == null)
                throw new Exception("Cliente não encontrado.");

            return usuario.LimitePix;
        }

        public async Task AlterarLimite(string documento, string novoLimite)
        {
            var usuario = await Buscar(documento);

            if (usuario == null)
                throw new Exception("Cliente não encontrado.");

            usuario.LimitePix = novoLimite;
            await Atualizar(usuario);
        }

        public async Task<bool> RealizarTransacao(string documento, decimal valorTransacao)
        {
            var usuario = await Buscar(documento);

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