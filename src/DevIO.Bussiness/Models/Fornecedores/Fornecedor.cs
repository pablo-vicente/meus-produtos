using System.Collections.Generic;
using DevIO.Bussiness.Core.Models;
using DevIO.Bussiness.Models.Produtos;

namespace DevIO.Bussiness.Models.Fornecedores
{
    public class Fornecedor : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }
        
        public ICollection<Produto> Produtos { get; set; }
    }
}