using AntecipacaoDeRecebivel.Domain.Entities;
using AntecipacaoDeRecebivel.Domain.Enums;
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

        public decimal LimiteDeFaturamento(Empresa empresa)
        {
            decimal fmt = empresa.FaturamentoMensal;
            if (fmt >= 10_000m && fmt <= 50_000)
            {
                return fmt * 0.5m;
            } else if (fmt > 50_000 && fmt <= 100_000)
            {
                return fmt * (empresa.Ramo == RamoEnum.Produtos ? 0.60m : 0.55m);
            } else if (fmt > 100_000)
            {
                return fmt * (empresa.Ramo == RamoEnum.Produtos ? 0.65m : 0.60m);
            }

            return 0.0m;
        }
    }
}
