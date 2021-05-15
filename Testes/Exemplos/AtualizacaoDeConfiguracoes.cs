// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class AtualizacaoDeConfiguracoes : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                await proxy.Aplicacoes.Atualizar(new()
                {
                    Id = Guid.Parse("41440b4c-280f-40ee-bdce-e6fdc420d1cf"),
                    Ativa = false,
                    Configuracao = new()
                    {
                        AcompanhamentoAoVivo = true,
                        DocumentosDoPacote = "Assinado;Manifesto",
                        EventosReportados = "Lote",
                        FormatoDeCallback = "JSON",
                        HeadersDeCallback = "AppKey=123",
                        IPsAutorizados = "127.0.0.1",
                        PosicaoDoCarimbo = "Rodapé",
                        UrlDeCallback = "https://www.empresa.com.br/assinaturas"
                    }
                }, cancellationToken);
            }
        }
    }
}