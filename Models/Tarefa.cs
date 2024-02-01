using System;
using System.Security;

namespace MeuTodo.Models
{
    public class Tarefa
    {
        public Guid Id { get; set; }

       // public Guid UsuarioId { get; set; }

        public int NumeroBoleto { get; set; }

        public int ValorBoleto { get; set; }

        public decimal ValorJuros { get; set; }

        public string StatusBoleto { get; set; }

        public decimal ValorTotalPago { get; set; }

        public DateTime DataVencimentoBoleto { get; set; }

        public DateTime DataPagamentoBoleto { get; set; }
    }
}