using FluentValidation;
using Service.Entidades;

namespace Service.Validacao
{
    public class CaixaValidator : AbstractValidator<Caixa>
    {
        public CaixaValidator()
        {
            RuleFor(p => p.Quantidade)
                .NotEqual(0).WithMessage("Valor de quantidade não pode ser zero!");
        }
    }
}
