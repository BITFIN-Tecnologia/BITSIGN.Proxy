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
            this.Conexao = new Conexao(Ambiente.Sandbox, this.CodigoDoContratante, this.ChaveDeIntegracao, FormatoDeSerializacao.Json);

        public abstract Task Executar(CancellationToken cancellationToken = default);

        protected Guid CodigoDoContratante { get; } = new Guid("39cf6084-655b-4355-a321-b7dc8d49ce6a");

        protected string ChaveDeIntegracao { get; set; } = "Wm1VM05HSXlNRGN0TmpSa09TMDBaVEEwTFdFNFpUSXRZVEE1TmpBNFl6VmtOemsx";

        protected Conexao Conexao { get; }
    }
}