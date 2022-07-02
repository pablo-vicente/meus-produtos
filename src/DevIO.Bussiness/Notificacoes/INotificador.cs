using System.Collections.Generic;

namespace DevIO.Bussiness.Notificacoes
{
    public interface INotificador
    {
        bool TemNotificacao();
        IEnumerable<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}