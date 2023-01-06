// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Classe que abstrai a complexidade para criação dos pacotes de lotes e documentos que são enviados e recebidos.
    /// </summary>
    public class Pacote
    {
        private const string ArquivoDeManifesto = "Manifesto.{0}";

        internal Pacote(byte[] dados, FormatoDeSerializacao formatoDeSerializacao)
        {
            this.Vazio = (dados?.Length ?? 0) == 0;

            if (!this.Vazio)
                Deserializar(dados, formatoDeSerializacao);
        }

        internal Pacote(Lote lote, FormatoDeSerializacao formatoDeSerializacao = FormatoDeSerializacao.Json)
        {
            this.Lote = lote;
            this.Arquivos =
                Enumerable.Concat(
                    new[] { (string.Format(ArquivoDeManifesto, formatoDeSerializacao.ToString().ToLower()), Serializador.EmBytes(Serializador.Serializar(lote, formatoDeSerializacao, "Lote"))) },
                    lote.Documentos.Where(d => d.ConteudoOriginal?.Length > 0).Select(d => (d.NomeDoArquivo, d.ConteudoOriginal)));
        }

        private void Deserializar(byte[] dados, FormatoDeSerializacao formatoDeSerializacao)
        {
            var zip = Compactador.Descompactar(dados);
            var manifesto = zip.FirstOrDefault(arquivo => string.Compare(arquivo.nome, string.Format(ArquivoDeManifesto, formatoDeSerializacao.ToString().ToLower()), true) == 0);

            var lote = Serializador.Deserializar<List<Lote>>(Encoding.UTF8.GetString(manifesto.conteudo), formatoDeSerializacao.ToString(), "Lotes").First();

            foreach (var documento in lote.Documentos)
            {
                var (nome, conteudo) = zip.FirstOrDefault(a => string.Compare(a.nome, documento.NomeDoArquivo, true) == 0);

                documento.ConteudoOriginal = zip.FirstOrDefault(a => string.Compare(a.nome, documento.NomeDoArquivo, true) == 0).conteudo;
                documento.ConteudoAssinado = zip.FirstOrDefault(a => string.Compare(a.nome, documento.NomeDoArquivoAssinado, true) == 0).conteudo;
                documento.ConteudoDoManifesto = zip.FirstOrDefault(a => string.Compare(a.nome, documento.NomeDoArquivoDeManifesto, true) == 0).conteudo;
                documento.ConteudoDoOrginalComManifesto = zip.FirstOrDefault(a => string.Compare(a.nome, documento.NomeDoArquivoOriginalComManifesto, true) == 0).conteudo;
            }

            this.Lote = lote;
            this.Arquivos = zip;
        }

        internal byte[] Serializar() =>
            Compactador.Compactar(
                this.Arquivos.Concat(
                    !(this.Lote.Anexos?.Any() ?? false) ? Enumerable.Empty<(string, byte[])>() : this.Lote.Anexos.Select(a => ($"anexos/{a.NomeDoArquivo}", a.Conteudo))));

        /// <summary>
        /// Indica se o pacote está ou não vazio.
        /// </summary>
        public bool Vazio { get; }

        /// <summary>
        /// Lote de documentos com seus respectivos arquivos.
        /// </summary>
        public Lote Lote { get; internal set; }

        /// <summary>
        /// Relação de arquivos contidos no lote.
        /// </summary>
        public IEnumerable<(string, byte[])> Arquivos { get; private set; }
    }
}