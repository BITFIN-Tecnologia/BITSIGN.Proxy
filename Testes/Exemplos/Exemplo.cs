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
            this.Conexao = new Conexao(Ambiente.Sandbox, this.Versao, this.CodigoDoContratante, this.ChaveDeIntegracao, FormatoDeSerializacao.Json);

        public abstract Task Executar(CancellationToken cancellationToken = default);

        protected string Versao { get; } = "v1";

        protected Guid CodigoDoContratante { get; } = new("E99B78CA-2ABA-4FD2-B8DC-30CBCEAAD090");

        protected string ChaveDeIntegracao { get; set; } = "D23D751E7E1F3C0D3BC7C90862C34CEFE8BCD7BC";

        protected Conexao Conexao { get; }
    }
}