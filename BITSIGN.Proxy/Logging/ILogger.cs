// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;

namespace BITSIGN.Proxy.Logging
{
    /// <summary>
    /// Definição para a criação e armazenamento de logs de execução.
    /// </summary>
    public interface ILogger : IDisposable
    {
        /// <summary>
        /// Permite incluir uma mensagem e sua respectiva severidade.
        /// </summary>
        /// <param name="severidade">Severidade da mensagem.</param>
        /// <param name="mensagem">Mensagem a ser armazenada.</param>
        void Escrever(Severidade severidade, string mensagem);

        /// <summary>
        /// Informa que uma exceção ocorreu.
        /// </summary>
        /// <param name="excecao">Dados da exceção ocorrida.</param>
        void Escrever(Exception excecao);

        /// <summary>
        /// Inclui uma quebra de linha.
        /// </summary>
        void QuebrarLinha();

        /// <summary>
        /// Efetiva fisicamente a gravação dos logs.
        /// </summary>
        void Flush();
    }
}