// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Parâmetros para revogação de certificado.
    /// </summary>
    public class RevogacaoDeCertificado
    {
        /// <summary>
        /// Comprometimento da Chave Privada.
        /// </summary>
        public const string ComprometimentoDaChavePrivada = "Comprometimento da Chave Privada";
        /// <summary>
        /// Encerramento de Relacionamento.
        /// </summary>
        public const string EncerramentoDeRelacionamento = "Encerramento de Relacionamento";
        /// <summary>
        /// Certificado Substituído.
        /// </summary>
        public const string CertificadoSubstituido = "Certificado Substituído";
        /// <summary>
        /// Interrupção das Atividades.
        /// </summary>
        public const string InterrupcaoDasAtividades = "Interrupção das Atividades";
        /// <summary>
        /// Esquecimento da Senha de Instalação.
        /// </summary>
        public const string EsquecimentoDaSenhaDeInstalacao = "Esquecimento da Senha de Instalação";
        /// <summary>
        /// Emitido Indevidamente.
        /// </summary>
        public const string EmitidoIndevidamente = "Emitido Indevidamente";
        /// <summary>
        /// Dados Incorrretos.
        /// </summary>
        public const string DadosIncorrretos = "Dados Incorrretos";
        /// <summary>
        /// Outro Motivo.
        /// </summary>
        public const string OutroMotivo = "Outro Motivo";

        /// <summary>
        /// Identificador da Unidade Organizacional (OU).
        /// </summary>
        public Guid OrganizacaoId { get; set; }

        /// <summary>
        /// Comprometimento da Chave Privada, Encerramento de Relacionamento, Certificado Substituído, Interrupção das Atividades, Esquecimento da Senha de Instalação, Emitido Indevidamente, Dados Incorrretos, Outro Motivo.
        /// </summary>
        public string Motivo { get; set; }

        /// <summary>
        /// Complemento para justificativa do motivo de revogação.
        /// </summary>
        public string Complemento { get; set; }
    }
}