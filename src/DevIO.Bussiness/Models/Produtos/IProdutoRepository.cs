using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Bussiness.Core.Data;

namespace DevIO.Bussiness.Models.Produtos
{
    public interface IProdutoRepository :IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
        Task<IEnumerable<Produto>> ObterProdutosFornecedores();
        Task<Produto> ObterProdutoFornecedor(Guid id);
    }
}