// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class StatusDosServicos : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                var relatorio = await proxy.Status.Atualizar(cancellationToken);

                Console.WriteLine($"Status Geral: {relatorio.Status} - Duração: {relatorio.Duracao} ms");
                Console.WriteLine("--------- SERVIÇOS ---------");

                foreach (var s in relatorio.Servicos)
                {
                    Console.WriteLine($"Serviço: {s.Nome}");
                    Console.WriteLine($"Status: {s.Status}");
                    Console.WriteLine($"Mensagem: {s.Mensagem}");
                    Console.WriteLine($"Duração: {s.Duracao} ms");
                    Console.WriteLine();
                }
            }
        }
    }
}