// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Documento para assinatura digital.
    /// </summary>
    [DebuggerDisplay("Tipo: {Tipo,nq} - Arquivo: {NomeDoArquivo,nq} - {Descricao,nq}")]
    public class Documento : Base
    {
        /// <summary>
        /// Data de criação do documento.
        /// </summary>
        [XmlAttribute]
        public DateTime Data { get; set; }

        /// <summary>
        /// Descrição amigável do arquivo.
        /// </summary>
        [XmlAttribute]
        public string Descricao { get; set; }

        /// <summary>
        /// Tipo do documento (opções predefinidas).
        /// </summary>
        [XmlAttribute]
        public string Tipo { get; set; }

        /// <summary>
        /// Nome do arquivo (original) dentro do pacote.
        /// </summary>
        [XmlAttribute]
        public string NomeDoArquivo { get; set; }

        /// <summary>
        /// Formato do arquivo (PDF, XML, etc.).
        /// </summary>
        [XmlAttribute]
        public string FormatoDoArquivo { get; set; }

        /// <summary>
        /// Tamanho (em bytes) do arquivo.
        /// </summary>
        [XmlAttribute]
        public long TamanhoDoArquivo { get; set; }

        /// <summary>
        /// Código hash do arquivo original.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Padrão de assinatura (CAdES, PAdES ou XAdES).
        /// </summary>
        [XmlAttribute]
        public string PadraoDeAssinatura { get; set; }

        /// <summary>
        /// Política de assinatura utilizada.
        /// </summary>
        [XmlAttribute]
        public string PoliticaDeAssinatura { get; set; }

        /// <summary>
        /// Tags utilizadas para correlacionar o documento ao sistema do contratante.
        /// </summary>
        [XmlAttribute]
        public string Tags { get; set; }

        /// <summary>
        /// Indica o status atual do documento.
        /// </summary>
        [XmlAttribute]
        public string Status { get; set; }

        /// <summary>
        /// Data em que o status foi definido.
        /// </summary>
        [XmlAttribute]
        public DateTime DataDoStatus { get; set; }

        /// <summary>
        /// Informa o nome do arquivo assinado.
        /// </summary>
        [XmlAttribute]
        public string NomeDoArquivoAssinado { get; set; }

        /// <summary>
        /// Informa o nome do arquivo de manifesto de assinaturas.
        /// </summary>
        [XmlAttribute]
        public string NomeDoArquivoDeManifesto { get; set; }

        /// <summary>
        /// Informa o nome do arquivo original com de manifesto de assinaturas adicionado.
        /// </summary>
        [XmlAttribute]
        public string NomeDoArquivoOriginalComManifesto { get; set; }

        /// <summary>
        /// Conteúdo do arquivo original.
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        public byte[] ConteudoOriginal { get; set; }

        /// <summary>
        /// Conteúdo do arquivo assinado.
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        public byte[] ConteudoAssinado { get; set; }

        /// <summary>
        /// Conteúdo do arquivo de manifesto de assinaturas.
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        public byte[] ConteudoDoManifesto { get; set; }

        /// <summary>
        /// Conteúdo do arquivo original com o manifesto de assinaturas incluído.
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        public byte[] ConteudoDoOrginalComManifesto { get; set; }

        /// <summary>
        /// URL para acesso online do manifesto de assinaturas do documento.
        /// </summary>
        [XmlAttribute]
        public string UrlDoManifesto { get; set; }

        /// <summary>
        /// Relação de assinaturas associadas ao documento.
        /// </summary>
        public List<Assinatura> Assinaturas { get; set; }
    }
}
