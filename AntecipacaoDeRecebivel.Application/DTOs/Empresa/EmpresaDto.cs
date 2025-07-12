using AntecipacaoDeRecebivel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Application.DTOs.Empresa
{
    public class EmpresaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public decimal FaturamentoMensal { get; set; }
        public RamoEnum Ramo { get; set; }
        public List<NotaFiscal.NotaFiscalDto> NotasFiscais { get; set; } = new();
    }
}
