using System;
using System.Data.Entity;
using System.Threading.Tasks;
using DevIO.Bussiness.Models.Fornecedores;
using DevIO.Infra.Data.Context;

namespace DevIO.Infra.Data.Repositories
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MeuDbContext dbContext) : base(dbContext) { }
        
        public async Task<Fornecedor> ObterFornecedorEnderecoAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Endereco)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEnderecoAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Endereco)
                .Include(x => x.Produtos)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}