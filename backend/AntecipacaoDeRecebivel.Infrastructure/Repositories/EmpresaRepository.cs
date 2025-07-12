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
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly AppDbContext _context;

        public EmpresaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Empresa?> GetByIdAsync(int id)
        {
            var dbModel = await _context.Empresas
                .AsNoTracking()
                .Include(e => e.NotasFiscais)
                .FirstOrDefaultAsync(e => e.Id == id);

            return dbModel == null ? null : MapToDomain(dbModel);
        }

        public async Task AddAsync(Empresa empresa)
        {
            var dbModel = MapToDbModel(empresa);
            _context.Empresas.Add(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task<Empresa?> GetByCnpjAsync(string cnpj)
        {
            var dbModel = await _context.Empresas
                .AsNoTracking()
                .Include(e => e.NotasFiscais)
                .FirstOrDefaultAsync(e => e.Cnpj == cnpj);

            return dbModel == null ? null : MapToDomain(dbModel);
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            var dbModels = await _context.Empresas
                .AsNoTracking()
                .Include(e => e.NotasFiscais)
                .ToListAsync();

            return dbModels.Select(MapToDomain);
        }

        public async Task UpdateAsync(Empresa empresa)
        {
            var existingDbModel = await _context.Empresas
                //.Include(e => e.NotasFiscais)
                .FirstOrDefaultAsync(e => e.Id == empresa.Id 
                    && e.IsDeleted == false);

            if (existingDbModel == null)
                throw new ArgumentException("Empresa não encontrada");

            // Update properties
            //existingDbModel.Nome = empresa.Nome;
            //existingDbModel.Cnpj = empresa.Cnpj;
            existingDbModel.FaturamentoMensal = empresa.FaturamentoMensal;
            existingDbModel.Ramo = empresa.Ramo;

            // Handle NotasFiscais updates (simplified)
            //existingDbModel.NotasFiscais.Clear();
            //foreach (var notaFiscal in empresa.NotasFiscais)
            //{
            //    existingDbModel.NotasFiscais.Add(new NotaFiscalDbModel
            //    {
            //        Numero = notaFiscal.Numero,
            //        Valor = notaFiscal.Valor,
            //        DataDeVencimento = notaFiscal.DataDeVencimento,
            //        EmpresaId = empresa.Id
            //    });
            //}

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dbModel = await _context.Empresas
                .Include(e => e.NotasFiscais)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (dbModel != null)
            {
                // NotasFiscais will be deleted automatically due to cascade delete
                //_context.Empresas.Remove(dbModel);

                dbModel.IsDeleted = true;
                foreach (var nf in dbModel.NotasFiscais)
                {
                    nf.IsDeleted = true;
                }

                await _context.SaveChangesAsync();
            }
        }

        // Mapping methods
        private Empresa MapToDomain(EmpresaDbModel dbModel)
        {
            var empresa = new Empresa(dbModel.Nome, dbModel.Cnpj, dbModel.FaturamentoMensal, dbModel.Ramo);

            // Set the ID using reflection or a setter method
            // In a real scenario, you'd need a way to set the ID
            var idProperty = typeof(Empresa).GetProperty("Id");
            idProperty?.SetValue(empresa, dbModel.Id);

            // Add NotasFiscais
            foreach (var notaFiscalDb in dbModel.NotasFiscais)
            {
                var notaFiscal = new NotaFiscal(
                    notaFiscalDb.Numero,
                    notaFiscalDb.Valor,
                    notaFiscalDb.DataDeVencimento,
                    notaFiscalDb.EmpresaId
                );

                // Set NotaFiscal ID
                var notaIdProperty = typeof(NotaFiscal).GetProperty("Id");
                notaIdProperty?.SetValue(notaFiscal, notaFiscalDb.Id);

                empresa.AdicionarNotaFiscal(notaFiscal);
            }

            return empresa;
        }

        private EmpresaDbModel MapToDbModel(Empresa empresa)
        {
            var dbModel = new EmpresaDbModel
            {
                Nome = empresa.Nome,
                Cnpj = empresa.Cnpj,
                FaturamentoMensal = empresa.FaturamentoMensal,
                Ramo = empresa.Ramo
            };

            // Map NotasFiscais
            foreach (var notaFiscal in empresa.NotasFiscais)
            {
                dbModel.NotasFiscais.Add(new NotaFiscalDbModel
                {
                    Id = notaFiscal.Id,
                    Numero = notaFiscal.Numero,
                    Valor = notaFiscal.Valor,
                    DataDeVencimento = notaFiscal.DataDeVencimento,
                    EmpresaId = empresa.Id
                });
            }

            return dbModel;
        }
    }
}
