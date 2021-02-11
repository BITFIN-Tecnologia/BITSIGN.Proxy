// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public interface IExemplo
    {
        Task Executar(params string[] parametros);
    }
}