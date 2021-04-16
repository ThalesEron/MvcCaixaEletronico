using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcThales.Models;
using MvcThales.Models.Caixa;
using Service;
using Service.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MvcThales.Controllers
{
    public class CaixaController : Controller
    {
        private readonly ILogger<CaixaController> _logger;
        private readonly IProcessaPagamento _processaPagamento;
        private readonly ICaixaService CaixaService;
        private readonly IMapper Mapper;

        public CaixaController(ILogger<CaixaController> logger, IProcessaPagamento processaPagamento, ICaixaService caixaService, IMapper mapper)
        {
            _logger = logger;
            _processaPagamento = processaPagamento;
            CaixaService = caixaService;
            Mapper = mapper;
        }

        public IActionResult Index()
        {
            var valor = CaixaService.ValorTotalNotas();

            ViewBag.ValorTotal = valor;

            return View();
        }

        [HttpPost]
        [Route("Inserir")]
        public IActionResult Inserir(int valorNota, int quantidade)
        {
            var caixaModel = new CaixaViewModel
            {
                Quantidade = quantidade,
                ValorNota = valorNota
            };

            var caixa = Mapper.Map<Caixa>(caixaModel);
            caixa.CaixalID = Guid.NewGuid();

            CaixaService.CadastarNota(caixa);

            return View();
        }

        [HttpPost]
        [Route("Remover")]
        public IActionResult Remover(int valorDois, int valorCinco, int valorDez, int valorVinte, int valorCinquenta, int valorCem)
        {
            var listaNotasRemove = new List<Caixa>();

            var notaCemMontar = new Caixa
            {
                Quantidade = valorCem,
                ValorNota = 100
            };

            listaNotasRemove.Add(notaCemMontar);

            var notaCinquentaMontar = new Caixa
            {
                Quantidade = valorCinquenta,
                ValorNota = 50
            };

            listaNotasRemove.Add(notaCinquentaMontar);

            var notaVinteMontar = new Caixa
            {
                Quantidade = valorVinte,
                ValorNota = 20
            };

            listaNotasRemove.Add(notaVinteMontar);

            var notaDezMontar = new Caixa
            {
                Quantidade = valorDez,
                ValorNota = 10
            };

            listaNotasRemove.Add(notaDezMontar);

            var notaCintoMontar = new Caixa
            {
                Quantidade = valorCinco,
                ValorNota = 5
            };

            listaNotasRemove.Add(notaCintoMontar);

            var notaDoisMontar = new Caixa
            {
                Quantidade = valorDois,
                ValorNota = 2
            };

            listaNotasRemove.Add(notaDoisMontar);
            CaixaService.RemoverNotas(listaNotasRemove);

            return View();
        }


        [Route("SacarDinheiro")]
        public IActionResult Saque(int valorSaque)
        {
            if (valorSaque != 0)
            {
               var txt = CaixaService.SugestaoSaque(valorSaque);

                if (txt == "Notas insuficientes para esse saque")
                {
                    Console.WriteLine(txt);
                }
                else
                {

                    string[] array = txt.Split('-');

                    ViewBag.NotaCem = Int32.Parse(array[0]);
                    ViewBag.NotaCinquenta = Int32.Parse(array[1]);
                    ViewBag.NotaVinte = Int32.Parse(array[2]);
                    ViewBag.NotaDez = Int32.Parse(array[3]);
                    ViewBag.NotaCinco = Int32.Parse(array[4]);
                    ViewBag.NotaDois = Int32.Parse(array[5]);

                    

                    ViewBag.ValorDesejado = valorSaque;
                }
                         
            }
            

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
