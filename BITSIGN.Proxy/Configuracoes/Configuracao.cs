// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Collections.Generic;
using System.Linq;

namespace BITSIGN.Proxy.Configuracoes
{
    /// <summary>
    /// Base para arquivos de configuração.
    /// </summary>
    public abstract class Configuracao
    {
        /// <summary>
        /// Retorna a conexão correspondente ao seu nome (<see cref="Conexao.Nome"/>).
        /// </summary>
        /// <param name="nome">Nome da aplicação.</param>
        /// <returns>Objeto <see cref="Proxy.Conexao"/> correspodente.</returns>
        /// <exception cref="ArgumentNullException">Caso o nome da conexão seja nulo ou vazio.</exception>
        public Conexao Conexao(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentNullException(nameof(nome));

            return this.Conexoes?.FirstOrDefault(c => c.Nome == nome);
        }

        /// <summary>
        /// Relação de conexões configuradas.
        /// </summary>
        public IEnumerable<Conexao> Conexoes { get; protected set; }
    }
}