using Service.Entidades;
using Service.Validacao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class CaixaService : ICaixaService
    {
        public Task CadastarNota(Caixa nota)
        {
            var validator = new CaixaValidator().Validate(nota);

            if (validator.IsValid == false)
            {
                foreach (var valor in validator.Errors)
                    Console.WriteLine(valor);
                return Task.CompletedTask;
            }

            CaixaRepositorio.CadastrarNota(nota);
            return Task.CompletedTask;
        }

        public decimal ValorTotalNotas()
            => CaixaRepositorio.ValorTotalNotas();

        public string SugestaoSaque(decimal valor)
            => CaixaRepositorio.SugestaoSaque(valor);

        public void RemoverNotas(List<Caixa> notas)
         => CaixaRepositorio.RemoverNotas(notas);

        private readonly ICaixaRepositorio CaixaRepositorio;
        public CaixaService(ICaixaRepositorio caixaRepositorio)
        {
            CaixaRepositorio = caixaRepositorio;
        }
    }
}
