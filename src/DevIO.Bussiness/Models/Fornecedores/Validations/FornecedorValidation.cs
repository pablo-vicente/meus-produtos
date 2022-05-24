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
        }
            
    }
}