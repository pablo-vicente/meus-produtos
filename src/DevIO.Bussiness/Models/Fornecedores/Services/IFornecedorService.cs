using System;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Models.Fornecedores.Services
{
    public interface IFornecedorService
    {
        Task AdicionarAsync(Fornecedor fornecedor);
        Task AtualizarAsync(Fornecedor fornecedor);
        Task RemoverAsync(Guid id);

        Task AtualizarEnderecoAsync(Endereco endereco);
    }
}