// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class ValidandoCertificado : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(this.Conexao))
            {
                var pfx = File.ReadAllBytes(@"c:\temp\certificado.pfx");

                var resultado = await proxy.Certificados.Validar(new()
                {
                    Certificado = Convert.ToBase64String(pfx),
                    DataDeReferencia = DateTime.Now,
                    Senha = "P@$$w0rd",
                    ValidaChavePrivada = true
                }, cancellationToken);

                if (resultado != null)
                {
                    Console.WriteLine($"Válido: {resultado.Valido}");
                    Console.WriteLine($"Nome: {resultado.Certificado.NomeDoProprietario}");
                    Console.WriteLine($"Documento: {resultado.Certificado.DocumentoDoProprietario}");
                }
            }
        }
    }
}