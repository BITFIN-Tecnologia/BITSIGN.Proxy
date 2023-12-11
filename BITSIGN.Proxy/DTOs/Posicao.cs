// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Informações complementares para os padrões de assinatura PAdES ou XAdES.
    /// </summary>
    public class Posicao
    {
        /// <summary>
        /// Construtor útil para o processo de (de)serialização.
        /// </summary>
        protected Posicao() { }

        /// <summary>
        /// Assinatura em arquivo XML no padrão XAdES.
        /// </summary>
        /// <param name="estilo">Estilo do encapsulamento da assinatura XML (Enveloped ou Enveloping).</param>
        /// <param name="tag">Nome do elemento no XML que servirá de referência para a assinatura.</param>
        public Posicao(string estilo, string tag)
        {
            this.Estilo = estilo;
            this.Tag = tag;
        }

        /// <summary>
        /// Assinatura em arquivo PDF no padrão PAdES.
        /// </summary>
        /// <param name="pagina">Número da página do PDF onde a assinatura deverá ser estampada.</param>
        /// <param name="x">Posição no eixo X (horizontal). A referência é rodapé/esquerda.</param>
        /// <param name="y">Posição no eixo Y (vertical). A referência é rodapé/esquerda.</param>
        public Posicao(int pagina, float x, float y)
        {
            this.Pagina = pagina;
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Número da página do PDF onde a assinatura deverá ser estampada.
        /// </summary>
        [XmlAttribute]
        public int Pagina { get; set; }

        /// <summary>
        /// Posição no eixo X (horizontal). A referência é rodapé/esquerda.
        /// </summary>
        [XmlAttribute]
        public float X { get; set; }

        /// <summary>
        /// Posição no eixo Y (vertical). A referência é rodapé/esquerda.
        /// </summary>
        [XmlAttribute]
        public float Y { get; set; }

        /// <summary>
        /// Estilo do encapsulamento da assinatura no XML (Enveloped ou Enveloping).
        /// </summary>
        [XmlAttribute]
        public string Estilo { get; set; }

        /// <summary>
        /// Elemento no XML que servirá de referência para a assinatura.
        /// </summary>
        [XmlAttribute]
        public string Tag { get; set; }

        /// <summary>
        /// Representação do objeto.
        /// </summary>
        /// <returns><see cref="String"/> com as propriedades formatadas de acordo com a finalidade.</returns>
        public override string ToString() =>
            string.IsNullOrWhiteSpace(Tag) ?
                $"[PDF]: Página: {Pagina} - Eixos (x,y): ({X},{Y})" :
                $"[XML]: Estilo: {Estilo} - Tag: {Tag}";
    }
}