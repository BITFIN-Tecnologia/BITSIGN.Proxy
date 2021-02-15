// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class RenovacaoDeChave : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                var novaChave = await proxy.Configuracoes.RenovarChave(cancellationToken);

                Console.WriteLine(novaChave);

                //Armazenar para futuras requisições;
                this.ChaveDeIntegracao = novaChave;
            }
        }
    }
}