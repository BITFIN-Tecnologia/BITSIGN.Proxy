// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class BuscadorDeRecursos : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                var lotes = await proxy.Buscador.Lotes(new()
                {
                    DataInicial = DateTime.Now.AddDays(-20),
                    DataFinal = DateTime.Now,
                    BaseDaData = "Criacao",
                    Paginador = new()
                    {
                        PaginaAtual = 1,
                        RegistrosPorPagina = 10
                    }
                }, cancellationToken);

                foreach (var l in lotes.Dados)
                {
                    Console.WriteLine($"Id: {l.Id}");
                    Console.WriteLine($"Data: {l.Data:dd/MM/yyyy}");
                    Console.WriteLine($"Tags: {l.Tags}");
                    Console.WriteLine();
                }
            }
        }
    }
}