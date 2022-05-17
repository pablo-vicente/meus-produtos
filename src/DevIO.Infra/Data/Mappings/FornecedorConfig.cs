using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DevIO.Bussiness.Models.Fornecedores;

namespace DevIO.Infra.Data.Mappings
{
    public class FornecedorConfig : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorConfig()
        {
            HasKey(x => x.Id);

            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(200);

            Property(x => x.Documento)
                .IsRequired()
                .HasMaxLength(14)
                .HasColumnAnnotation("IX_Document",
                    new IndexAnnotation(new IndexAttribute
                    {
                        IsUnique = true
                    }));

            HasRequired(x => x.Endereco)
                .WithRequiredPrincipal(x => x.Fornecedor);

            ToTable("Fornecedores");
        }
    }
}