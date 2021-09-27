// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Collections.Generic;
using System.IO;

namespace BITSIGN.Proxy.Logging
{
    /// <summary>
    /// Armazena os logs gerados em formato texto.
    /// </summary>
    public class LogEmTexto : ILogger
    {
        private readonly TextWriter escritor;
        private readonly List<string> mensagens;
        private readonly int qtdeDeMensagensParaFlush;

        /// <summary>
        /// Inicializa o log com armazenamento das mensagens no sistema de arquivos.
        /// </summary>
        /// <param name="escritor">Local onde serão armazenadas as mensagens de logs que forem geradas.</param>
        /// <param name="qtdeDeMensagensParaFlush">Quantidade de mensagens em buffer. Quando este valor for atingido, as mensagens serão escritas no arquivo. O padrão é <c>10</c>.</param>
        public LogEmTexto(TextWriter escritor, int qtdeDeMensagensParaFlush = 10)
        {
            this.escritor = escritor;
            this.mensagens = new(qtdeDeMensagensParaFlush);
            this.qtdeDeMensagensParaFlush = qtdeDeMensagensParaFlush;
        }

        /// <summary>
        /// Informa que uma exceção ocorreu.
        /// </summary>
        /// <param name="excecao">Dados da exceção ocorrida.</param>
        public void Escrever(Exception excecao) =>
            Escrever(Severidade.Excecao, excecao.ToString());

        /// <summary>
        /// Permite incluir uma mensagem e sua respectiva severidade.
        /// </summary>
        /// <param name="severidade">Severidade da mensagem.</param>
        /// <param name="mensagem">Mensagem a ser armazenada.</param>
        public void Escrever(Severidade severidade, string mensagem)
        {
            mensagens.Add($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {severidade,-8} - {mensagem.Replace(Environment.NewLine, " [CRLF] ")}");

            if (mensagens.Count >= qtdeDeMensagensParaFlush)
                this.Flush();
        }

        /// <summary>
        /// Inclui uma quebra de linha.
        /// </summary>
        public void QuebrarLinha() => this.mensagens.Add(string.Empty);

        /// <summary>
        /// Efetiva fisicamente a gravação dos logs.
        /// </summary>
        public void Flush()
        {
            foreach (var m in this.mensagens)
                this.escritor.WriteLine(m);

            this.mensagens.Clear();
            this.escritor.Flush();
        }

        /// <summary>
        /// Armazena eventuais mensagens que estejam no buffer, encerra e descarta os recursos utilizados.
        /// </summary>
        public void Dispose()
        {
            this.Flush();
            this.escritor.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}