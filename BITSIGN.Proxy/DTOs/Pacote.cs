// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
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
        private const string ManifestoXml = "Manifesto.xml";
        private const string ManifestoJson = "Manifesto.json";

        private static readonly string[] Manifestos = new[] { ManifestoXml, ManifestoJson };

        internal Pacote(byte[] dados)
        {
            this.Vazio = (dados?.Length ?? 0) == 0;

            if (!this.Vazio)
                Deserializar(dados);
        }

        /// <summary>
        /// Inicia o pacote com o lote para ser enviado ao serviço.
        /// </summary>
        /// <param name="lote">Lote com os documentos originais para envio e geração de assinaturas.</param>
        /// <param name="formatoDeSerializacao">Formato em que será serializado o manifesto.</param>
        /// <remarks>Aqueles documentos que estiverem com a propriedade <see cref="Documento.ConteudoOriginal"/> preenchida serão embutidos no pacote; alternativamente, informe os dados do arquivo à ser assinado na propriedade <see cref="Documento.Download"/>, e isso indicará ao serviço que deverá realizar o download ao invés de procurar pelo arquivo fisicamente dentro do pacote.</remarks>
        public Pacote(Lote lote, FormatoDeSerializacao formatoDeSerializacao = FormatoDeSerializacao.Json)
        {
            this.Lote = lote;
            this.Arquivos =
                Enumerable.Concat(
                    new[] { (formatoDeSerializacao == FormatoDeSerializacao.Json ? ManifestoJson : ManifestoXml, Serializador.Serializar(lote, formatoDeSerializacao, "Lote").EmBytes()) },
                    lote.Documentos.Where(d => d.ConteudoOriginal?.Length > 0).Select(d => (d.NomeDoArquivo, d.ConteudoOriginal)));
        }

        private void Deserializar(byte[] dados)
        {
            var zip = Compactador.Descompactar(dados);
            var manifesto = zip.FirstOrDefault(arquivo => Manifestos.Contains(arquivo.nome, StringComparer.CurrentCultureIgnoreCase));
            var formatoDeSerializacao = string.Compare(manifesto.nome, ManifestoJson, true) == 0 ? FormatoDeSerializacao.Json : FormatoDeSerializacao.Xml;

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
            this.Conteudo = dados;
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
        /// Conteúdo utilizado para materializar/gerar os dados (<see cref="Lote"/> e os <see cref="Arquivos"/>).
        /// </summary>
        public byte[] Conteudo { get; private set; }

        /// <summary>
        /// Relação de arquivos contidos no lote.
        /// </summary>
        public IEnumerable<(string, byte[])> Arquivos { get; private set; }
    }
}