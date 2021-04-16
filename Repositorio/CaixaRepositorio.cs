using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service;
using Service.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorio
{

    public class PessoaMap : IEntityTypeConfiguration<Caixa>
    {

        public void Configure(EntityTypeBuilder<Caixa> builder)
        {
            builder.HasKey(p => p.CaixalID);
            builder.Property(p => p.Quantidade);
            builder.Property(p => p.ValorNota);

            builder.ToTable("CaixaEletronico");
        }
    }


    public class Context : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                        .UseSqlServer("Server=DESKTOP-PJMFVJI\\SQLEXPRESS;Database=cx1;Trusted_Connection=True;");
        }

        public DbSet<Caixa> Notas { get; set; }
    }

    public class CaixaRepositorio : ICaixaRepositorio
    {
        public Task CadastrarNota(Caixa caixa)
        {

            var detalhar = Context.Notas.FirstOrDefaultAsync(x => x.ValorNota == caixa.ValorNota).Result;

            if (detalhar == null)
            {
                Context.Notas.Add(caixa);
            }
            else
            {
                detalhar.Quantidade += caixa.Quantidade;
            }
            
            Context.SaveChanges();
            return Task.CompletedTask;
        }


        public decimal ValorTotalNotas()
        {

            var detalhar = Context.Notas;
            decimal valorTotal = 0;

            foreach (var nota in detalhar)
            {
                valorTotal += nota.Quantidade * nota.ValorNota;
            }

            return valorTotal;
        }

        public string SugestaoSaque(decimal valor)
        {
            var notaCem = 0;
            var notaCinquenta = 0;
            var notaVinte = 0;
            var notaDez = 0;
            var notaCinco = 0;
            var notaDois = 0;

            var detalharNotaCem = Context.Notas.FirstOrDefaultAsync(x => x.ValorNota == 100).Result?.Quantidade;
            var detalharNotaCinquenta = Context.Notas.FirstOrDefaultAsync(x => x.ValorNota == 50).Result?.Quantidade;
            var detalharNotaVinte = Context.Notas.FirstOrDefaultAsync(x => x.ValorNota == 20).Result?.Quantidade;
            var detalharNotaDez = Context.Notas.FirstOrDefaultAsync(x => x.ValorNota == 10).Result?.Quantidade;
            var detalharNotaCinco = Context.Notas.FirstOrDefaultAsync(x => x.ValorNota == 5).Result?.Quantidade;
            var detalharNotaDois = Context.Notas.FirstOrDefaultAsync(x => x.ValorNota == 2).Result?.Quantidade;

            while (valor > 0)
            {
                if (valor >= 100 && detalharNotaCem >= notaCem && valor - 100 != 1)
                {
                    valor -= 100;
                    notaCem++;
                }
                else if(valor >= 50 && detalharNotaCinquenta >= notaCinquenta && valor - 50 != 1)
                {
                    valor -= 50;
                    notaCinquenta++;
                }else if(valor >= 20 && detalharNotaVinte >= notaVinte && valor - 20 != 1)
                {
                    valor -= 20;
                    notaVinte++;
                }else if (valor >= 10 && detalharNotaDez >= notaDez && valor - 10 != 1)
                {
                    valor -= 10;
                    notaDez++;
                }else if(valor >= 5 && detalharNotaCinco >= notaCinco && valor-5 != 1)
                {
                    valor -= 5;
                    notaCinco++;
                }else if(valor >= 2 && detalharNotaDois >= notaDois && valor - 2 != 1)
                {
                    valor -= 2;
                    notaDois++;
                }
                else
                {
                    return "Notas insuficientes para esse saque";
                }
            }

            return notaCem.ToString() + "-" + notaCinquenta.ToString() + "-" + notaVinte.ToString() + "-" + notaDez.ToString() + "-" + notaCinco.ToString() + "-" + notaDois.ToString();
        }

        public void RemoverNotas(List<Caixa> notas)
        {
            
            foreach(var nota in notas)
            {
                var detalhar = Context.Notas.FirstOrDefaultAsync(x => x.ValorNota == nota.ValorNota).Result;

               if(detalhar != null)
                    detalhar.Quantidade -= nota.Quantidade;    
            }
            Context.SaveChanges();
        }

        private readonly Context Context;

        public CaixaRepositorio(Context context)
        {
            this.Context = context;
        }
    }
}
