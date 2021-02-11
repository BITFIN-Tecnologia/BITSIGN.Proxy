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

namespace Testes
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var codigoDoContratante = new Guid("985E0702-E94A-4954-B7A8-1F28C73C8122");

            using (var proxy = new ProxyDoServico(
                new Conexao(
                    Ambiente.Sandbox,
                    codigoDoContratante,
                    new Guid("5A83A804-1416-476F-8C78-D4D1B8D33FE4"),
                    FormatoDeSerializacao.Json),
                new LogEmTexto(new StreamWriter("Log.txt")),
                new RastreioComGuid()))
            {
                var pacote = new Pacote(new Lote()
                {
                    Contratante = new Contratante()
                    {
                        Id = codigoDoContratante,
                        Entidade = new Entidade()
                        {
                            Nome = "Antares Securitizadora de Recebíveis Comerciais S/A",
                            Documento = "031508560000185"
                        }
                    },
                    Entidade = new Entidade()
                    {
                        Nome = "Antares Securitizadora de Recebíveis Comerciais S/A",
                        Documento = "031508560000185"
                    },
                    Documentos = new List<Documento>()
                    {
                        new Documento()
                        {
                            NomeDoArquivo = "Declaracao.pdf",
                            Descricao = "Declaração de Isenção",
                            Tipo = "Contrato",
                            Tags = "contratoId=123",
                            FormatoDoArquivo = "PDF",
                            ConteudoOriginal = File.ReadAllBytes("Exemplo/Declaracao.pdf"),
                            PadraoDeAssinatura = "CAdES",
                            PoliticaDeAssinatura = "PA_AD_RB_v2_3",
                            Assinaturas = new List<Assinatura>()
                            {
                                new Assinatura()
                                {
                                    Perfil = "Declarante",
                                    QtdeMinima = 1,
                                    Assinantes = new List<Assinante>()
                                    {
                                        new Assinante()
                                        {
                                            Entidade = new Entidade()
                                            {
                                                Nome = "Israel",
                                                Documento = "28387365823",
                                                Email = "israelaece@yahoo.com.br"
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

                var local = await proxy.Lotes.Upload(pacote);

                Console.WriteLine(pacote.Lote.Id);
                Console.WriteLine(pacote.Lote.UrlAoVivo);
            }
        }
    }
}