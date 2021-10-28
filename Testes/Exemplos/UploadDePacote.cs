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
    public class UploadDePacote : Exemplo
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
                            NomeDoArquivo = "ContratoDeLocacao1.pdf",
                            Descricao = "Contrato de Locação 1",
                            Tipo = Documento.Tipos.Contrato,
                            Tags = "contratoId=123",
                            FormatoDoArquivo = "PDF",
                            ConteudoOriginal = File.ReadAllBytes("Exemplo/ContratoDeLocacao1.pdf"),
                            TamanhoDoArquivo = File.ReadAllBytes("Exemplo/ContratoDeLocacao1.pdf").Length,
                            PadraoDeAssinatura = Constantes.PadroesDeAssinatura.CAdES,
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
                                                Nome = "Jack Bauer",
                                                Documento = "57863748070",
                                                Email = "jack.bauer@ctu.com"
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
            }
        }
    }
}