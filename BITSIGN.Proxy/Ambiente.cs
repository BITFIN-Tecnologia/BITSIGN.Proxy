// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

namespace BITSIGN.Proxy
{
    /// <summary>
    /// Tipos de ambientes disponíveis para consumo dos serviços.
    /// </summary>
    public enum Ambiente
    {
        /// <summary>
        /// Ambiente destinado exclusivamente para testes de implementação e integração.
        /// </summary>
        /// <remarks>O ambiente de Sandbox é um "espelho" do ambiente de Produção, com exceção de que as assinaturas realizadas não são efetivamente válidas.</remarks>
        Sandbox = 0,
        /// <summary>
        /// Esta opção indica que as requisições serão encaminhadas para ambiente de Produção, e estarão sujeitas à cobrança e aos encargos do plano contratado.
        /// </summary>
        Producao = 1,
        /// <summary>
        /// Utilizado quando a solução está hospedada localmente (on-premises).
        /// </summary>
        Local = 2
    }
}