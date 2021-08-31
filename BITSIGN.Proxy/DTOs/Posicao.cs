// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Define a posição no documento PDF onde a assinatura será estampada (padrão PAdES).
    /// </summary>
    public class Posicao
    {
        /// <summary>
        /// Número da página onde a assinatura deve ser colocada.
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
    }
}