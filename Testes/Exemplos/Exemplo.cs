// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.Utilitarios;
using System;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public abstract class Exemplo
    {
        public Exemplo() =>
            this.Conexao = new Conexao(Ambiente.Sandbox, this.CodigoDoContratante, this.ChaveDeIntegracao, FormatoDeSerializacao.Json);

        public abstract Task Executar(params string[] parametros);

        protected Guid CodigoDoContratante { get; } = new Guid("985e0702-e94a-4954-b7a8-1f28c73c8122");

        protected string ChaveDeIntegracao { get; } = "5a83a804-1416-476f-8c78-d4d1b8d33fe4";

        protected Conexao Conexao { get; }
    }
}