// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.DTOs;
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
                await proxy.Configuracoes.Atualizar(new Integracao()
                {
                    AcompanhamentoAoVivo = true,
                    DocumentosDoPacote = "Assinado;Manifesto",
                    EventosReportados = "Lote",
                    FormatoDeCallback = "JSON",
                    HeadersDeCallback = "AppKey=123",
                    IPsAutorizados = "127.0.0.1",
                    PosicaoDoCarimbo = "Rodapé",
                    UrlDeCallback = "https://www.empresa.com.br/assinaturas"
                }, cancellationToken);
            }
        }
    }
}