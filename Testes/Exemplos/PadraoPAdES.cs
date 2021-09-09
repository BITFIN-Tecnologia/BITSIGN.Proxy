﻿// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
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
    public class PadraoPAdES : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            //Arquivo a ser enviado para coleta de assinatura(s).
            var arquivo = File.ReadAllBytes("Exemplo/Declaracao.pdf");

            var padraoDeAssinatura = Constantes.PadroesDeAssinatura.PAdES;

            //Posicionamento da assinatura no documento.
            var posicao = new Posicao()
            {
                Pagina = 1,
                X = 140,
                Y = 240
            };

            //Criação do proxy de comunicação com o serviço.
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                var pacote = new Pacote(new()
                {
                    Aplicacao = new()
                    {
                        Contratante = new()
                        {
                            Id = this.CodigoDoContratante,
                            Entidade = new()
                            {
                                Nome = "White House - USA",
                                Documento = "95038850000195",
                                Email = "contact@whitehouse.com"
                            }
                        }
                    },
                    Entidade = new()
                    {
                        Nome = "White House - USA",
                        Documento = "14175773000113",
                        Email = "contact@whitehouse.com"
                    },
                    DataDeExpiracao = DateTime.Now.AddDays(10),
                    Documentos = new List<Documento>()
                    {
                        new Documento()
                        {
                            NomeDoArquivo = "Declaracao.pdf",
                            Descricao = "Declaração",
                            Tipo = "Declaração",
                            Tags = "declaracaoId=123",
                            FormatoDoArquivo = "PDF",
                            ConteudoOriginal = arquivo,
                            TamanhoDoArquivo = arquivo.Length,
                            PadraoDeAssinatura = padraoDeAssinatura,
                            Assinaturas = new List<Assinatura>()
                            {
                                new()
                                {
                                    Perfil = "Declarante",
                                    QtdeMinima = 1,
                                    Assinantes = new List<Assinante>()
                                    {
                                        new()
                                        {
                                            Entidade = new()
                                            {
                                                Nome = "Jack Bauer",
                                                Documento = "57863748070",
                                                Email = "jack.bauer@ctu.com"
                                            },
                                            Notificar = true,
                                            Obrigatorio = false,
                                            Posicao = posicao
                                        }
                                    }
                                }
                            }
                        }
                    },
                    Tags = "processo=456"
                });

                var urlDoLote = await proxy.Lotes.Upload(pacote, cancellationToken);

                Console.WriteLine(urlDoLote);
            }
        }
    }
}