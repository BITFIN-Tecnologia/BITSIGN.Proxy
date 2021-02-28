// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;

namespace BITSIGN.Proxy
{
    /// <summary>
    /// Especificação para implementação para extração e uso das configurações.
    /// </summary>
    public interface IConfiguracao
    {
        /// <summary>
        /// Ambiente de Sandbox ou Produção.
        /// </summary>
        Ambiente Ambiente { get; }

        /// <summary>
        /// Código do Contratante.
        /// </summary>
        Guid CodigoDoContratante { get; }

        /// <summary>
        /// Chave de Integração gerado para o contratante.
        /// </summary>
        string ChaveDeIntegracao { get; }

        /// <summary>
        /// Formato da serialização das mensagens trocadas com os serviços.
        /// </summary>
        FormatoDeSerializacao FormatoDeSerializacao { get; }

        /// <summary>
        /// Define o tempo máximo de espera permitido para executar uma requisição.
        /// </summary>
        TimeSpan Timeout { get; }
    }
}