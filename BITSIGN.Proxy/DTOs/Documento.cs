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
        /// Tipos de documentos suportados.
        /// </summary>
        public class Tipos
        {
            /// <summary>
            /// Aditivo
            /// </summary>
            public const string Aditivo = "Aditivo";
            /// <summary>
            /// Autorização.
            /// </summary>
            public const string Autorizacao = "Autorização";
            /// <summary>
            /// Carta de Anuência.
            /// </summary>
            public const string CartaDeAnuencia = "Carta de Anuência";
            /// <summary>
            /// Carta de Cessão.
            /// </summary>
            public const string CartaDeCessao = "Carta de Cessão";
            /// <summary>
            /// Carta de Circularização.
            /// </summary>
            public const string CartaDeCircularizacao = "Carta de Circularização";
            /// <summary>
            /// Confissão de Dívida.
            /// </summary>
            public const string ConfissaoDeDivida = "Confissão de Dívida";
            /// <summary>
            /// Conhecimento de Transporte.
            /// </summary>
            public const string ConhecimentoDeTransporte = "Conhecimento de Transporte";
            /// <summary>
            /// Contrato.
            /// </summary>
            public const string Contrato = "Contrato";
            /// <summary>
            /// Debênture.
            /// </summary>
            public const string Debentura = "Debênture";
            /// <summary>
            /// Declaração.
            /// </summary>
            public const string Declaracao = "Declaração";
            /// <summary>
            /// Diploma.
            /// </summary>
            public const string Diploma = "Diploma";
            /// <summary>
            /// Duplicata.
            /// </summary>
            public const string Duplicata = "Duplicata";
            /// <summary>
            /// Exame.
            /// </summary>
            public const string Exame = "Exame";
            /// <summary>
            /// Procuração.
            /// </summary>
            public const string Procuracao = "Procuração";
            /// <summary>
            /// Nota Fiscal.
            /// </summary>
            public const string NotaFiscal = "Nota Fiscal";
            /// <summary>
            /// Nota Promissória.
            /// </summary>
            public const string NotaPromissoria = "Nota Promissória";
            /// <summary>
            /// Termo de Cessão.
            /// </summary>
            public const string TermoDeCessao = "Termo de Cessão";
        }

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
        /// Nome do arquivo (original).
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
        /// Parâmetros para download do arquivo.
        /// </summary>
        public Download Download { get; set; }

        /// <summary>
        /// Nome da Template utilizada para expandir o conteúdo deste arquivo em novos documentos.
        /// </summary>
        [XmlAttribute]
        public string Template { get; set; }

        /// <summary>
        /// Código hash do arquivo original.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Indica se o carimbo do tempo é exigido para as assinaturas no documento.
        /// </summary>
        [XmlAttribute]
        public bool CarimboDoTempo { get; set; }

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
        /// URL para acesso ao vivo do andamento das assinaturas do documento.
        /// </summary>
        [XmlAttribute]
        public string UrlAoVivo { get; set; }

        /// <summary>
        /// Relação de assinaturas associadas ao documento.
        /// </summary>
        public List<Assinatura> Assinaturas { get; set; }
    }
}