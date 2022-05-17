using System.Data.Entity;
using DevIO.Bussiness.Models.Fornecedores;
using DevIO.Bussiness.Models.Produtos;
using DevIO.Infra.Data.Mappings;

namespace DevIO.Infra.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext() : base("DefaultConnection")
        {
            
        }
        
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FornecedorConfig());
            modelBuilder.Configurations.Add(new EnderecoConfig());
            modelBuilder.Configurations.Add(new ProdutoConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}