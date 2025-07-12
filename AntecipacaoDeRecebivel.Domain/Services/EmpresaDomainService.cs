using AntecipacaoDeRecebivel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Domain.Services
{
    public class EmpresaDomainService
    {
        public bool PodeEmitirNotaFiscal(Empresa empresa, decimal valor)
        {
            // Complex business rule that involves multiple entities
            return empresa.FaturamentoMensal >= valor * 0.1m;
        }
    }
}
