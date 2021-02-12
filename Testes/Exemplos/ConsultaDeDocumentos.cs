// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class ConsultaDeDocumentos : Exemplo
    {
        public override async Task Executar(params string[] parametros)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                //Retorna todas as informações de um determinado documento, exceto seus arquivos (bytes[]).
                var detalhes = await proxy.Documentos.Detalhes(new Guid("aa9076b3-a058-44e2-b776-dca0a1743ce7"));

                if (detalhes != null)
                {
                    Console.WriteLine($"Status: {detalhes.Status}");

                    //Individualmente, cada método a seguir retorna: arquivo origninal, assinado e o manifesto.
                    File.WriteAllBytes(detalhes.NomeDoArquivo, await proxy.Documentos.Original(detalhes.Id));

                    if (detalhes.Status == "Assinado")
                    {
                        File.WriteAllBytes(detalhes.NomeDoArquivoAssinado, await proxy.Documentos.Assinado(detalhes.Id));
                        File.WriteAllBytes(detalhes.NomeDoArquivoDeManifesto, await proxy.Documentos.Manifesto(detalhes.Id));
                    }
                }
            }
        }
    }
}