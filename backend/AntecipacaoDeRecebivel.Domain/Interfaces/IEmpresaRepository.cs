using AntecipacaoDeRecebivel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Domain.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<Empresa?> GetByIdAsync(int id);
        Task<Empresa?> GetByCnpjAsync(string cnpj);
        Task<IEnumerable<Empresa>> GetAllAsync();
        Task AddAsync(Empresa empresa);
        Task UpdateAsync(Empresa empresa);
        Task DeleteAsync(int id);
    }
}
