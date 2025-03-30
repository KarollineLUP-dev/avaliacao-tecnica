using System;
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
        int opcao = int.Parse(Console.ReadLine());

        int A = 0, B = 0, C = 0, D = 0;
        List<int> E = new List<int>();

        switch (opcao)
        {
            case 1:
                Console.WriteLine("Digite o valor de A:");
                A = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o valor de B:");
                B = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o valor de C:");
                C = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o valor de D:");
                D = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite os valores proibidos separando-os por virgula:");
                E = Console.ReadLine().Split(',').Select(int.Parse).ToList();
                break;
            case 2:
                A = 10; B = 50; C = 5; D = 5; E = new List<int> { 3, 7 };
                Console.WriteLine("Executando teste pronto...");
                break;
            default:
                Console.WriteLine("\nOpção Inválida!");
                break;
        }

        Console.WriteLine($"Valores utilizados: A={A}, B={B}, C={C}, D={D}, E=[{string.Join(", ", E)}]");
        List<int> resultado = EncontrarNumerosEscondidos(A, B, C, D, E);
        Console.WriteLine("Resultado: [" + string.Join(", ", resultado) + "]");

    }
}