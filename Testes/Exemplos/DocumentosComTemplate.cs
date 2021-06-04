// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class DocumentosComTemplate : Exemplo
    {
        private const string Arquivo = @"Id;Numero;Tipo;Valor;Emissao;Vencimento;DataDeDesconto;ValorDeDesconto;NF;NomeDoCedente;DocumentoDoCedente;IEDoCedente;LogrdouroDoCedente;BairroDoCedente;LocalidadeDoCedente;UFDoCedente;CepDoCedente;NomeDoSacado;DocumentoDoSacado;IEDoSacado;LogradouroDoSacado;BairroDoSacado;LocalidadeDoSacado;UFDoSacado;CepDoSacado
9282;9080/1;DM;10.293,22;04/06/2021;04/07/2021;01/07/2021;200,00;9080;Nome da Empresa Ltda.;20987222000190;1234567;Rua São João, 29;Jd. do Lago;Louveira;SP;13000123;Jack Bauer;22233344400;7654321;Rua da Liberdade, 94;Centro;São Paulo;SP;01001000;
9282;9080/2;DM;10.293,22;04/06/2021;04/08/2021;01/08/2021;200,00;9080;Nome da Empresa Ltda.;20987222000190;1234567;Rua São João, 29;Jd. do Lago;Louveira;SP;13000123;Jack Bauer;22233344400;7654321;Rua da Liberdade, 94;Centro;São Paulo;SP;01001000";

        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            var arquivo = Encoding.UTF8.GetBytes(Arquivo);

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
                            NomeDoArquivo = "Duplicatas.csv",
                            Descricao = "Relação de Duplicatas",
                            Tipo = "Duplicata",
                            FormatoDoArquivo = "CSV",
                            ConteudoOriginal = arquivo,
                            TamanhoDoArquivo = arquivo.Length,
                            PadraoDeAssinatura = "CAdES",
                            PoliticaDeAssinatura = "PA_AD_RB_v2_3",
                            Template = "Duplicata",
                            Assinaturas = new List<Assinatura>()
                            {
                                new()
                                {
                                    Perfil = "Representante",
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
                    Tags = "operacao=123"
                });

                var urlDoLote = await proxy.Lotes.Upload(pacote, cancellationToken);

                Console.WriteLine(urlDoLote);
                Console.WriteLine(pacote.Lote.Id);
                Console.WriteLine(pacote.Lote.UrlAoVivo);

                //Deverá retornar a mesma quantidade que está no arquivo CSV, que neste caso, são 2.
                Console.WriteLine(pacote.Lote.QtdeDeDocumentos);
            }
        }
    }
}