// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;

namespace BITSIGN.Proxy.Configuracoes
{
    /// <summary>
    /// Especificação para implementação para extração e uso das configurações.
    /// </summary>
    public interface IConfiguracao
    {
        /// <summary>
        /// Ambiente de Sandbox, Produção ou Local.
        /// </summary>
        Ambiente Ambiente { get; }

        /// <summary>
        /// Endereço base (HTTP) das APIs. Somente é utilizado quando a solução estiver hospedada localmente.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Endpoint que resume o status atual dos serviços e seus recursos. Somente é utilizado quando a solução estiver hospedada localmente.
        /// </summary>
        string Status { get; }

        /// <summary>
        /// Versão das APIs.
        /// </summary>
        string Versao { get; }

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