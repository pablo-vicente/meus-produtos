using System.Data.Entity.ModelConfiguration;
using DevIO.Bussiness.Models.Produtos;

namespace DevIO.Infra.Data.Mappings
{
    public class ProdutoConfig : EntityTypeConfiguration<Produto>
    {
        public ProdutoConfig()
        {
            HasKey(x => x.Id);

            Property(x => x.Nome)
                .HasMaxLength(200);
            
            Property(x => x.Descricao)
                .HasMaxLength(1000);
            
            Property(x => x.Imagem)
                .HasMaxLength(100);

            HasRequired(x => x.Fornecedor)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.FornecedorId);
            
            ToTable("Produtos");
        }
    }
}