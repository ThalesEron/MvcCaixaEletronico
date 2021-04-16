using System;
using System.Collections.Generic;

namespace MvcThales.Models.Caixa
{
    public class CaixaViewModel
    {
        public int Quantidade { get; set; }
        public int ValorNota { get; set; }
    }

    public class FakeViewModel
    {
        IEnumerable<decimal> Valores { get; set; }
    }

}
