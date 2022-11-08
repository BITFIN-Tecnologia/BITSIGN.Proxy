// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Informações sobre o certificado digital.
    /// </summary>
    [DebuggerDisplay("{Thumbprint,nq}")]
    public class Certificado : Base
    {
        /// <summary>
        /// Data de cadastro na plataforma.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Nome da entidade (Pessoa Física ou Jurídica) proprietária do certificado ou do domínio quando se tratar de SSL.
        /// </summary>
        public string NomeDoProprietario { get; set; }

        /// <summary>
        /// Documento da entidade (CPF ou CNPJ) proprietária do certificado.
        /// </summary>
        public string DocumentoDoProprietario { get; set; }

        /// <summary>
        /// E-mail associado ao certificado (RFC 822).
        /// </summary>
        public string EmailDoProprietario { get; set; }

        /// <summary>
        /// Série encontrada no certificado (A1, A2, A3 e A4)."
        /// </summary>
        public string Serie { get; set; }

        /// <summary>
        /// Identificação sobre a entidade para qual o certificado foi emitido.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Nome da Autoridade Certificadora, emissora do certificado.
        /// </summary>
        public string Emissor { get; set; }

        /// <summary>
        /// Hash sobre as propriedades do certificado.
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Número atribuído pela Autoridade Certificadora, emissora do certificado.
        /// </summary>
        public string NumeroDeSerie { get; set; }

        /// <summary>
        /// Data de início de sua validade.
        /// </summary>
        public DateTime DataDeValidadeInicial { get; set; }

        /// <summary>
        /// Data final de sua validade.
        /// </summary>
        public DateTime DataDeValidadeFinal { get; set; }

        /// <summary>
        /// Versão.
        /// </summary>
        public int? Versao { get; set; }

        /// <summary>
        /// Indica se o certificado está revogado.
        /// </summary>
        public bool Revogado { get; set; }

        /// <summary>
        /// Data em que o certificado foi revogado.
        /// </summary>
        public DateTime? DataDeRevogacao { get; set; }
    }
}