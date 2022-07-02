using DevIO.Bussiness.Core.Models;
using DevIO.Bussiness.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace DevIO.Bussiness.Core.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var validationResultError in validationResult.Errors)
                Notificar(validationResultError.ErrorMessage);
        }
        
        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade)
            where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid)
                return true;

            Notificar(validator);
            return false;
        }
    }
}