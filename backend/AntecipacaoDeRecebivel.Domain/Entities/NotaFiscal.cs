using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Domain.Entities
{
    public class NotaFiscal
    {
        public int Id { get; private set; }
        public string Numero { get; private set; }
        public decimal Valor { get; private set; }
        public DateOnly DataDeVencimento { get; private set; }
        public decimal ValorAntecipado { get; set; }
        public bool JaFoiAntecipada { get; set; }
        public int EmpresaId { get; private set; }

        public NotaFiscal(string numero, decimal valor, DateOnly dataDeVencimento, int empresaId)
        {
            // Domain validation
            if (numero.Length != 9) throw new ArgumentException("Número da NF é inválido");
            if (valor <= 0) throw new ArgumentException("Valor deve ser maior que zero");

            Numero = numero;
            Valor = valor;
            DataDeVencimento = dataDeVencimento;
            EmpresaId = empresaId;
        }

        public bool EstaVencida()
        {
            var result = DateTime.Compare(DateTime.Parse(DataDeVencimento.ToString()), DateTime.Now);
            return result >= 0;
        }
    }
}
