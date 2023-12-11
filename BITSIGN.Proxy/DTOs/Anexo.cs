// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Arquivo anexo ao lote de assinaturas com informações complementares.
    /// </summary>
    [DebuggerDisplay("{NomeDoArquivo,nq}")]
    public class Anexo : Base
    {
        /// <summary>
        /// Nome do arquivo anexo.
        /// </summary>
        [XmlAttribute]
        public string NomeDoArquivo { get; set; }

        /// <summary>
        /// Breve descrição da finalidade do anexo.
        /// </summary>
        [XmlAttribute]
        public string Descricao { get; set; }

        /// <summary>
        /// Conteúdo do arquivo anexo.
        /// </summary>
        [XmlIgnore, JsonIgnore]
        public byte[] Conteudo { get; set; }
    }
}