// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testes.Exemplos;

namespace Testes
{
    class Program
    {
        private static readonly Dictionary<int, Exemplo> exemplos = new Dictionary<int, Exemplo>()
        {
            { 1, new UploadDeLote() },
            { 2, new LogsComDepuracao() },
            { 3, new ConsultaDeLotes() },
            { 4, new ConsultaDeDocumentos() },
            { 5, new DownloadDePacote() },
            { 6, new DadosFinanceiros() }
        };

        static async Task Main(string[] args)
        {
            Console.WriteLine("EXEMPLOS DISPONÍVEIS");

            foreach (var e in exemplos)
                Console.WriteLine($"  {e.Key:00} - {e.Value.GetType().Name}");

            Console.WriteLine();
            Console.Write("INFORME O NÚMERO DO EXEMPLO: ");

            if (int.TryParse(Console.ReadLine(), out var chave) && exemplos.TryGetValue(chave, out var exemplo))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"EXECUTANDO: {exemplo.GetType().Name}");
                Console.ForegroundColor = ConsoleColor.Yellow;

                await exemplo.Executar();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("EXEMPLO NÃO ENCONTRADO");
            }

            Console.ResetColor();
        }
    }
}