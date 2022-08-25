// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Testes.Exemplos;

namespace Testes
{
    class Program
    {
        private static readonly Dictionary<int, Exemplo> exemplos = new()
        {
            { 1, new UploadDePacote() },
            { 2, new UploadDeLote() },
            { 3, new LogsComDepuracao() },
            { 4, new ConsultaDeLotes() },
            { 5, new ConsultaDeDocumentos() },
            { 6, new DownloadDePacote() },
            { 7, new DadosFinanceiros() },
            { 8, new AtualizacaoDeConfiguracoes() },
            { 9, new RenovacaoDeChave() },
            { 10, new NotificacoesDoLote() },
            { 11, new UsoDoAppSettings() },
            { 12, new DadosDoContratante() },
            { 13, new StatusDosServicos() },
            { 14, new BuscadorDeRecursos() },
            { 15, new TratamentoDeErros() },
            { 16, new DocumentosComTemplate() },
            { 17, new PadraoPAdES() },
            { 18, new PadraoXAdES() },
            { 19, new VisualizacaoDeDumps() },
            { 20, new ValidandoCertificado() },
            { 21, new EmissaoDeCertificados() }
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

                using (var cts = new CancellationTokenSource())
                    await exemplo.Executar(cts.Token);
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