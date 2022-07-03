using System;
using System.Web.Mvc;
using DevIO.Bussiness.Models.Fornecedores;

namespace DevIO.AppMvc.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatarDocumento(this WebViewPage page, TipoFornecedor tipoFornecedor, string documento)
        {
            switch (tipoFornecedor)
            {
                case TipoFornecedor.PessoaFisica:
                    return Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00");
                
                case TipoFornecedor.PessoaJurifica:
                    return Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");

                default:
                    throw new ArgumentOutOfRangeException(nameof(tipoFornecedor), tipoFornecedor, null);
            }
        }
    }
}