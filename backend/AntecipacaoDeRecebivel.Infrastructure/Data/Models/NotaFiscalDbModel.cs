using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Infrastructure.Data.Models
{
    public class NotaFiscalDbModel
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; } //foreign key
        public string Numero { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateOnly DataDeVencimento { get; set; }
        public decimal ValorAntecipado { get; set; }
        public bool JaFoiAntecipada { get; set; }
        public bool IsDeleted { get; set; }

        //navigation
        public virtual EmpresaDbModel Empresa { get; set; } = null!;
    }
}
