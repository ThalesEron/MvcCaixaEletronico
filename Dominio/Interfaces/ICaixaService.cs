using Service.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface ICaixaService
    {
        public Task CadastarNota(Caixa nota);
        public decimal ValorTotalNotas();
        public string SugestaoSaque(decimal valor);
        public void RemoverNotas(List<Caixa> notas);
    }
}
