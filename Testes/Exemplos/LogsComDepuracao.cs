// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.Comunicacao;
using BITSIGN.Proxy.DTOs;
using BITSIGN.Proxy.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class LogsComDepuracao : Exemplo
    {
        public override async Task Executar(params string[] parametros)
        {
            var arquivo = File.ReadAllBytes("Exemplo/ContratoDeLocacao.pdf");

            //Gera, para cada requisição, um Guid para correlacionar os logs do cliente com o do serviço.
            var geradorDeRastreio = new RastreioComGuid();

            //Contexto do log. O conteúdo é descarregado na conclusão do bloco using.
            using (var loggerEmArquivo = new LogEmTexto(new StreamWriter("Log.txt", true)))
            {
                using (var proxy = new ProxyDoServico(this.Conexao, loggerEmArquivo, geradorDeRastreio))
                {
                    var pacote = new Pacote(new Lote()
                    {
                        Contratante = new Contratante()
                        {
                            Id = this.CodigoDoContratante,
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

                    await proxy.Lotes.Upload(pacote);
                }
            }

            Console.WriteLine(File.ReadAllText("Log.txt"));
        }
    }
}