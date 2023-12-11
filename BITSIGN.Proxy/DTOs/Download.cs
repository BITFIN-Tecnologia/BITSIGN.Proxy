// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Diagnostics;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Parâmetros para download de arquivo.
    /// </summary>
    [DebuggerDisplay("Url: {Url,nq}")]
    public class Download
    {
        /// <summary>
        /// Endereço HTTP de onde o arquivo deverá ser baixado (através do método GET). Opcionalmente, poderá utilizar a API de Uploads.
        /// </summary>
        [XmlAttribute]
        public string Url { get; set; }

        /// <summary>
        /// Dicionário com cabeçalhos necessários para realizar a requisição.
        /// </summary>
        /// <remarks>O formato deverá ser: "chave:valor". Se houver múltiplos itens, eles deverão ser separados por ";", exemplo: "chave1:valor1;chave2:valor2".</remarks>
        [XmlAttribute]
        public string Headers { get; set; }
    }
}