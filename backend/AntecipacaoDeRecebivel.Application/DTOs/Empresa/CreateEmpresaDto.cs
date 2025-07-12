using AntecipacaoDeRecebivel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Application.DTOs.Empresa
{
    public class CreateEmpresaDto
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public decimal FaturamentoMensal { get; set; }
        public RamoEnum Ramo { get; set; }
    }
}
