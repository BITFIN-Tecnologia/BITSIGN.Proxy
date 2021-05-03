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

        protected Guid CodigoDoContratante { get; } = new("09900CE6-2DC0-4593-8653-DCE6151C31C8");

        protected string ChaveDeIntegracao { get; set; } = "T1RaaU9URXhOekl0T1dNek55MDBOR1ExTFRoaFkyRXRZbU0wWm1FNE5USTBaakJo";

        protected Conexao Conexao { get; }
    }
}