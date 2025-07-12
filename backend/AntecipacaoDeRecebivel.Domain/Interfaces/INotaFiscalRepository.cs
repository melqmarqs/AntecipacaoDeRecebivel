using AntecipacaoDeRecebivel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Domain.Interfaces
{
    public interface INotaFiscalRepository
    {
        Task<NotaFiscal?> GetByIdAsync(int id);
        Task<IEnumerable<NotaFiscal>> GetByEmpresaIdAsync(int empresaId);
        Task AddAsync(NotaFiscal notaFiscal);
        Task UpdateAsync(NotaFiscal notaFiscal);
        Task DeleteAsync(int id);
    }
}
