using DevIO.Business.Core.Validations.Documentos;
using FluentValidation;

namespace DevIO.Bussiness.Models.Fornecedores.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O campo {propertyName} precisa ser fornecido")
                .Length(2, 200)
                .WithMessage("O campo {propertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(x => x.TipoFornecedor is TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(x => x.Documento.Length)
                    .Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracters e foi fornecido {PropertyValye}");

                RuleFor(x => CpfValidacao.Validar(x.Documento)).Equal(true)
                    .WithMessage("o documento fornecido é inválido");
            });
            
            When(x => x.TipoFornecedor is TipoFornecedor.PessoaJurifica, () =>
            {
                
                RuleFor(x => x.Documento.Length)
                    .Equal(CnpjValidacao.TamanhoCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracters e foi fornecido {PropertyValye}");

                RuleFor(x => CnpjValidacao.Validar(x.Documento)).Equal(true)
                    .WithMessage("o documento fornecido é inválido");
            });
        }
            
    }
}