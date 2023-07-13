// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class EmissaoDeCertificados : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                var senha = "P@$$w0rd";

                var certificado = await proxy.Certificados.Emitir(new()
                {
                    DataDeValidade = DateTime.Now.AddMonths(5),
                    DocumentoDoProprietario = "11122233344",
                    NomeDoProprietario = "Jack Bauer",
                    EmailDoProprietario = "jack.bauer@ctu.com",
                    OrganizacaoId = Guid.NewGuid(),
                    Senha = senha
                }, cancellationToken);

                var x509 = new X509Certificate2(Convert.FromBase64String(certificado), senha, X509KeyStorageFlags.EphemeralKeySet);

                File.WriteAllBytes(@"c:\temp\JackBauer.pfx", x509.Export(X509ContentType.Pfx, "SENHA_FORTE"));
            }
        }
    }
}