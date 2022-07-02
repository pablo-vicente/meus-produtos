using System;
using System.Threading.Tasks;
using DevIO.Bussiness.Models.Fornecedores;
using DevIO.Infra.Data.Context;

namespace DevIO.Infra.Data.Repositories
{
    public class EnderecoRespository : Repository<Endereco>, IEnderecoRespository
    {
        public EnderecoRespository(MeuDbContext dbContext) : base(dbContext) { }
        
        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await ObterPorIdAsync(fornecedorId);
        }
    }
}