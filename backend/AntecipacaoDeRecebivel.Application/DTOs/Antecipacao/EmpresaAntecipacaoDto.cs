using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Application.DTOs.Antecipacao
{
    public class EmpresaAntecipacaoDto
    {
        public string Empresa { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public decimal Limite { get; set; }
        public decimal TotalLiquido { get; set; }
        public decimal TotalBruto { get; set; }
        public IEnumerable<NfAntecipacaoDto> NotasFiscais { get; set; } = new List<NfAntecipacaoDto>();
    }
}
