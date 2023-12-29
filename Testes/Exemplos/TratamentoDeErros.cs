// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.Comunicacao;
using BITSIGN.Proxy.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class TratamentoDeErros : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                var pacote = new Pacote(new Lote()
                {
                    Aplicacao = new()
                    {
                        Id = this.CodigoDaAplicacao,
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
                            Descricao = "Cxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxontrato de Locação 1",
                            Tipo = Documento.Tipos.Contrato,
                            Tags = "contratoId=123",
                            FormatoDoArquivo = "PDF",
                            ConteudoOriginal = File.ReadAllBytes("Exemplo/ContratoDeLocacao1.pdf"),
                            TamanhoDoArquivo = File.ReadAllBytes("Exemplo/ContratoDeLocacao1.pdf").Length,
                            PadraoDeAssinatura = Constantes.PadroesDeAssinatura.CAdES,
                            PoliticaDeAssinatura = "PA_AD_RB_v2_3",
                            AssinaturaAnexada = true,
                            Assinaturas = new List<Assinatura>()
                            {
                                new()
                                {
                                    Perfil = "Locador",
                                    QtdeMinima = 1,
                                    Assinantes = new List<Assinante>()
                                    {
                                        new ()
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
                    }
                });

                try
                {
                    var urlDoLote = await proxy.Lotes.Upload(pacote, cancellationToken);
                }
                catch (ErroNaRequisicao ex)
                {
                    Console.WriteLine($"Mensagem: {ex.Message}");
                    Console.WriteLine($"Link: {ex.HelpLink}");

                    foreach (var chave in ex.Data.Keys)
                        Console.WriteLine($"{chave}: {ex.Data[chave]}");
                }
            }
        }
    }
}