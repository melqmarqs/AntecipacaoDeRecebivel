using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Domain.Services
{
    public class NotaFiscalDomainService
    {
        public string GerarNumeroDeNotaFiscal()
        {
            const string pool = "0123456789";
            const int tamanhoNumeroNf = 9;
            var builder = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < tamanhoNumeroNf; i++)
            {
                var character = pool[random.Next(0, pool.Length)];
                builder.Append(character);
            }

            return builder.ToString();
        }
    }
}
