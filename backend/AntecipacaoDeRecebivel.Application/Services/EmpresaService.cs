using AntecipacaoDeRecebivel.Application.DTOs.Empresa;
using AntecipacaoDeRecebivel.Application.Interfaces;
using AntecipacaoDeRecebivel.Domain.Entities;
using AntecipacaoDeRecebivel.Domain.Interfaces;
using AntecipacaoDeRecebivel.Domain.Services;

namespace AntecipacaoDeRecebivel.Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly EmpresaDomainService _domainService;

        public EmpresaService(IEmpresaRepository empresaRepository, EmpresaDomainService domainService)
        {
            _empresaRepository = empresaRepository;
            _domainService = domainService;
        }

        public async Task<EmpresaDto> CreateEmpresaAsync(CreateEmpresaDto createDto)
        {
            // Use case orchestration
            var empresa = new Empresa(
                createDto.Nome,
                createDto.Cnpj,
                createDto.FaturamentoMensal,
                createDto.Ramo);

            await _empresaRepository.AddAsync(empresa);

            return new EmpresaDto
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Cnpj = empresa.Cnpj,
                FaturamentoMensal = empresa.FaturamentoMensal,
                Ramo = empresa.Ramo
            };
        }

        public async Task<IEnumerable<EmpresaDto>> GetAllEmpresasAsync()
        {
            var empresas = await _empresaRepository.GetAllAsync();
            return empresas.Select(MapToEmpresaDto);
        }

        public async Task<EmpresaDto> GetEmpresaAsync(int id)
        {
            var empresa = await _empresaRepository.GetByIdAsync(id);
            if (empresa == null) throw new ArgumentException("Empresa não encontrada");

            return MapToEmpresaDto(empresa);
        }

        public async Task UpdateEmpresaAsync(EmpresaDto dto)
        {
            var empresa = new Empresa(
                dto.Nome,
                dto.Cnpj,
                dto.FaturamentoMensal,
                dto.Ramo
            );
            empresa.SetId(dto.Id);

            await _empresaRepository.UpdateAsync(empresa);
        }

        public async Task DeleteEmpresaAsync(int id)
        {
            await _empresaRepository.DeleteAsync(id);
        }

        private EmpresaDto MapToEmpresaDto(Empresa empresa)
        {
            return new EmpresaDto
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Cnpj = empresa.Cnpj,
                FaturamentoMensal = empresa.FaturamentoMensal,
                Ramo = empresa.Ramo
            };
        }
    }
}
