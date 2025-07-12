using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Application.DTOs.NotaFiscal
{
    public record CreateNotaFiscalDto (
        int EmpresaId,
        string Numero,
        decimal Valor,
        DateTime DataDeVencimento
    );
}
