using System;
using System.Threading.Tasks;
using DevIO.Bussiness.Core.Data;

namespace DevIO.Bussiness.Models.Fornecedores
{
    public interface IFornecedorRepository :IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEnderecoAsync(Guid id);
        Task<Fornecedor> ObterFornecedorProdutosEnderecoAsync(Guid id);
    }
}