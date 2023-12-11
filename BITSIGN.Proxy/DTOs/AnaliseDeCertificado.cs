// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Instruções para análise do certificado.
    /// </summary>
    public class AnaliseDeCertificado
    {
        /// <summary>
        /// Arquivo *.cer ou *.pfx codificado em Base64.
        /// </summary>
        public string Certificado { get; set; }

        /// <summary>
        /// Data de referência para validação da expiração.
        /// </summary>
        public DateTime? DataDeReferencia { get; set; }

        /// <summary>
        /// Senha para acessar a chave privada (quando houver).
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Indica se deve validar a presença da chave privada.
        /// </summary>
        public bool ValidaChavePrivada { get; set; }
    }
}