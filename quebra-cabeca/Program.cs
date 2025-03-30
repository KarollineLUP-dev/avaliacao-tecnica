using System;
using System.Collections.Generic;
using System.Linq;
public class Program
{
    public static List<int> EncontrarNumerosEscondidos(int A, int B, int C, int D, List<int> E)
    {
        List<int> numerosEncontrados = new List<int>();

        for (int i = A; i < B; i++)
        {
            if (i % C != 0) continue;

            int somaNumeros = i.ToString().Sum(x => x - '0');

            if (somaNumeros < D) continue;

            if (i.ToString().Any(x => E.Contains(x - '0'))) continue;

            numerosEncontrados.Add(i);
        }
        return numerosEncontrados;
    }

    public static void Main()
    {
        Console.Clear();
        Console.WriteLine("Escolha uma opção:");
        Console.WriteLine("1 - Inserir valores manualmente");
        Console.WriteLine("2 - Rodar teste pronto");

        int opcao;
        while (!int.TryParse(Console.ReadLine(), out opcao) || (opcao != 1 && opcao != 2))
        {
            Console.WriteLine("Opção inválida. Escolha 1 ou 2:");
        }

        int A = 0, B = 0, C = 0, D = 0;
        List<int> E = new List<int>();

        switch (opcao)
        {
            case 1:
                A = LerInteiro("Digite o valor inicial do intervalo (maior que 1 e menor que 10000):", 2, 9999);
                B = LerInteiro($"Digite o valor final do intervalo (maior que {A} e menor que 10000):", A + 1, 10000);
                C = LerInteiro("Digite o valor do divisor (maior que 2 e menor que 100):", 2, 99);
                D = LerInteiro("Digite o valor mínimo da soma dos dígitos: (maior que 2 e menor que 50)", 2, 50);

                Console.WriteLine("Digite os valores proibidos separando-os por virgula (maior que 0 e menor que 10):");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    var valoresInvalidos = input.Split(',')
                                                .Where(x => !int.TryParse(x, out _))
                                                .ToList();

                    if (valoresInvalidos.Any())
                    {
                        Console.WriteLine($"Aviso: Os seguintes valores foram ignorados por não serem numéricos: {string.Join(", ", valoresInvalidos)}");
                    }

                    E = input.Split(',')
                             .Where(x => int.TryParse(x, out _))
                             .Select(int.Parse)
                             .Distinct()
                             .ToList();
                }
                break;
            case 2:
                A = 10; B = 50; C = 5; D = 5; E = new List<int> { 3, 7 };
                Console.WriteLine("Executando teste pronto...");
                break;
        }

        Console.WriteLine($"Valores utilizados: A={A}, B={B}, C={C}, D={D}, E=[{string.Join(", ", E)}]");
        List<int> resultado = EncontrarNumerosEscondidos(A, B, C, D, E);
        Console.WriteLine("Resultado: [" + string.Join(", ", resultado) + "]");
    }

    private static int LerInteiro(string mensagem, int min, int max)
    {
        int valor;
        do
        {
            Console.WriteLine(mensagem);
        } while (!int.TryParse(Console.ReadLine(), out valor) || valor < min || valor > max);

        return valor;
    }
}