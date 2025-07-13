using AntecipacaoDeRecebivel.Application.DTOs.Antecipacao;
using AntecipacaoDeRecebivel.Application.DTOs.NotaFiscal;
using AntecipacaoDeRecebivel.Domain.Entities;

namespace AntecipacaoDeRecebivel.Application.Interfaces
{
    public interface INotaFiscalService
    {
        Task<NotaFiscalDto?> GetByIdAsync(int id);
        Task<IEnumerable<NotaFiscalDto>> GetByEmpresaIdAsync(int empresaId);
        Task AddAsync(CreateNotaFiscalDto notaFiscal);
        Task UpdateAsync(NotaFiscalDto notaFiscal);
        Task DeleteAsync(int id);
        Task<EmpresaAntecipacaoDto> CalcularAntecipacao(IEnumerable<int> nfsId);
    }
}
