using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DevIO.AppMvc.ViewModels;
using DevIO.Bussiness.Models.Fornecedores.Services;
using DevIO.Bussiness.Models.Produtos;
using DevIO.Bussiness.Models.Produtos.Services;
using DevIO.Bussiness.Notificacoes;
using DevIO.Infra.Data.Repositories;

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
        public async Task<ActionResult> Index()
        {
            var produtos = await _produtoRepository.ObterTodosAsync();
            var produtosViewModels = _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
            
            return View(produtosViewModels);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);
            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);
            
            return View(produtoViewModel);
        }

        // public async Task<ActionResult> Create(ProdutoViewModel produtoViewModel)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var produto = _mapper.Map<Produto>(produtoViewModel);
        //         await _produtoService.AdicionarAsync(produto);
        //     }
        // }
    }
}