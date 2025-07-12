using AntecipacaoDeRecebivel.Domain.Entities;
using AntecipacaoDeRecebivel.Domain.Interfaces;
using AntecipacaoDeRecebivel.Infrastructure.Data;
using AntecipacaoDeRecebivel.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Infrastructure.Repositories
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private readonly AppDbContext _context;

        public NotaFiscalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(NotaFiscal notaFiscal)
        {
            var dbModel = MapToDbModel(notaFiscal);
            _context.NotasFiscais.Add(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dbModel = await _context.NotasFiscais.FindAsync(id);
            if (dbModel != null)
            {
                dbModel.IsDeleted = true;
                //_context.NotasFiscais.Remove(dbModel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<NotaFiscal>> GetByEmpresaIdAsync(int empresaId)
        {
            var dbModels = await _context.NotasFiscais
                .AsNoTracking()
                .Where(n => n.EmpresaId == empresaId 
                    && n.Empresa.IsDeleted == false)
                .ToListAsync();

            return dbModels.Select(MapToDomain);
        }

        public async Task<NotaFiscal?> GetByIdAsync(int id)
        {
            var dbModel = await _context.NotasFiscais
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id 
                    && n.IsDeleted == false);

            return dbModel == null ? null : MapToDomain(dbModel);
        }

        public async Task UpdateAsync(NotaFiscal notaFiscal)
        {
            var existingDbModel = await _context.NotasFiscais.FindAsync(notaFiscal.Id);
            if (existingDbModel == null)
                throw new ArgumentException("Nota Fiscal não encontrada");

            if (existingDbModel.JaFoiAntecipada)
                throw new ArgumentException("Nota Fiscal ja foi antecipada e não pode ser editada");

            existingDbModel.Valor = notaFiscal.Valor;
            existingDbModel.DataDeVencimento = notaFiscal.DataDeVencimento;

            await _context.SaveChangesAsync();
        }

        private NotaFiscal MapToDomain(NotaFiscalDbModel dbModel)
        {
            var notaFiscal = new NotaFiscal(
                dbModel.Numero,
                dbModel.Valor,
                dbModel.DataDeVencimento,
                dbModel.EmpresaId
            );

            // Set ID using reflection
            var idProperty = typeof(NotaFiscal).GetProperty("Id");
            idProperty?.SetValue(notaFiscal, dbModel.Id);

            return notaFiscal;
        }

        private NotaFiscalDbModel MapToDbModel(NotaFiscal notaFiscal)
        {
            return new NotaFiscalDbModel
            {
                Id = notaFiscal.Id,
                Numero = notaFiscal.Numero,
                Valor = notaFiscal.Valor,
                DataDeVencimento = notaFiscal.DataDeVencimento,
                EmpresaId = notaFiscal.EmpresaId
            };
        }
    }
}
