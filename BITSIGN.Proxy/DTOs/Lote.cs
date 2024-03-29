﻿// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Define a relação de documentos enviados para geração de assinaturas digitais.
    /// </summary>
    [DebuggerDisplay("{Data} - {Aplicacao.Contratante.Entidade.Nome,nq}")]
    public class Lote : Base
    {
        /// <summary>
        /// Aplicação à qual o lote deverá ser inserido.
        /// </summary>
        public Aplicacao Aplicacao { get; set; }

        /// <summary>
        /// Modelo de assinatura que deve ser aplicada (D = Digital, E = Eletrônica).
        /// </summary>
        public string ModeloDeAssinatura { get; set; }

        /// <summary>
        /// Entidade associada ao lote.
        /// </summary>
        public Entidade Entidade { get; set; }

        /// <summary>
        /// Data de criação do lote.
        /// </summary>
        [XmlAttribute]
        public DateTime Data { get; set; }

        /// <summary>
        /// Data em que o lote será dado como expirado se não tiver suas assinaturas concluídas ou rejeitadas.
        /// </summary>
        [XmlAttribute]
        public DateTime DataDeExpiracao { get; set; }

        /// <summary>
        /// Tags utilizadas para correlacionar o lote ao sistema do contratante.
        /// </summary>
        [XmlAttribute]
        public string Tags { get; set; }

        /// <summary>
        /// Estrutura para organização em diretórios virtuais.
        /// </summary>
        public string Diretorio { get; set; }

        /// <summary>
        /// Totaliza a quantidade de documentos deste lote.
        /// </summary>
        [XmlAttribute]
        public int QtdeDeDocumentos { get; set; }

        /// <summary>
        /// Indica o status atual do lote.
        /// </summary>
        [XmlAttribute]
        public string Status { get; set; }

        /// <summary>
        /// Data em que o status foi definido.
        /// </summary>
        [XmlAttribute]
        public DateTime DataDoStatus { get; set; }

        /// <summary>
        /// URL para acesso ao vivo do andamento das assinaturas de todos os documentos do lote.
        /// </summary>
        [XmlAttribute]
        public string UrlAoVivo { get; set; }

        /// <summary>
        /// Detalhes da falha que ocorreu durante o processamento. Quando configurado, ela também é reportada via callbacks.
        /// </summary>
        [XmlAttribute]
        public string FalhaDeProcessamento { get; set; }

        /// <summary>
        /// Relação de documentos adicionados ao lote.
        /// </summary>
        public List<Documento> Documentos { get; set; }

        /// <summary>
        /// Relação de observadores que acompanham o processo de assinaturas.
        /// </summary>
        public List<Observador> Observadores { get; set; }

        /// <summary>
        /// Relação de arquivos anexados ao lote.
        /// </summary>
        public List<Anexo> Anexos { get; set; }
    }
}