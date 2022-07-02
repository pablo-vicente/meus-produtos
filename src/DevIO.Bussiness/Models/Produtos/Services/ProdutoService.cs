using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Bussiness.Core.Services;
using DevIO.Bussiness.Models.Fornecedores.Validations;
using DevIO.Bussiness.Models.Produtos;
using DevIO.Bussiness.Models.Produtos.Services;
using DevIO.Bussiness.Models.Produtos.Validations;
using DevIO.Bussiness.Notificacoes;

namespace DevIO.Bussiness.Models.Fornecedores.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(
            IProdutoRepository produtoRepository,
            INotificador notificador) : base(notificador)
        { 
            _produtoRepository = produtoRepository;
        }

        public async Task AdicionarAsync(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto))
                return;
            await _produtoRepository.AdicionarAsync(produto);
        }

        public async Task AtualizarAsync(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto))
                return;
            await _produtoRepository.AtualizarAsync(produto);
        }

        public async Task RemoverAsync(Guid id)
        {
            await _produtoRepository.RemoverAsync(id);
        }
    }
}