using AntecipacaoDeRecebivel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Domain.Entities
{
    public class Empresa
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public decimal FaturamentoMensal { get; private set; }
        public RamoEnum Ramo { get; private set; }

        private readonly List<NotaFiscal> _notasFiscais = new();
        public IReadOnlyList<NotaFiscal> NotasFiscais => _notasFiscais.AsReadOnly();

        public Empresa() { }

        public Empresa(string nome, string cnpj, decimal faturamentoMensal, RamoEnum ramo)
        {
            // Domain validation
            if (string.IsNullOrEmpty(nome)) throw new ArgumentException("Nome é obrigatório");
            if (cnpj.Length != 14) throw new ArgumentException("CNPJ deve ter 14 dígitos");

            Nome = nome;
            Cnpj = cnpj;
            FaturamentoMensal = faturamentoMensal;
            Ramo = ramo;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void AdicionarNotaFiscal(NotaFiscal notaFiscal)
        {
            _notasFiscais.Add(notaFiscal);
        }
    }
}
