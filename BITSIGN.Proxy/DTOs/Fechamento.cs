// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Apuração mensal das assinaturas realizadas.
    /// </summary>
    [DebuggerDisplay("Período: {Mes}/{Ano} - Valor: {ValorTotal}")]
    public class Fechamento : Base
    {
        /// <summary>
        /// Contratante associado ao fechamento.
        /// </summary>
        public Contratante Contratante { get; set; }

        /// <summary>
        /// Detalhes do plano contratado.
        /// </summary>
        public Plano Plano { get; set; }

        /// <summary>
        /// Ano do período de apuração.
        /// </summary>
        [XmlAttribute]
        public int Ano { get; set; }

        /// <summary>
        /// Mês do período de apuração.
        /// </summary>
        [XmlAttribute]
        public int Mes { get; set; }

        /// <summary>
        /// Indica o status atual do fechamento.
        /// </summary>
        [XmlAttribute]
        public string Status { get; set; }

        /// <summary>
        /// Data em que o status foi definido.
        /// </summary>
        [XmlAttribute]
        public DateTime DataDoStatus { get; set; }

        /// <summary>
        /// Data em que a apuração foi realizada.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Valor total apurado, calculado com base no plano contratado.
        /// </summary>
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// Quantidade de lotes enviados no período.
        /// </summary>
        public int QtdeDeLotes { get; set; }

        /// <summary>
        /// Quantidade de documentos enviados no período.
        /// </summary>
        public int QtdeDeDocumentos { get; set; }

        /// <summary>
        /// Quantidade de assinaturas realizadas no período.
        /// </summary>
        public int QtdeDeAssinaturas { get; set; }

        /// <summary>
        /// Total de armazenamento consumido no período.
        /// </summary>
        public long Armazenamento { get; set; }

        /// <summary>
        /// Data de vencimento para pagamento.
        /// </summary>
        public DateTime DataDeVencimento { get; set; }

        /// <summary>
        /// Valores vigentes do plano contratatado.
        /// </summary>
        public ValoresVigentes Valores { get; set; }

        /// <summary>
        /// Informações sobre a nota fiscal associada à este fechamento.
        /// </summary>
        public NotaFiscal NotaFiscal { get; set; }
    }
}