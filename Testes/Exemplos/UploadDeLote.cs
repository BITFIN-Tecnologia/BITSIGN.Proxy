// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class UploadDeLote : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            //Criação do proxy de comunicação com o serviço.
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                var lote = new Lote()
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
                    ModeloDeAssinatura = Constantes.ModelosDeAssinatura.Digital,
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
                            Download = new ()
                            {
                                Url = "http://localhost:33664/documentacao/BITSIGN-Instalacao-Web.pdf"
                            },
                            PadraoDeAssinatura = Constantes.PadroesDeAssinatura.CAdES,
                            PoliticaDeAssinatura = Constantes.Politicas.ICPBrasil.CAdES_PA_AD_RB_v2_3,
                            AssinaturaAnexada = true,
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
                                            Autenticacao = Constantes.TiposDeAutenticacao.CertificadoDigital,
                                            Notificar = true,
                                            Obrigatorio = false
                                        }
                                    }
                                }
                            }
                        }
                    },
                    Tags = "processo=456",
                    Diretorio = "formalizacao/contratos"
                };

                var info = await proxy.Lotes.Upload(lote, cancellationToken);

                Console.WriteLine($"Url: {info.Url}");
                Console.WriteLine($"Id:  {info.Id}");
            }
        }
    }
}