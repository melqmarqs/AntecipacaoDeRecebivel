using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoDeRecebivel.Domain.ValueObjects
{
    public class Cnpj
    {
        public string Value { get; private set; }

        public Cnpj(string value)
        {
            if (!IsValid(value)) throw new ArgumentException("CNPJ inválido");
            Value = value;
        }

        private bool IsValid(string cnpj) => cnpj.Length == 14; // Simplified validation
    }

}
