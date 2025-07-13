using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Application.DTOs.Antecipacao
{
    public class NfAntecipacaoDto
    {
        public string Numero { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ValorLiquido { get; set; }
    }
}
