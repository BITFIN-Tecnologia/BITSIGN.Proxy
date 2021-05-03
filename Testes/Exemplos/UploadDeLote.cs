// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class UploadDeLote : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            //Arquivo a ser enviado para coleta de assinatura(s).
            //var arquivo = File.ReadAllBytes("Exemplo/ContratoDeLocacao.pdf");

            //Criação do proxy de comunicação com o serviço.
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                var pacote = new Pacote(new()
                {
                    Contratante = new()
                    {
                        Id = this.CodigoDoContratante,
                        Entidade = new()
                        {
                            Nome = "White House - USA",
                            Documento = "031508560000185",
                            Email = "contact@whitehouse.com"
                        }
                    },
                    Entidade = new()
                    {
                        Nome = "White House - USA",
                        Documento = "014175773000113",
                        Email = "contact@whitehouse.com"
                    },
                    DataDeExpiracao = DateTime.Now.AddDays(10),
                    Documentos = new List<Documento>()
                    {
                        new Documento()
                        {
                            NomeDoArquivo = "ContratoDeLocacao1.pdf",
                            Descricao = "Contrato de Locação 1",
                            Tipo = "Contrato",
                            Tags = "contratoId=123",
                            FormatoDoArquivo = "PDF",
                            ConteudoOriginal = File.ReadAllBytes("Exemplo/ContratoDeLocacao1.pdf"),
                            TamanhoDoArquivo = File.ReadAllBytes("Exemplo/ContratoDeLocacao1.pdf").Length,
                            PadraoDeAssinatura = "CAdES",
                            PoliticaDeAssinatura = "PA_AD_RB_v2_3",
                            Assinaturas = new List<Assinatura>()
                            {
                                new()
                                {
                                    Perfil = "Locador",
                                    QtdeMinima = 1,
                                    Assinantes = new List<Assinante>()
                                    {
                                        new()
                                        {
                                            Entidade = new()
                                            {
                                                Nome = "Israel Aece",
                                                Documento = "28387365823",
                                                Email = "israelaece@yahoo.com.br"
                                            },
                                            Notificar = true,
                                            Obrigatorio = false
                                        }
                                    }
                                }
                            }
                        },
                        new Documento()
                        {
                            NomeDoArquivo = "ContratoDeLocacao2.pdf",
                            Descricao = "Contrato de Locação 2",
                            Tipo = "Contrato",
                            Tags = "contratoId=123",
                            FormatoDoArquivo = "PDF",
                            ConteudoOriginal = File.ReadAllBytes("Exemplo/ContratoDeLocacao2.pdf"),
                            TamanhoDoArquivo = File.ReadAllBytes("Exemplo/ContratoDeLocacao2.pdf").Length,
                            PadraoDeAssinatura = "CAdES",
                            PoliticaDeAssinatura = "PA_AD_RB_v2_3",
                            Assinaturas = new List<Assinatura>()
                            {
                                new()
                                {
                                    Perfil = "Locador",
                                    QtdeMinima = 1,
                                    Assinantes = new List<Assinante>()
                                    {
                                        new()
                                        {
                                            Entidade = new()
                                            {
                                                Nome = "Israel Aece",
                                                Documento = "28387365823",
                                                Email = "israelaece@yahoo.com.br"
                                            },
                                            Notificar = true,
                                            Obrigatorio = false
                                        }
                                    }
                                }
                            }
                        },
                        new Documento()
                        {
                            NomeDoArquivo = "ContratoDeLocacao3.pdf",
                            Descricao = "Contrato de Locação 3",
                            Tipo = "Contrato",
                            Tags = "contratoId=123",
                            FormatoDoArquivo = "PDF",
                            ConteudoOriginal = File.ReadAllBytes("Exemplo/ContratoDeLocacao3.pdf"),
                            TamanhoDoArquivo = File.ReadAllBytes("Exemplo/ContratoDeLocacao3.pdf").Length,
                            PadraoDeAssinatura = "CAdES",
                            PoliticaDeAssinatura = "PA_AD_RB_v2_3",
                            Assinaturas = new List<Assinatura>()
                            {
                                new()
                                {
                                    Perfil = "Locador",
                                    QtdeMinima = 1,
                                    Assinantes = new List<Assinante>()
                                    {
                                        new()
                                        {
                                            Entidade = new()
                                            {
                                                Nome = "Israel Aece",
                                                Documento = "28387365823",
                                                Email = "israelaece@yahoo.com.br"
                                            },
                                            Notificar = true,
                                            Obrigatorio = false
                                        }
                                    }
                                }
                            }
                        }
                    },
                    Observadores = new List<Observador>()
                    {
                        new() { Email = "teste@xpto.com.br" },
                        new() { Email = "xpto@xpto.com.br" }
                    },
                    Anexos = new List<Anexo>()
                    {
                        new() { NomeDoArquivo = "Instrucoes.txt", Conteudo = File.ReadAllBytes("Exemplo/Instrucoes.txt"), Descricao = "Descrição sobre o processo." }
                    },
                    Tags = "processo=456"
                });

                var urlDoLote = await proxy.Lotes.Upload(pacote, cancellationToken);

                Console.WriteLine(urlDoLote);
                Console.WriteLine(pacote.Lote.Id);
                Console.WriteLine(pacote.Lote.UrlAoVivo);
            }
        }
    }
}