// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;

namespace BITSIGN.Proxy.Logging
{
    [DebuggerDisplay("{Id}")]
    internal class Escopo : ILogger
    {
        private readonly ILogger logger;

        internal Escopo(ILogger logger)
        {
            this.logger = logger;
            this.Habilitado = logger != null;
        }

        public void Escrever(Severidade severidade, string mensagem)
        {
            if (this.Habilitado)
                this.logger?.Escrever(severidade, $"{this.Id} - {mensagem}");
        }

        public void Escrever(Exception excecao)
        {
            if (this.Habilitado)
                this.Escrever(Severidade.Excecao, excecao.ToString());
        }

        internal Escopo Iniciar()
        {
            this.Id = Guid.NewGuid();
            this.Escrever(Severidade.Info, "INÍCIO DO ESCOPO");

            return this;
        }

        internal void Encerrar()
        {
            this.Escrever(Severidade.Info, "FIM DO ESCOPO");
            this.QuebrarLinha();
            this.Flush();

            this.Id = Guid.Empty;
        }

        public void QuebrarLinha() =>
            this.logger?.QuebrarLinha();

        public void Flush() => 
            this.logger?.Flush();

        public void Dispose()
        {
            this.Encerrar();
            this.logger?.Dispose();
        }

        public Guid Id { get; private set; }

        public bool Habilitado { get; }
    }
}