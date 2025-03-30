namespace ApiProduto.Dtos
{
    public class ProdutoDto
    {
        public string Nome { get; set; } = string.Empty;
        public int Estoque { get; set; }
        public decimal Valor { get; set; }
    }
}
