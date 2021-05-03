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
    public class DownloadDePacote : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                //Retorna o pacote, contendo o arquivo de manifesto.xml, arquivos originais e assinados.
                var pacote = await proxy.Lotes.Pacote(new("06202cf4-281d-46a5-bd81-975c15f58d94"), cancellationToken);

                Console.WriteLine(pacote.Lote.Id);

                foreach (var a in pacote.Arquivos)
                    File.WriteAllBytes(a.Item1, a.Item2);
            }
        }
    }
}