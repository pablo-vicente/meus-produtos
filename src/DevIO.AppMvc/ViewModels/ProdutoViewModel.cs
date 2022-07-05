using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using DevIO.AppMvc.Extensions;

namespace DevIO.AppMvc.ViewModels
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel()
        {
            Id = new Guid();
        }
        [Key]
        public Guid Id { get; }
        
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }
        
        [StringLength(200, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 2)]
        public string Nome { get; set; }
        
        [DisplayName("Descrição")]
        [StringLength(1000, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 2)]
        public string Descricao { get; set; }
        
        [DisplayName("Imagem")]
        public string Imagem { get; set; }
        
        [DisplayName("Imagem do produto")]
        public HttpPostedFileBase ImagemUpload { get; set; }
        
        [Moeda]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public decimal Valor { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }
        
        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }
        
        public FornecedorViewModel Fornecedor { get; set; }
        
        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }
    }
}