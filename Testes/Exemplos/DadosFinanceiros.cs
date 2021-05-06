// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class DadosFinanceiros : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                //Percorre os planos contratados.
                foreach (var pc in await proxy.Financeiro.Planos(cancellationToken))
                {
                    Console.WriteLine(pc.Plano.Nome);
                    Console.WriteLine(pc.Plano.Quantidade);
                }

                //Percorre os últimos 12 fechamentos realizados.
                foreach (var f in await proxy.Financeiro.Fechamentos(cancellationToken))
                {
                    Console.WriteLine($"Período: {f.Mes:00}/{f.Ano}");
                    Console.WriteLine($"Valor: {f.ValorTotal:N2}");
                    Console.WriteLine($"Status: {f.Status}");
                }

                //Detalhes sobre o fechamento.
                var fechamento = await proxy.Financeiro.Fechamento(new("a0ef4f82-91a6-4a28-a9ae-361c3c0a427d"), cancellationToken);

                if (fechamento != null)
                {
                    Console.WriteLine($"Período: {fechamento.Mes:00}/{fechamento.Ano}");
                    Console.WriteLine($"Valor: {fechamento.ValorTotal:N2}");
                    Console.WriteLine($"Status: {fechamento.Status}");
                    Console.WriteLine($"Qtde. de Lotes: {fechamento.QtdeDeLotes}");
                    Console.WriteLine($"Qtde. de Documentos: {fechamento.QtdeDeDocumentos}");
                    Console.WriteLine($"Qtde. de Assinaturas: {fechamento.QtdeDeAssinaturas}");
                    Console.WriteLine($"Plano Contratato: {fechamento.Plano.Nome}");
                }
            }
        }
    }
}