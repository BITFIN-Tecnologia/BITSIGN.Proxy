// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Parametros disponíveis para busca de recursos.
    /// </summary>
    public class ParametrosDeBusca
    {
        /// <summary>
        /// Status do Recurso.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Data inicial da busca.
        /// </summary>
        public DateTime DataInicial { get; set; }

        /// <summary>
        /// Data final da busca.
        /// </summary>
        public DateTime DataFinal { get; set; }

        /// <summary>
        /// Identifica se o período de data se aplica à data de criação (Criacao) ou do status (Status).
        /// </summary>
        public string BaseDaData { get; set; }

        /// <summary>
        /// Documento (CPF/CNPJ) da Entidade associada ao Lote.
        /// </summary>
        public string DocumentoDaEntidade { get; set; }

        /// <summary>
        /// Documento (CPF/CNPJ) do Assinante.
        /// </summary>
        public string DocumentoDoAssinante { get; set; }

        /// <summary>
        /// Tags associadas ao recurso.
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Diretório virtual onde está localizado o lote.
        /// </summary>
        public string Diretorio { get; set; }

        /// <summary>
        /// Endereço do destinatário da notificação.
        /// </summary>
        public string EnderecoDoDestinario { get; set; }

        /// <summary>
        /// Informações sobre a paginação da consulta.
        /// </summary>
        public Paginacao Paginador { get; set; }
    }
}