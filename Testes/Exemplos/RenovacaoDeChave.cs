// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
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
                var novaChave = await proxy.Aplicacoes.RenovarChave(Guid.Parse("e4005970-f755-4d6d-8f71-f7a4937c5f9d"), cancellationToken);

                Console.WriteLine(novaChave);

                //Armazenar para futuras requisições;
                this.ChaveDeIntegracao = novaChave;
            }
        }
    }
}