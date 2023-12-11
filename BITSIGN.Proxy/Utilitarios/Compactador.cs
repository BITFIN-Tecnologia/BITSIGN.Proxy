// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace BITSIGN.Proxy.Utilitarios
{
    internal static class Compactador
    {
        internal static byte[] Compactar(IEnumerable<(string nome, byte[] conteudo)> documentos)
        {
            using (var msZip = new MemoryStream())
            {
                using (var zip = new ZipArchive(msZip, ZipArchiveMode.Create, true))
                    foreach (var item in documentos)
                        using (var entry = zip.CreateEntry(item.nome, CompressionLevel.Fastest).Open())
                            entry.Write(item.conteudo, 0, item.conteudo.Length);

                return msZip.ToArray();
            }
        }

        internal static IEnumerable<(string nome, byte[] conteudo)> Descompactar(byte[] zipContent)
        {
            using (var msZip = new MemoryStream(zipContent))
            {
                using (var zip = new ZipArchive(msZip, ZipArchiveMode.Read))
                {
                    foreach (var item in zip.Entries)
                    {
                        using (var ms = new MemoryStream())
                        {
                            item.Open().CopyTo(ms);

                            yield return (item.FullName, ms.ToArray());
                        }
                    }
                }
            }
        }
    }
}