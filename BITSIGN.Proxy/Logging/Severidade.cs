// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

namespace BITSIGN.Proxy.Logging
{
    /// <summary>
    /// Opções para categorização dos logs.
    /// </summary>
    public enum Severidade
    {
        /// <summary>
        /// Mensagem meramente informativa.
        /// </summary>
        Info,
        /// <summary>
        /// Alerta de que algo não está como esperado, mas que não prejudica a execução.
        /// </summary>
        Alerta,
        /// <summary>
        /// Algum erro previsto ocorreu durante a execução.
        /// </summary>
        Erro,
        /// <summary>
        /// Uma falha não prevista ocorreu durante a execução.
        /// </summary>
        Excecao
    }
}