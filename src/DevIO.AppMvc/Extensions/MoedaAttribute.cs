using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DevIO.AppMvc.Extensions
{
    public class MoedaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var moeda = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Moeda em formato inválido");
            }
        }
    }
}