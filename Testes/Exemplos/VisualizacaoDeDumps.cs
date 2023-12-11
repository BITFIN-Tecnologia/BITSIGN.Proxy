// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class VisualizacaoDeDumps : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            var id = "39c0a7b5";

            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                var requisicaoId = Guid.Empty;

                foreach (var dump in await proxy.Dumps.Relacao(id, cancellationToken))
                {
                    Console.WriteLine($"{dump.Data}: {dump.RequisicaoId} ({dump.Formato})");
                    requisicaoId = dump.RequisicaoId;
                }

                Console.WriteLine();
                Console.WriteLine(await proxy.Dumps.Conteudo(id, requisicaoId, cancellationToken));
            }
        }
    }
}