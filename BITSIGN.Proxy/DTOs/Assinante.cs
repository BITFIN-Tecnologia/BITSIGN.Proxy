// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Informações sobre o assinante.
    /// </summary>
    [DebuggerDisplay("{Entidade.Nome,nq}")]
    public class Assinante : Base
    {
        /// <summary>
        /// Pessoa física ou jurídica definida como assinante.
        /// </summary>
        public Entidade Entidade { get; set; }

        /// <summary>
        /// Indica a entidade e o tipo de vínculo à qual o assinante está associado.
        /// </summary>
        public EntidadeVinculada EntidadeVinculada { get; set; }

        /// <summary>
        /// Indica o status atual do assinante.
        /// </summary>
        [XmlAttribute]
        public string Status { get; set; }

        /// <summary>
        /// Data em que o status foi definido.
        /// </summary>
        [XmlAttribute]
        public DateTime DataDoStatus { get; set; }

        /// <summary>
        /// Indica quem forneceu a data/hora para a assinatura.
        /// </summary>
        [XmlAttribute]
        public string ProvedorDoTempo { get; set; }

        /// <summary>
        /// Indica se o assinante é ou não obrigatório.
        /// </summary>
        [XmlAttribute]
        public bool Obrigatorio { get; set; }

        /// <summary>
        /// Parâmetros da assinatura no documento (padrão PAdES ou XAdES).
        /// </summary>
        public virtual Posicao Posicao { get; set; }

        /// <summary>
        /// Observações complementares do assinante. Serve para indicar, por exemplo, o motivo de rejeição.
        /// </summary>
        [XmlAttribute]
        public string Observacoes { get; set; }

        /// <summary>
        /// Indica se o assinante deve ser notificado de que há um novo documento disponível para assinatura.
        /// </summary>
        [XmlAttribute]
        public bool Notificar { get; set; }
    }

    /// <summary>
    /// Informações sobre entidade vinculada ao assinante.
    /// </summary>
    [DebuggerDisplay("{Entidade.Nome,nq} ({Tipo,nq})")]
    public class EntidadeVinculada
    {
        /// <summary>
        /// Dados da Entidade.
        /// </summary>
        public Entidade Entidade { get; set; }

        /// <summary>
        /// Tipo de vínculo (representado, outorgante, etc.) em que a <see cref="Entidade"/> está associada ao <see cref="Assinante"/>.
        /// </summary>
        [XmlAttribute]
        public string Tipo { get; set; }
    }
}