// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.Utilitarios
{
    internal static class Serializador
    {
        internal static class Json
        {
            private static readonly JsonSerializerOptions config = new()
            {
                WriteIndented = false,
                PropertyNameCaseInsensitive = true
            };

            internal static string Serializar(object objeto, bool identado = false) =>
                JsonSerializer.Serialize(objeto, objeto.GetType(), !identado ? config : new JsonSerializerOptions(config) { WriteIndented = true });

            internal static T Deserializar<T>(string conteudo) where T : class =>
                JsonSerializer.Deserialize<T>(conteudo, config);
        }

        internal static class Xml
        {
            private static readonly XmlWriterSettings config = new()
            {
                Encoding = new UTF8Encoding(false),
#if DEBUG
                Indent = true
#else
                Indent = identado
#endif
            };

            internal static string Serializar(object objeto, string elementoRaiz, bool identado = false)
            {
                var s = new XmlSerializer(objeto.GetType(), new XmlRootAttribute(elementoRaiz));

                using (var ms = new MemoryStream())
                {
                    using (var writer = XmlWriter.Create(ms, config))
                        s.Serialize(ms, objeto);

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }

            internal static T Deserializar<T>(string conteudo, string elementoRaiz) where T : class
            {
                var s = new XmlSerializer(typeof(T), new XmlRootAttribute(elementoRaiz));

                using (var ms = new MemoryStream(EmBytes(conteudo)))
                    return s.Deserialize(ms) as T;
            }
        }

        internal static string Serializar(object objeto, string formato, string elementoRaiz = null) =>
            formato.Contains(FormatoDeSerializacao.Json.ToString(), StringComparison.InvariantCultureIgnoreCase) ? Json.Serializar(objeto) : Xml.Serializar(objeto, elementoRaiz);

        internal static T Deserializar<T>(string conteudo, string formato, string elementoRaiz = null) where T : class =>
            formato.Contains(FormatoDeSerializacao.Json.ToString(), StringComparison.InvariantCultureIgnoreCase) ? Json.Deserializar<T>(conteudo) : Xml.Deserializar<T>(conteudo, elementoRaiz);

        internal static byte[] EmBytes(this string conteudo) => Encoding.UTF8.GetBytes(conteudo);
    }
}