using System.Data.Entity.ModelConfiguration;
using DevIO.Bussiness.Models.Fornecedores;

namespace DevIO.Infra.Data.Mappings
{
    public class EnderecoConfig : EntityTypeConfiguration<Endereco>
    {
        public EnderecoConfig()
        {
            HasKey(x => x.Id);

            Property(x => x.Logradouro)
                .HasMaxLength(200);
            
            Property(x => x.Numero)
                .HasMaxLength(50);
            
            Property(x => x.Cep)
                .HasMaxLength(8)
                .IsFixedLength();
            
            Property(x => x.Complemento)
                .HasMaxLength(250);
            
            Property(x => x.Bairro)
                .IsRequired()
                .HasMaxLength(100);
            
            Property(x => x.Cidade)
                .IsRequired()
                .HasMaxLength(100);
            
            Property(x => x.Estado)
                .IsRequired()
                .HasMaxLength(100);

            ToTable("Enderecos");
        }
    }
}