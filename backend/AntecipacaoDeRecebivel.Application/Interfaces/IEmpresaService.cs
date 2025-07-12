using AntecipacaoDeRecebivel.Application.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Application.Interfaces
{
    public interface IEmpresaService
    {
        Task<EmpresaDto> CreateEmpresaAsync(CreateEmpresaDto createDto);
        Task<EmpresaDto> GetEmpresaAsync(int id);
        Task<IEnumerable<EmpresaDto>> GetAllEmpresasAsync();
        Task UpdateEmpresaAsync(EmpresaDto empresaDto);
        Task DeleteEmpresaAsync(int id);
    }
}
