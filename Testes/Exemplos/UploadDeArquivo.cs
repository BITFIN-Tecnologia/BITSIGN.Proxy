// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class UploadDeArquivo : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            var nomeDoArquivo = "ContratoDeLocacao1.pdf";

            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                var upload = await proxy.Uploads.Enviar(nomeDoArquivo, File.ReadAllBytes($"Exemplo/{nomeDoArquivo}"), cancellationToken);

                Console.WriteLine($"Local: {upload.Url}");

                var arquivo = await proxy.Uploads.Download(upload.Id, upload.NomeDoArquivo, cancellationToken);

                Console.WriteLine($"Download (Tamanho): {arquivo.Length}");
            }
        }
    }
}