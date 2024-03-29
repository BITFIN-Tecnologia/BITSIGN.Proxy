﻿// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
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
                    Id = Guid.Parse("E4005970-F755-4D6D-8F71-F7A4937C5F9D"),
                    Ativa = true,
                    Configuracao = new()
                    {
                        AcompanhamentoAoVivo = true,
                        DocumentosDoPacote = "Assinado;Manifesto",
                        EventosReportados = "Lote",
                        FormatoDeCallback = "JSON",
                        HeadersDeCallback = "AppKey=123",
                        IPsAutorizados = "127.0.0.1",
                        PosicaoDoCarimbo = "Rodapé"
                    }
                }, cancellationToken);
            }
        }
    }
}