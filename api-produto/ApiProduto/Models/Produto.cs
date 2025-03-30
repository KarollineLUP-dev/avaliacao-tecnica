using System;

namespace ApiProduto.Models
{
    public class Produto
    {
        public int Id { get; set; }

        private string _nome;
        public string Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O nome do produto não pode ser nulo ou vazio.");
                _nome = value;
            }
        }

        private int _estoque;
        public int Estoque
        {
            get => _estoque;
            set
            {
                if (value < 0)
                    throw new ArgumentException("O estoque do produto não pode ser negativo.");
                _estoque = value;
            }
        }

        private decimal _valor;
        public decimal Valor
        {
            get => _valor;
            set
            {
                if (value < 0)
                    throw new ArgumentException("O valor do produto não pode ser negativo.");
                _valor = value;
            }
        }
    }
}
