// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.Comunicacao;
using BITSIGN.Proxy.DTOs;
using BITSIGN.Proxy.Logging;
using BITSIGN.Proxy.Utilitarios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class LogsComDepuracao : IExemplo
    {
        public async Task Executar(params string[] parametros)
        {
            var codigoDoContratante = new Guid("985e0702-e94a-4954-b7a8-1f28c73c8122");
            var arquivo = File.ReadAllBytes("Exemplo/ContratoDeLocacao.pdf");

            using (var loggerEmArquivo = new LogEmTexto(new StreamWriter("Log.txt")))
            {
                using (var proxy = new ProxyDoServico(
                    new Conexao(
                        Ambiente.Sandbox,
                        codigoDoContratante,
                        new Guid("5a83a804-1416-476f-8c78-d4d1b8d33fe4"),
                        FormatoDeSerializacao.Json),
                    loggerEmArquivo,
                    new RastreioComGuid()))
                {
                    var pacote = new Pacote(new Lote()
                    {
                        Contratante = new Contratante()
                        {
                            Id = codigoDoContratante,
                            Entidade = new Entidade()
                            {
                                Nome = "White House - USA",
                                Documento = "016338212000113"
                            }
                        },
                        Entidade = new Entidade()
                        {
                            Nome = "White House - USA",
                            Documento = "016338212000113"
                        },
                        Documentos = new List<Documento>()
                        {
                            new Documento()
                            {
                                NomeDoArquivo = "ContratoDeLocacao.pdf",
                                Descricao = "Contrato de Locação",
                                Tipo = "Contrato",
                                Tags = "contratoId=123",
                                FormatoDoArquivo = "PDF",
                                ConteudoOriginal = arquivo,
                                TamanhoDoArquivo = arquivo.Length,
                                PadraoDeAssinatura = "CAdES",
                                PoliticaDeAssinatura = "PA_AD_RB_v2_3",
                                Assinaturas = new List<Assinatura>()
                                {
                                    new Assinatura()
                                    {
                                        Perfil = "Locador",
                                        QtdeMinima = 1,
                                        Assinantes = new List<Assinante>()
                                        {
                                            new Assinante()
                                            {
                                                Entidade = new Entidade()
                                                {
                                                    Nome = "Jack Bauer",
                                                    Documento = "57863748070",
                                                    Email = "jack.bauer@uct.com"
                                                },
                                                Notificar = true,
                                                Obrigatorio = false
                                            },
                                            new Assinante()
                                            {
                                                Entidade = new Entidade()
                                                {
                                                    Nome = "Nina Myers",
                                                    Documento = "88488048025",
                                                    Email = "nina.myers@uct.com"
                                                },
                                                Notificar = true,
                                                Obrigatorio = false
                                            }
                                        }
                                    },
                                    new Assinatura()
                                    {
                                        Perfil = "Locatário",
                                        QtdeMinima = 1,
                                        Assinantes = new List<Assinante>()
                                        {
                                            new Assinante()
                                            {
                                                Entidade = new Entidade()
                                                {
                                                    Nome = "Joe Biden",
                                                    Documento = "94478520097",
                                                    Email = "potus@whitehouse.com"
                                                },
                                                Notificar = true,
                                                Obrigatorio = true
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        Tags = "processo=456"
                    });

                    var urlDoLote = await proxy.Lotes.Upload(pacote);

                    Console.WriteLine(pacote.Lote.Id);
                    Console.WriteLine(pacote.Lote.UrlAoVivo);
                }
            }
        }
    }
}