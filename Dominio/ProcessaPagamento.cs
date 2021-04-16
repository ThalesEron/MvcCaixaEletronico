using System;
using System.Threading.Tasks;

namespace Service
{

    public class ProcessaPagamento : IProcessaPagamento
    {
        public Task ProcessarPagamento()
        {
            // Mysql
            Console.WriteLine("PROCESSOU");
            return Task.CompletedTask;
        }
    }
}
