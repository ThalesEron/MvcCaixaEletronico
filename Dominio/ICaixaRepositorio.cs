using Service.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface ICaixaRepositorio
    {
        public Task CadastrarNota(Caixa pessoa);
        public decimal ValorTotalNotas();
        public string SugestaoSaque(decimal valor);
        public void RemoverNotas(List<Caixa> notas);
    }
}
