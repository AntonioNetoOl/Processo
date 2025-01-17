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
        
        string caminhoJson = @"C:\Users\MASTER\source\repos\Json";

        
        if (!File.Exists(caminhoJson))
        {
            Console.WriteLine($"O arquivo JSON não foi encontrado no caminho: {caminhoJson}");
            return;
        }

        var dadosJson = CarregarJson(caminhoJson);
        ProcessarFaturamento(dadosJson);
    }

    
    static List<Faturamento> CarregarJson(string caminhoJson)
    {
        string json = File.ReadAllText(caminhoJson); 
        return JsonConvert.DeserializeObject<List<Faturamento>>(json); 
    }
    
    static void ProcessarFaturamento(List<Faturamento> faturamento)
    {
        
        var diasComFaturamento = faturamento.FindAll(f => f.Valor > 0);

        
        double mediaFaturamento = 0;
        foreach (var f in diasComFaturamento)
        {
            mediaFaturamento += f.Valor;
        }
        mediaFaturamento /= diasComFaturamento.Count;

        double menorFaturamento = double.MaxValue;
        double maiorFaturamento = double.MinValue;

        foreach (var f in diasComFaturamento)
        {
            if (f.Valor < menorFaturamento)
                menorFaturamento = f.Valor;
            if (f.Valor > maiorFaturamento)
                maiorFaturamento = f.Valor;
        }

        int diasAcimaMedia = 0;
        foreach (var f in diasComFaturamento)
        {
            if (f.Valor > mediaFaturamento)
                diasAcimaMedia++;
        }

        Console.WriteLine($"Menor valor de faturamento: R${menorFaturamento:F2}");
        Console.WriteLine($"Maior valor de faturamento: R${maiorFaturamento:F2}");
        Console.WriteLine($"Número de dias com faturamento superior à média: {diasAcimaMedia}");
    }
}
