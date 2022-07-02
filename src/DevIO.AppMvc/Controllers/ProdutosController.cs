using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DevIO.AppMvc.ViewModels;
using DevIO.Bussiness.Models.Produtos;
using DevIO.Bussiness.Models.Produtos.Services;

namespace DevIO.AppMvc.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController()
        {
          
        }
        [Route("lista-de-produtos")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var produtos = await _produtoRepository.ObterTodosAsync();
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
            return View();
        }
        
        [Route("novo-produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) 
                return View(produtoViewModel);
            
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
    }
}