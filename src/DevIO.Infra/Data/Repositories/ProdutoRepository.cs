
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Bussiness.Models.Produtos;

namespace DevIO.Infra.Data.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x=>x.Fornecedor)
                .OrderBy(x=>x.Nome)
                .ToListAsync();
        }

        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await _dbSet
                .Include(x=>x.Fornecedor)
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id == id);
        }
        
        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(x => x.FornecedorId == fornecedorId)
                .ToListAsync();
        }
    }
}