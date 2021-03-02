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

                Console.WriteLine($"Status Geral: {relatorio.Status} - Duração: {relatorio.Duracao}");
                Console.WriteLine("--------- SERVIÇOS ---------");

                foreach (var item in relatorio.Servicos)
                {
                    Console.WriteLine($"Serviço: {item.Nome}");
                    Console.WriteLine($"Status: {item.Status}");
                    Console.WriteLine($"Mensagem: {item.Mensagem}");
                    Console.WriteLine($"Duração: {item.Duracao}");
                    Console.WriteLine();
                }
            }
        }
    }
}