using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DevIO.AppMvc.ViewModels;
using DevIO.Bussiness.Models.Fornecedores;
using DevIO.Bussiness.Models.Produtos;
using DevIO.Bussiness.Models.Produtos.Services;
using DevIO.Bussiness.Notificacoes;

namespace DevIO.AppMvc.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController(
            IProdutoRepository produtoRepository, 
            IFornecedorRepository fornecedorRepository,
            IProdutoService produtoService, 
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
        }

        [Route("lista-de-produtos")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var produtos = await _produtoRepository.ObterProdutosFornecedores();
            var produtosViewModels = _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
            
            return View(produtosViewModels);
        }

        [Route("dados-do-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);

            if (produto is null)
                return HttpNotFound();
            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);
            
            return View(produtoViewModel);
        }

        [Route("novo-produto")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var produtosViewModel = await PopularForncedores(new ProdutoViewModel());
            return View(produtosViewModel);
        }

        private async Task<ProdutoViewModel> PopularForncedores(ProdutoViewModel produtoViewModel)
        {
            var fornecedores = await _fornecedorRepository.ObterTodosAsync();
            var fornecedoresViewModel = _mapper.Map<IEnumerable<FornecedorViewModel>>(fornecedores);
            produtoViewModel.Fornecedores = fornecedoresViewModel;
           
            return produtoViewModel;
        }

        [Route("novo-produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularForncedores(produtoViewModel);
            if (!ModelState.IsValid) 
                return View(produtoViewModel);

            var upload = UploadImagem(produtoViewModel.ImagemUpload);
            if(!upload.success)
                return View(produtoViewModel);

            produtoViewModel.Imagem = upload.path;
            var produto = _mapper.Map<Produto>(produtoViewModel);
            await _produtoService.AdicionarAsync(produto);

            return RedirectToAction("Index");
        }

        [Route("editar-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);

            if (produto is null)
                return HttpNotFound();
            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);

           return View(produtoViewModel);
        }
        
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
                return View(produtoViewModel);

            var produtoAtualizacao1 = await _produtoRepository.ObterProdutoFornecedor(produtoViewModel.Id);
            var produtoAtualizacao = _mapper.Map<ProdutoViewModel>(produtoAtualizacao1);
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;

            if (produtoViewModel.ImagemUpload != null)
            {
                var upload = UploadImagem(produtoViewModel.ImagemUpload);
                if (!upload.success)
                    return View(produtoViewModel);

                produtoAtualizacao.Imagem = upload.path;
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;
            produtoAtualizacao.FornecedorId = produtoViewModel.FornecedorId;
            produtoAtualizacao.Fornecedor = produtoViewModel.Fornecedor;
            
            var produto = _mapper.Map<Produto>(produtoViewModel);
            await _produtoService.AtualizarAsync(produto);

            return RedirectToAction("Index");
        }
        
        [Route("excluir-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);
            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);

            return View(produtoViewModel);
        }

        [Route("editar-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfimerd(Guid id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);

            if (produto is null)
                return HttpNotFound();
            
            await _produtoService.RemoverAsync(id);
            return RedirectToAction("Index");
        }

        private (bool success, string path) UploadImagem(HttpPostedFileBase fileBase)
        {
            if (fileBase is null || fileBase.ContentLength == 0)
            {
                ModelState.AddModelError(string.Empty, "Imagem em formato inválido!");
                return (false, string.Empty);
            }

            var imgPrefixo = Guid.NewGuid() + "_";
            var path = Path.Combine(HttpContext.Server.MapPath("~/Imagens"), imgPrefixo + fileBase.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return (false, string.Empty);
            }
            
            fileBase.SaveAs(path);
            return (true, path);
        }
    }
}