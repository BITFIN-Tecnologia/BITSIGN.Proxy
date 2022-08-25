// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Parâmetros para emissão de certificado.
    /// </summary>
    [DebuggerDisplay("{NomeDoProprietario,nq}")]
    public class EmissaoDeCertificado
    {
        /// <summary>
        /// Identificador da Unidade Organizacional (OU).
        /// </summary>
        public Guid OrganizacaoId { get; set; }

        /// <summary>
        /// Nome da entidade (Pessoa Física ou Jurídica) proprietária do certificado.
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
        /// Data/hora final da validade do certificado.
        /// </summary>
        public DateTime DataDeValidade { get; set; }

        /// <summary>
        /// Senha para geração do arquivo PFX.
        /// </summary>
        public string Senha { get; set; }
    }
}