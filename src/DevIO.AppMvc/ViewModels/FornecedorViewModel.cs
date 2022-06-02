using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using DevIO.Bussiness.Models.Fornecedores;

namespace DevIO.AppMvc.ViewModels
{
    public class FornecedorViewModel
    {
        public FornecedorViewModel()
        {
            Id = new Guid();
        }
        [Key]
        public Guid Id { get; }
        
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 2)]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 11)]
        public string Descricao { get; set; }
        
        [DisplayName("Tipo")]
        public TipoFornecedor TipoFornecedor { get; set; }
        
        public EnderecoViewModel Endereco { get; set; }
        
        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }
        
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }
}