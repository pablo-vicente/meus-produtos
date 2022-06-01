using System.Collections.Generic;

namespace DevIO.Bussiness.Notificacoes
{
    public interface INotificacor
    {
        bool TemNotificacao();
        IEnumerable<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}