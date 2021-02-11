﻿// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Collections.Generic;

namespace BITSIGN.Proxy
{
    /// <summary>
    /// Informações necessárias para iniciar a comunicação com o serviço.
    /// </summary>
    public class Conexao
    {
        private static readonly IDictionary<Ambiente, Uri> urls = new Dictionary<Ambiente, Uri>(2)
        {
            { Ambiente.Sandbox, new Uri("http://localhost:33664/api/") },
            { Ambiente.Producao, new Uri("http://localhost:33664/api/") },
        };

        /// <summary>
        /// Inicia a conexão com o mínimo necessário para estabelecer a comunicação com um dos <see cref="Proxy.Ambiente"/>s.
        /// </summary>
        /// <param name="ambiente">Ambiente de produção ou de testes (Sandbox).</param>
        /// <param name="codigoDoContratante">Código exclusivo do contratante.</param>
        /// <param name="codigoDeIntegracao">Código do contratante para integração entre sistemas.</param>
        /// <param name="formato">Define como será serializado o conteúdo das mensagens trocadas com os serviços. O padrão é <see cref="FormatoDeSerializacao.Json"/>.</param>
        /// <exception cref="ArgumentException">Se o <paramref name="codigoDoContratante"/> ou o <paramref name="codigoDeIntegracao"/> forem <see cref="Guid.Empty"/>.</exception>
        public Conexao(Ambiente ambiente, Guid codigoDoContratante, Guid codigoDeIntegracao, FormatoDeSerializacao formato = FormatoDeSerializacao.Json)
        {
            this.Ambiente = ambiente;

            this.CodigoDoContratante = 
                codigoDoContratante != Guid.Empty ? codigoDoContratante : throw new ArgumentException("Código do Contratante não informado.", nameof(codigoDoContratante));

            this.CodigoDeIntegracao = 
                codigoDeIntegracao != Guid.Empty ? codigoDeIntegracao : throw new ArgumentException("Código de Integração não informado.", nameof(codigoDeIntegracao));

            this.FormatoDeSerializacao = formato;
        }

        /// <summary>
        /// Ambiente de Sandbox ou Produção.
        /// </summary>
        public Ambiente Ambiente { get; }

        /// <summary>
        /// Código do Contratante.
        /// </summary>
        public Guid CodigoDoContratante { get; }

        /// <summary>
        /// Código de Integração gerado para o contratante.
        /// </summary>
        public Guid CodigoDeIntegracao { get; }

        /// <summary>
        /// Formato da serialização das mensagens trocadas com os serviços.
        /// </summary>
        public FormatoDeSerializacao FormatoDeSerializacao { get; }

        /// <summary>
        /// Endereço HTTP para o serviço, variando de acordo com o <see cref="Ambiente"/>.
        /// </summary>
        public Uri Url => urls[this.Ambiente];
    }
}