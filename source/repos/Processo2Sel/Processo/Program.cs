using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class Faturamento
{
    public int Dia { get; set; }
    public double Valor { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Caminho absoluto do arquivo JSON
        string caminhoJson = @"C:\Users\MASTER\source\repos\Json";

        // Verificar se o arquivo existe
        if (!File.Exists(caminhoJson))
        {
            Console.WriteLine($"O arquivo JSON não foi encontrado no caminho: {caminhoJson}");
            return;
        }

        // Carregar e processar os dados do JSON
        var dadosJson = CarregarJson(caminhoJson);
        ProcessarFaturamento(dadosJson);
    }

    // Função para carregar o JSON
    static List<Faturamento> CarregarJson(string caminhoJson)
    {
        string json = File.ReadAllText(caminhoJson); // Ler o conteúdo do arquivo JSON
        return JsonConvert.DeserializeObject<List<Faturamento>>(json); // Desserializar os dados JSON
    }

    // Função para processar os dados do faturamento
    static void ProcessarFaturamento(List<Faturamento> faturamento)
    {
        // Filtrando os dias com faturamento
        var diasComFaturamento = faturamento.FindAll(f => f.Valor > 0);

        // Calcular a média de faturamento
        double mediaFaturamento = 0;
        foreach (var f in diasComFaturamento)
        {
            mediaFaturamento += f.Valor;
        }
        mediaFaturamento /= diasComFaturamento.Count;

        // Calcular o menor e maior valor de faturamento
        double menorFaturamento = double.MaxValue;
        double maiorFaturamento = double.MinValue;

        foreach (var f in diasComFaturamento)
        {
            if (f.Valor < menorFaturamento)
                menorFaturamento = f.Valor;
            if (f.Valor > maiorFaturamento)
                maiorFaturamento = f.Valor;
        }

        // Contar os dias com faturamento superior à média
        int diasAcimaMedia = 0;
        foreach (var f in diasComFaturamento)
        {
            if (f.Valor > mediaFaturamento)
                diasAcimaMedia++;
        }

        // Exibir os resultados
        Console.WriteLine($"Menor valor de faturamento: R${menorFaturamento:F2}");
        Console.WriteLine($"Maior valor de faturamento: R${maiorFaturamento:F2}");
        Console.WriteLine($"Número de dias com faturamento superior à média: {diasAcimaMedia}");
    }
}
