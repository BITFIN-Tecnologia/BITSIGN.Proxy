// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.Utilitarios;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public abstract class Exemplo
    {
        public Exemplo() =>
            this.Conexao =
                new(
                    "Testes", 
                    Ambiente.Sandbox, 
                    this.Versao, 
                    this.CodigoDoContratante, 
                    this.CodigoDaAplicacao, 
                    this.ChaveDeIntegracao, 
                    FormatoDeSerializacao.Json);

        public abstract Task Executar(CancellationToken cancellationToken = default);

        protected string Versao { get; } = "v1";

        protected Guid CodigoDoContratante { get; } = new("985e0702-e94a-4954-b7a8-1f28c73c8122");

        protected Guid CodigoDaAplicacao { get; } = new("98b4307e-51d4-4f2f-88da-cbb23b903db5");

        protected string ChaveDeIntegracao { get; set; } = "TWpZd00yTXpPVGN0TmpFMk9TMDBaRGRqTFdFMk1XTXROR1kzWkRVM01qTmhNR0Zq";

        protected Conexao Conexao { get; }
    }
}