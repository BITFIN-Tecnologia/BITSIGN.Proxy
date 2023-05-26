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
            /// Apólice.
            /// </summary>
            public const string Apolice = "Apólice";
            /// <summary>
            /// Autorização.
            /// </summary>
            public const string Autorizacao = "Autorização";
            /// <summary>
            /// Balanço.
            /// </summary>
            public const string Balanco = "Balanço";
            /// <summary>
            /// Boletim.
            /// </summary>
            public const string Boletim = "Boletim";
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
            /// Cédula de Crédito à Exportação.
            /// </summary>
            public const string CedulaDeCreditoExportacao = "Cédula de Crédito à Exportação";
            /// <summary>
            /// Cédula de Crédito Bancário.
            /// </summary>
            public const string CedulaDeCreditoBancario = "Cédula de Crédito Bancário";
            /// <summary>
            /// Cédula de Crédito Imobiliário.
            /// </summary>
            public const string CedulaDeCreditoImobiliario = "Cédula de Crédito Imobiliário";
            /// <summary>
            /// Cédula de Crédito Rural.
            /// </summary>
            public const string CedulaDeCreditoRural = "Cédula de Crédito Rural";
            /// <summary>
            /// Cédula de Produtor Rural.
            /// </summary>
            public const string CedulaDeProdutorRural = "Cédula de Produtor Rural";
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
            public const string Debenture = "Debênture";
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
            /// Endividamento.
            /// </summary>
            public const string Endividamento = "Endividamento";
            /// <summary>
            /// Exame.
            /// </summary>
            public const string Exame = "Exame";
            /// <summary>
            /// Faturamento.
            /// </summary>
            public const string Faturamento = "Faturamento";
            /// <summary>
            /// Ficha Cadastral.
            /// </summary>
            public const string FichaCadastral = "Ficha Cadastral";
            /// <summary>
            /// Fonte de Dados.
            /// </summary>
            public const string FonteDeDados = "Fonte de Dados";
            /// <summary>
            /// Laudo Técnico.
            /// </summary>
            public const string LaudoTecnico = "Laudo Técnico";
            /// <summary>
            /// Manifesto.
            /// </summary>
            public const string Manifesto = "Manifesto";
            /// <summary>
            /// Nota de Crédito à Exportação.
            /// </summary>
            public const string NotaDeCreditoExportacao = "Nota de Crédito à Exportação";
            /// <summary>
            /// Nota Fiscal.
            /// </summary>
            public const string NotaFiscal = "Nota Fiscal";
            /// <summary>
            /// Nota Promissória.
            /// </summary>
            public const string NotaPromissoria = "Nota Promissória";
            /// <summary>
            /// Nota Promissória Rural.
            /// </summary>
            public const string NotaPromissoriaRural = "Nota Promissória Rural";
            /// <summary>
            /// Procuração.
            /// </summary>
            public const string Procuracao = "Procuração";
            /// <summary>
            /// Prontuário.
            /// </summary>
            public const string Prontuario = "Prontuário";
            /// <summary>
            /// Proposta.
            /// </summary>
            public const string Proposta = "Proposta";
            /// <summary>
            /// Receituário.
            /// </summary>
            public const string Receituario = "Receituário";
            /// <summary>
            /// Recibo.
            /// </summary>
            public const string Recibo = "Recibo";
            /// <summary>
            /// Registro de Ponto.
            /// </summary>
            public const string RegistroDePonto = "Registro de Ponto";
            /// <summary>
            /// Rescisão.
            /// </summary>
            public const string Rescisao = "Rescisão";
            /// <summary>
            /// Termo de Adesão.
            /// </summary>
            public const string TermoDeAdesao = "Termo de Adesão";
            /// <summary>
            /// Termo de Cessão.
            /// </summary>
            public const string TermoDeCessao = "Termo de Cessão";
            /// <summary>
            /// Termo de Cessão.
            /// </summary>
            public const string TermoDeGarantia = "Termo de Garantia";
            /// <summary>
            /// Termo de Titularidade.
            /// </summary>
            public const string TermoDeTitularidade = "Termo de Titularidade";
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
        /// <remarks>As opções disponíveis estão disponíveis em <see cref="Constantes.Templates"/>.</remarks>
        [XmlAttribute]
        public string Template { get; set; }

        /// <summary>
        /// Algoritmo utilizado para geração do hash do arquivo original.
        /// </summary>
        [XmlAttribute]
        public string Algoritmo { get; set; }

        /// <summary>
        /// Código hash (codificado em hexadecimal) do arquivo original.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Indica se o carimbo do tempo é exigido para as assinaturas no documento.
        /// </summary>
        [XmlAttribute]
        public bool CarimboDoTempo { get; set; }

        /// <summary>
        /// Padrão de assinatura (<see cref="Constantes.PadroesDeAssinatura.CAdES"/>, <see cref="Constantes.PadroesDeAssinatura.PAdES"/> ou <see cref="Constantes.PadroesDeAssinatura.XAdES"/>).
        /// </summary>
        [XmlAttribute]
        public string PadraoDeAssinatura { get; set; }

        /// <summary>
        /// Política de assinatura utilizada.
        /// </summary>
        /// <remarks>As opções suportadas estão disponíveis através da classe <see cref="Constantes.Politicas"/>.</remarks>
        [XmlAttribute]
        public string PoliticaDeAssinatura { get; set; }

        /// <summary>
        /// Indica se as assinaturas serão anexadas ao próprio documento. O padrão é <see cref="bool">true</see>.
        /// </summary>
        [XmlAttribute]
        public bool AssinaturaAnexada { get; set; } = true;

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
        [XmlIgnore, JsonIgnore]
        public byte[] ConteudoOriginal { get; set; }

        /// <summary>
        /// Conteúdo do arquivo assinado.
        /// </summary>
        [XmlIgnore, JsonIgnore]
        public byte[] ConteudoAssinado { get; set; }

        /// <summary>
        /// Conteúdo do arquivo de manifesto de assinaturas.
        /// </summary>
        [XmlIgnore, JsonIgnore]
        public byte[] ConteudoDoManifesto { get; set; }

        /// <summary>
        /// Conteúdo do arquivo original com o manifesto de assinaturas incluído.
        /// </summary>
        [XmlIgnore, JsonIgnore]
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
        /// Número utilizado para ordenar o documento durante a exibição ao(s) assinante(s).
        /// </summary>
        [XmlAttribute]
        public int Ordenacao { get; set; }

        /// <summary>
        /// Relação de assinaturas associadas ao documento.
        /// </summary>
        public List<Assinatura> Assinaturas { get; set; }
    }
}