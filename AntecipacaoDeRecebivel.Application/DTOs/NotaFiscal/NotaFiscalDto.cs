using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Application.DTOs.NotaFiscal
{
    public record NotaFiscalDto (
        int Id,
        int EmpresaId,
        string Numero,
        decimal Valor,
        DateTime DataDeVencimento,
        decimal ValorAntecipado,
        bool JaFoiAntecipada
    );
}
