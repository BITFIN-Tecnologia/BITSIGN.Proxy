// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class NotificacoesDoLote : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                //Listando as notificações de um determinado lote
                var loteId = Guid.Parse("...");

                var notificacoes = await proxy.Lotes.Notificacoes(loteId, cancellationToken);

                //Solicitando o replay das notificações
                foreach (var n in notificacoes)
                    await proxy.Notificacoes.Replay(n.Id, cancellationToken);
            }
        }
    }
}