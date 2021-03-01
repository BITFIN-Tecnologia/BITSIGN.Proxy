﻿// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
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

                Console.WriteLine($"Status Geral: {Traduzir(relatorio.Status)} - Duração: {relatorio.Duracao}");
                Console.WriteLine("--------- SERVIÇOS ---------");

                foreach (var item in relatorio.Servicos)
                {
                    Console.WriteLine($"Serviço: {item.Nome}");
                    Console.WriteLine($"Status: {Traduzir(item.Status)}");
                    Console.WriteLine($"Descrição: {item.Descricao}");
                    Console.WriteLine($"Duração: {item.Duracao}");
                    Console.WriteLine();
                }
            }

            static string Traduzir(string status) =>
                status switch
                {
                    "Healthy" => "Online",
                    "Unhealthy" => "Offline",
                    "Degraded" => "Degradado",
                    _ => "Indeterminado"
                };
        }
    }
}