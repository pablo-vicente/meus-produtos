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
            IMapper mapper)
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

            var imgPrefixo = Guid.NewGuid() + "_";
            if(!UploadImage(produtoViewModel.ImagemUpload, imgPrefixo))
                return View(produtoViewModel);

            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
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

        private bool UploadImage(HttpPostedFileBase fileBase, string imgPrefix)
        {
            if (fileBase is null || fileBase.ContentLength == 0)
            {
                ModelState.AddModelError(string.Empty, "Imagem em formato inválido!");
                return false;
            }

            var path = Path.Combine(HttpContext.Server.MapPath("~/Imagens"), imgPrefix + fileBase.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }
            
            fileBase.SaveAs(path);
            return true;
        }
    }
}