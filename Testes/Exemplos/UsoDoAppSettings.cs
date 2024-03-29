﻿// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class UsoDoAppSettings : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                Console.WriteLine($"Ambiente: {proxy.Conexao.Ambiente}");
                Console.WriteLine($"Url: {proxy.Conexao.Url}");
                Console.WriteLine($"CodigoDoContratante: {proxy.Conexao.CodigoDoContratante}");
                Console.WriteLine($"ChaveDeIntegracao: {proxy.Conexao.ChaveDeIntegracao}");

                await Task.CompletedTask;
            }
        }
    }
}