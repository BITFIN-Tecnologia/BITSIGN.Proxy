// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Representa uma requsição gerada em ambiente de SANDBOX.
    /// </summary>
    [DebuggerDisplay("{Data} - {Formato,nq} - Tamanho: {Tamanho}")]
    public class Dump
    {
        /// <summary>
        /// Identificador da requisição gerada.
        /// </summary>
        public Guid RequisicaoId { get; set; }

        /// <summary>
        /// Data da criação.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Formato (mime type) do conteúdo.
        /// </summary>
        public string Formato { get; set; }

        /// <summary>
        /// Tamanho (em bytes) do conteúdo.
        /// </summary>
        public int Tamanho { get; set; }

        /// <summary>
        /// Cabeçalhos da requisição.
        /// </summary>
        public string Cabecalho { get; set; }
    }
}