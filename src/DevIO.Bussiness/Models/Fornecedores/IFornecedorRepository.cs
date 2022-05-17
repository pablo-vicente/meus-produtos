using System;
using System.Threading.Tasks;
using DevIO.Bussiness.Core.Data;

namespace DevIO.Bussiness.Models.Fornecedores
{
    public interface IFornecedorRepository :IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid id);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);
    }
}