using AntecipacaoDeRecebivel.Application.DTOs.Antecipacao;
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
        private readonly EmpresaDomainService _empresaDomainService;

        public NotaFiscalService(INotaFiscalRepository repository, IEmpresaRepository empresaRepository, NotaFiscalDomainService notaFiscalDomainService, EmpresaDomainService empresaDomainService)
        {
            _notaFiscalRepository = repository;
            _empresaRepository = empresaRepository;
            _notaFiscalDomainService = notaFiscalDomainService;
            _empresaDomainService = empresaDomainService;
        }

        public async Task AddAsync(CreateNotaFiscalDto notaFiscal)
        {
            string numeroNf = notaFiscal.Numero;
            if (string.IsNullOrEmpty(notaFiscal.Numero))
                numeroNf = _notaFiscalDomainService.GerarNumeroDeNotaFiscal();

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

        public async Task<EmpresaAntecipacaoDto> CalcularAntecipacao(IEnumerable<int> nfsId)
        {
            if (nfsId.Any())
            {
                Empresa? empresa = new Empresa();
                decimal limiteDeFaturamento = 0.0m;
                var dataDeHj = DateTime.Now;
                const double taxa = 0.0465;

                var nfsAntecipadas = new List<NfAntecipacaoDto>();
                foreach (var nfId in nfsId)
                {
                    var notaFiscal = await _notaFiscalRepository.GetByIdAsync(nfId);

                    if (notaFiscal is not null)
                    {
                        if (empresa is null || empresa.Id <= 0 || limiteDeFaturamento <= 0)
                        {
                            empresa = await _empresaRepository.GetByIdAsync(notaFiscal.EmpresaId);
                            limiteDeFaturamento = _empresaDomainService.LimiteDeFaturamento(empresa);
                        }

                        var valorAFaturar = (nfsAntecipadas.Sum(n => n.ValorBruto) + notaFiscal.Valor);

                        if (limiteDeFaturamento > 0 && valorAFaturar > limiteDeFaturamento)
                            throw new ArgumentException("Valor de faturamento excedeu o limite.");

                        int diferencaEmDias = (DateTime.Parse(notaFiscal.DataDeVencimento.ToString()) - dataDeHj.Date).Days;
                        if (diferencaEmDias > 0)
                        {
                            double desagio = (double)notaFiscal.Valor / Math.Pow(1 + taxa, diferencaEmDias / 30.0);
                            double valorLiquido = (double)notaFiscal.Valor - desagio;

                            var nfAntecipada = new NfAntecipacaoDto()
                            {
                                Numero = notaFiscal.Numero,
                                ValorBruto = notaFiscal.Valor,
                                ValorLiquido = (decimal)desagio
                            };

                            nfsAntecipadas.Add(nfAntecipada);
                        } else
                        {
                            throw new ArgumentException("Nota vencida");
                        }
                    }
                }

                var resp = new EmpresaAntecipacaoDto()
                {
                    Empresa = empresa.Nome,
                    Cnpj = empresa.Cnpj,
                    Limite = limiteDeFaturamento,
                    NotasFiscais = nfsAntecipadas,
                    TotalBruto = nfsAntecipadas.Sum(n => n.ValorBruto),
                    TotalLiquido = nfsAntecipadas.Sum(n => n.ValorLiquido)
                };

                return resp;
            } else
            {
                throw new ArgumentException("Nenhuma NF foi enviada.");
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
