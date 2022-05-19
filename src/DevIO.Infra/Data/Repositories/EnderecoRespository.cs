using System;
using System.Threading.Tasks;
using DevIO.Bussiness.Models.Fornecedores;

namespace DevIO.Infra.Data.Repositories
{
    public class EnderecoRespository : Repository<Endereco>, IEnderecoRespository
    {
        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await ObterPorId(fornecedorId);
        }
    }
}