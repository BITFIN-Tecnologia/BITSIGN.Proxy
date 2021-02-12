// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

namespace BITSIGN.Proxy.Logging
{
    /// <summary>
    /// Define a estrutura para geração de códigos de rastreio.
    /// </summary>
    /// <remarks>O rastreio de requisições é útil para correlacionar as requisições com os logs armazenados pelos serviços.</remarks>
    public interface IRastreio
    {
        /// <summary>
        /// Método que gera o código de rastreio.
        /// </summary>
        /// <returns>Valor aleatório, que identifica unicamente a requisição.</returns>
        string Gerar();
    }
}