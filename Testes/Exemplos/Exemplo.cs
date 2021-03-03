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

        protected Guid CodigoDoContratante { get; } = new Guid("9174a05f-7a5f-4a5f-b3b0-5757fe57a611");

        protected string ChaveDeIntegracao { get; set; } = "TVRObVpqTXpOV0l0T1dGaVlTMDBaVFU0TFRoaU1HSXRZalkwTURSaU16VTNZVEU1";

        protected Conexao Conexao { get; }
    }
}