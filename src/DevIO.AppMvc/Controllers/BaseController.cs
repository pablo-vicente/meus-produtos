using System;
using System.Web.Mvc;
using DevIO.Bussiness.Notificacoes;

namespace DevIO.AppMvc.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotificador _notificador;

        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            if (!_notificador.TemNotificacao())
                return true;

            var notificacoes = _notificador.ObterNotificacoes();
            foreach (var notificacoe in notificacoes)
                ViewData.ModelState.AddModelError(string.Empty,  notificacoe.Mensagem);

            return false;
        }
    }
}