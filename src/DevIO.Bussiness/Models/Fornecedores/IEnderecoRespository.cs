using System;
using System.Threading.Tasks;
using DevIO.Bussiness.Core.Data;

namespace DevIO.Bussiness.Models.Fornecedores
{
    public interface IEnderecoRespository:IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}