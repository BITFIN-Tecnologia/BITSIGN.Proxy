// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class ConsultaDeLotes : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                //Retorna todas as informações de um determinado lote.
                var lote = await proxy.Lotes.Detalhes(new("06202cf4-281d-46a5-bd81-975c15f58d94"), cancellationToken);

                if (lote != null)
                {
                    Console.WriteLine($"Status: {lote.Status}");

                    //Percorre os Documentos, suas Assinaturas e os Assinantes.
                    foreach (var documento in lote.Documentos)
                    {
                        Console.WriteLine($"Documento: {documento.NomeDoArquivo}");

                        foreach (var assinatura in documento.Assinaturas)
                        {
                            Console.WriteLine($"Perfil: {assinatura.Perfil}");

                            foreach (var assinante in assinatura.Assinantes)
                            {
                                Console.WriteLine($"Assinante: {assinante.Entidade.Nome}");
                            }
                        }
                    }
                }
            }
        }
    }
}