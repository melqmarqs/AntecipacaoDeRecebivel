using AntecipacaoDeRecebivel.Application.DTOs.NotaFiscal;
using AntecipacaoDeRecebivel.Application.Interfaces;
using AntecipacaoDeRecebivel.Domain.Entities;
using AntecipacaoDeRecebivel.Domain.Interfaces;
using AntecipacaoDeRecebivel.Domain.Services;
using System.Drawing;

namespace AntecipacaoDeRecebivel.Application.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly NotaFiscalDomainService _notaFiscalDomainService;

        public NotaFiscalService(INotaFiscalRepository repository, IEmpresaRepository empresaRepository, NotaFiscalDomainService notaFiscalDomainService)
        {
            _notaFiscalRepository = repository;
            _empresaRepository = empresaRepository;
            _notaFiscalDomainService = notaFiscalDomainService;
        }

        public async Task AddAsync(CreateNotaFiscalDto notaFiscal)
        {
            string numeroNf = _notaFiscalDomainService.GerarNumeroDeNotaFiscal();

            var nf = new NotaFiscal(
                numeroNf,
                notaFiscal.Valor,
                DateOnly.FromDateTime(notaFiscal.DataDeVencimento),
                notaFiscal.EmpresaId);

            await _notaFiscalRepository.AddAsync(nf);
        }

        public async Task DeleteAsync(int id)
        {
            await _notaFiscalRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<NotaFiscalDto>> GetByEmpresaIdAsync(int empresaId)
        {
            var nfs = await _notaFiscalRepository.GetByEmpresaIdAsync(empresaId);
            if (nfs == null)
                throw new ArgumentException("Não tem notas fiscais registradas");

            return nfs.Select(MapToNotaFiscalDto);
        }

        public async Task<NotaFiscalDto?> GetByIdAsync(int id)
        {
            var nf = await _notaFiscalRepository.GetByIdAsync(id);
            if (nf == null)
                throw new ArgumentException("Nota fiscal não encontrada");

            return MapToNotaFiscalDto(nf);
        }

        public async Task UpdateAsync(NotaFiscalDto notaFiscal)
        {
            var nf = new NotaFiscal(
                notaFiscal.Numero,
                notaFiscal.Valor,
                DateOnly.FromDateTime(notaFiscal.DataDeVencimento),
                notaFiscal.EmpresaId);

            await _notaFiscalRepository.UpdateAsync(nf);
        }

        public async Task CalcularAntecipacao(IEnumerable<NotaFiscalDto> dtos)
        {
            try
            {
                if (dtos.Any())
                {
                    int empresaId = dtos.FirstOrDefault().EmpresaId;
                    var empresa = await _empresaRepository.GetByIdAsync(empresaId);

                }
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private NotaFiscalDto MapToNotaFiscalDto(NotaFiscal nf)
        {
            return new NotaFiscalDto
            (
                nf.Id,
                nf.EmpresaId,
                nf.Numero,
                nf.Valor,
                DateTime.Parse(nf.DataDeVencimento.ToString()),
                nf.ValorAntecipado,
                nf.JaFoiAntecipada
            );
        }
    }
}
