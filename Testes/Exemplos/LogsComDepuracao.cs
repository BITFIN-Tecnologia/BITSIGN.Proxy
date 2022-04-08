// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.DTOs;
using BITSIGN.Proxy.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class LogsComDepuracao : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            //Reinicializa o log.
            File.Delete("Log.txt");

            var arquivo = File.ReadAllBytes("Exemplo/ContratoDeLocacao1.pdf");

            //Gera, para cada requisição, um Guid para correlacionar os logs do cliente com o do serviço.
            var geradorDeRastreio = new RastreioComGuid();

            //Contexto do log. O conteúdo é descarregado na conclusão do bloco using.
            using (var loggerEmArquivo = new LogEmTexto(new StreamWriter("Log.txt", true)))
            {
                using (var proxy = new ProxyDoServico(this.Conexao, loggerEmArquivo, geradorDeRastreio))
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
                            ConteudoOriginal = arquivo,
                            TamanhoDoArquivo = arquivo.Length,
                            PadraoDeAssinatura = Constantes.PadroesDeAssinatura.CAdES,
                            PoliticaDeAssinatura = "PA_AD_RB_v2_3",
                            AssinaturaAnexada = true,
                            Assinaturas = new List<Assinatura>()
                            {
                                new ()
                                {
                                    Perfil = "Locador",
                                    QtdeMinima = 1,
                                    Assinantes = new List<Assinante>()
                                    {
                                        new ()
                                        {
                                            Entidade = new ()
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

                    await proxy.Lotes.Upload(pacote, cancellationToken);
                }
            }

            Console.WriteLine(File.ReadAllText("Log.txt"));
        }
    }
}