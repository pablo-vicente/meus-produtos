using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Bussiness.Core.Services;
using DevIO.Bussiness.Models.Fornecedores.Validations;
using DevIO.Bussiness.Notificacoes;

namespace DevIO.Bussiness.Models.Fornecedores.Services
{
    public class FornecedorService : BaseService,IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRespository _enderecoRespository;

        public FornecedorService(
            IFornecedorRepository fornecedorRepository, 
            IEnderecoRespository enderecoRespository,
            INotificacor notificacor) : base(notificacor)
        { 
            _fornecedorRepository = fornecedorRepository;
            _enderecoRespository = enderecoRespository;
        }

        public async Task AdicionarAsync(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor) ||
                !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco))
                return;

            if(await FornecedorExistenteAsync(fornecedor))
                return;
            
            await _fornecedorRepository.AdicionarAsync(fornecedor);
            await _enderecoRespository.AdicionarAsync(fornecedor.Endereco);
        }

        private async Task<bool> FornecedorExistenteAsync(Fornecedor fornecedor)
        {
            var fornecedorAtual = await _fornecedorRepository.BuscarAsync(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id);

            if (!fornecedorAtual.Any())
                return false;
            
            Notificar("Já existe um fornecedor com este documento cadastrado.");
            
            return true;
        }

        public async Task AtualizarAsync(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor))
                return;

            if(await FornecedorExistenteAsync(fornecedor))
                return;
            await _fornecedorRepository.AtualizarAsync(fornecedor);
        }

        public async Task RemoverAsync(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorProdutosEnderecoAsync(id);

            if (fornecedor.Produtos.Any())
            {
                Notificar("Fornecedor possui produtos cadastrados.");
                return;
            }

            if (fornecedor.Endereco is null)
            {
                await _enderecoRespository.RemoverAsync(fornecedor.Id);
            }

            await _fornecedorRepository.RemoverAsync(fornecedor.Id);
        }

        public async Task AtualizarEnderecoAsync(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco))
                return;

            await _enderecoRespository.AtualizarAsync(endereco);
        }
    }
}