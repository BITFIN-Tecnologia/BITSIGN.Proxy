// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Collections.Generic;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Resultado da consulta realizada através dos buscadores.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Busca<T>
    {
        /// <summary>
        /// Dados retornados pela consulta, sub-dividido pela paginação.
        /// </summary>
        public T Dados { get; set; }

        /// <summary>
        /// Informações sobre a paginação dos dados retornados.
        /// </summary>
        public Paginacao Paginador { get; set; }
    }

    /// <summary>
    /// Resultado da consulta de lotes.
    /// </summary>
    public class Lotes : Busca<IEnumerable<Lote>> { }

    /// <summary>
    /// Resultado da consulta de documentos.
    /// </summary>
    public class Documentos : Busca<IEnumerable<Documento>> { }

    /// <summary>
    /// Resultado da consulta de notificações e callbacks.
    /// </summary>
    public class Notificacoes : Busca<IEnumerable<Notificacao>> { }
}