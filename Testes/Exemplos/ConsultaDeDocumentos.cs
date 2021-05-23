// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class ConsultaDeDocumentos : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                //Retorna todas as informações de um determinado documento, exceto seus arquivos (bytes[]).
                var documento = await proxy.Documentos.Detalhes(new Guid("0007EF79-EB33-4A31-9FF1-069424192FFF"), cancellationToken);

                if (documento != null)
                {
                    Console.WriteLine($"Status: {documento.Status}");

                    //Individualmente, cada método a seguir retorna: arquivo origninal, assinado e o manifesto.
                    File.WriteAllBytes(documento.NomeDoArquivo, await proxy.Documentos.Original(documento.Id));

                    if (documento.Status == "Assinado")
                    {
                        File.WriteAllBytes(documento.NomeDoArquivoAssinado, await proxy.Documentos.Assinado(documento.Id));
                        File.WriteAllBytes(documento.NomeDoArquivoDeManifesto, await proxy.Documentos.Manifesto(documento.Id));
                    }
                }
            }
        }
    }
}