using System;
using DevIO.Bussiness.Core.Models;
using DevIO.Bussiness.Models.Fornecedores;

namespace DevIO.Bussiness.Models.Produtos
{
    public class Produto : Entity
    {

        public Produto()
        {
            DataCadastro = DateTime.Now;
            Ativo = true;
        }
        public Guid FornecedorId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        
        public Fornecedor Fornecedor { get; set; }
    }
}