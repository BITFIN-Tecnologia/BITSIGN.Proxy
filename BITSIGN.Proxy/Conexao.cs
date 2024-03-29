﻿// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BITSIGN.Proxy
{
    /// <summary>
    /// Informações necessárias para iniciar a comunicação com o serviço.
    /// </summary>
    [DebuggerDisplay("{Nome,nq}")]
    public class Conexao
    {
        private static readonly Dictionary<Ambiente, string> apis = new(3)
        {
            { Ambiente.Sandbox, "https://sandbox.bitsign.com.br/api/{0}/" },
            { Ambiente.Producao, "https://bitsign.com.br/api/{0}/" },
            { Ambiente.Local, "http://localhost:33664/api/{0}/" }
        };

        private static readonly Dictionary<Ambiente, Uri> status = new(3)
        {
            { Ambiente.Sandbox, new("https://sandbox.bitsign.com.br/status") },
            { Ambiente.Producao, new("https://bitsign.com.br/status") },
            { Ambiente.Local, new("http://localhost:33664/status") }
        };

        /// <summary>
        /// Inicializa a conexão com o mínimo necessário para estabelecer a comunicação com um determinado <see cref="Proxy.Ambiente"/>.
        /// </summary>
        /// <param name="nome">Identifica à qual aplicação se refere a conexão.</param>
        /// <param name="ambiente">Ambiente de testes (Sandbox), produção ou local.</param>
        /// <param name="versao">Versão da API que deve ser utilizada.</param>
        /// <param name="codigoDoContratante">Código exclusivo do contratante.</param>
        /// <param name="codigoDaAplicacao">Código identificador da Aplicação.</param>
        /// <param name="chaveDeIntegracao">Chave de integração da Aplicação.</param>
        /// <param name="formato">Define como será serializado o conteúdo das mensagens trocadas com os serviços. O padrão é <see cref="FormatoDeSerializacao.Json"/>.</param>
        /// <param name="timeout">Define o tempo máximo de espera permitido para executar uma requisição. O tempo padrão é de 100 segundos.</param>
        /// <exception cref="ArgumentException">Se o <paramref name="codigoDoContratante"/> ou o <paramref name="chaveDeIntegracao"/> forem <see cref="Guid.Empty"/> ou se a <paramref name="versao"/> for vazia.</exception>
        public Conexao(string nome, Ambiente ambiente, string versao, Guid codigoDoContratante, Guid codigoDaAplicacao, string chaveDeIntegracao, FormatoDeSerializacao formato = FormatoDeSerializacao.Json, TimeSpan? timeout = null)
        {
            this.Nome = !string.IsNullOrWhiteSpace(nome) ? nome : "Conexão Principal";

            this.Versao = !string.IsNullOrWhiteSpace(versao) ? versao : throw new ArgumentException("Versão não informada.", nameof(versao));

            this.CodigoDoContratante =
                codigoDoContratante != Guid.Empty ? codigoDoContratante : throw new ArgumentException("Código do Contratante não informado.", nameof(codigoDoContratante));

            this.CodigoDaAplicacao =
                codigoDaAplicacao != Guid.Empty ? codigoDaAplicacao : throw new ArgumentException("Código da Aplicação não informado.", nameof(codigoDaAplicacao));

            this.ChaveDeIntegracao =
                !string.IsNullOrWhiteSpace(chaveDeIntegracao) ? chaveDeIntegracao : throw new ArgumentException("Chave de integração não informada.", nameof(chaveDeIntegracao));

            this.FormatoDeSerializacao = formato;
            this.Timeout = timeout ?? TimeSpan.FromSeconds(100);

            this.Ambiente = ambiente;
            this.Url = new(string.Format(apis[ambiente], this.Versao));
            this.Status = status[ambiente];
        }

        /// <summary>
        /// Identifica à qual aplicação se refere a conexão.
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Sandbox, Produção ou Local.
        /// </summary>
        public Ambiente Ambiente { get; private set; }

        /// <summary>
        /// Versão da API.
        /// </summary>
        public string Versao { get; private set; }

        /// <summary>
        /// Código do Contratante.
        /// </summary>
        public Guid CodigoDoContratante { get; private set; }

        /// <summary>
        /// Código identificador da Aplicação.
        /// </summary>
        public Guid CodigoDaAplicacao { get; private set; }

        /// <summary>
        /// Chave de Integração gerado para o contratante.
        /// </summary>
        public string ChaveDeIntegracao { get; private set; }

        /// <summary>
        /// Formato da serialização das mensagens trocadas com os serviços.
        /// </summary>
        public FormatoDeSerializacao FormatoDeSerializacao { get; private set; }

        /// <summary>
        /// Define o tempo máximo de espera permitido para executar uma requisição. O tempo padrão é de 100 segundos.
        /// </summary>
        public TimeSpan Timeout { get; private set; }

        /// <summary>
        /// Endereço base (HTTP) onde as APIs estão hospedadas, que varia de acordo com o <see cref="Ambiente"/>.
        /// </summary>
        public Uri Url { get; private set; }

        /// <summary>
        /// Endpoint que resume o status atual dos serviços e seus recursos.
        /// </summary>
        public Uri Status { get; private set; }

        /// <summary>
        /// Retorna o <see cref="Nome"/> da conexão.
        /// </summary>
        public override string ToString() => this.Nome;

        /// <summary>
        /// Retorna a conexão em formato do header Authorization.
        /// </summary>
        /// <param name="comLabel">Indica se deve incluir a chave Authorization. O padrão é <c>true</c>.</param>
        /// <returns><see cref="String"/> representando a respectiva conexão.</returns>
        public string GerarCabecalho(bool comLabel = true) =>
            comLabel ?
                $"Authorization:Basic {$"{this.CodigoDoContratante}:{this.ChaveDeIntegracao}".EmBase64()}" :
                $"{$"{this.CodigoDoContratante}:{this.ChaveDeIntegracao}".EmBase64()}";
    }
}