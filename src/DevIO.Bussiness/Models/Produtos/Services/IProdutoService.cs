using System;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Models.Produtos.Services
{
    public interface IProdutoService
    {
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task RemoverAsync(Guid id);

    }
}