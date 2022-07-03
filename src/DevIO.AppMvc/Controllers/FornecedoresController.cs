using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DevIO.AppMvc.ViewModels;
using DevIO.Bussiness.Models.Fornecedores;
using DevIO.Bussiness.Models.Fornecedores.Services;

namespace DevIO.AppMvc.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(
            IFornecedorRepository fornecedorRepository, 
            IFornecedorService fornecedorService, 
            IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }
        
        [Route("dados-do-fornecedor/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorEnderecoAsync(id);

            if (fornecedor is null)
                return HttpNotFound();
            var produtoViewModel = _mapper.Map<Fornecedor>(fornecedor);
            
            return View(produtoViewModel);
        }

        [HttpGet]
        [Route("lista-fornecedores")]
        public async Task<ActionResult> Index()
        {
            var fornecedores = await _fornecedorRepository.ObterTodosAsync();
            var fornecedoresViewModel = _mapper.Map<FornecedorViewModel>(fornecedores);
            
            return View(fornecedoresViewModel);
        }

        [HttpGet]
        [Route("editar-fornecedor")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var fornecedorEndereco = await _fornecedorRepository.ObterFornecedorEnderecoAsync(id);

            if (fornecedorEndereco is null)
                return HttpNotFound();

            var fornecedorViewModel = _mapper.Map<FornecedorViewModel>(fornecedorEndereco);

            return View(fornecedorViewModel);
        }
        
        [HttpPost]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<ActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (fornecedorViewModel.Id != id)
                return HttpNotFound();
            
            if(!ModelState.IsValid)
                return View(fornecedorViewModel);
            
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.AtualizarAsync(fornecedor);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterPorIdAsync(id);
            if (fornecedor is null)
                return HttpNotFound();

            var fornecedorViewModel = _mapper.Map<FornecedorViewModel>(fornecedor);
            
            return View(fornecedorViewModel);
        }
        
        [HttpPost, ActionName("Delete")]
        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterPorIdAsync(id);
            if (fornecedor is null)
                return HttpNotFound();
            
            await _fornecedorService.RemoverAsync(id);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [Route("novo-fornecedor")]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        
        [HttpPost]
        [Route("novo-fornecedor")]
        public async Task<ActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid)
                return HttpNotFound();

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.AdicionarAsync(fornecedor);
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<ActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor =  await _fornecedorRepository.ObterFornecedorEnderecoAsync(id);

            if (fornecedor is null)
                return HttpNotFound();
            
            var fornecedorViewModel = _mapper.Map<FornecedorViewModel>(fornecedor) ;

            var endereco = new FornecedorViewModel
            {
                Endereco = fornecedorViewModel.Endereco
            };

            return PartialView("_AtualizarEndereco", endereco);
        }
        
    }
}