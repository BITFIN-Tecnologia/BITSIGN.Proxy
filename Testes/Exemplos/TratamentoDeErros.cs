// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
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
                var pacote = new Pacote(new()
                {
                    Contratante = new()
                    {
                        Id = this.CodigoDoContratante,
                        Entidade = new()
                        {
                            Nome = "White House - USA",
                            Documento = "014175773000113",
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
                            Descricao = "Cxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxontrato de Locação 1",
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
                                        new ()
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