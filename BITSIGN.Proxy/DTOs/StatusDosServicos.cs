﻿// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Collections.Generic;
using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Relatório com o status geral e de cada serviço.
    /// </summary>
    [DebuggerDisplay("{Status,nq}")]
    public class StatusDosServicos
    {
        /// <summary>
        /// Status geral de todos os serviços
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Tempo de duração geral da análise em milissegundos.
        /// </summary>
        public string Duracao { get; set; }

        /// <summary>
        /// Relação de serviços com seus respectivos status.
        /// </summary>
        public IEnumerable<Servico> Servicos { get; set; }
        
        /// <summary>
        /// Detalha o status atual de um determinado serviço.
        /// </summary>
        [DebuggerDisplay("{Nome,nq}: {Status,nq}")]
        public class Servico
        {
            /// <summary>
            /// Nome do serviço analisado.
            /// </summary>
            public string Nome { get; set; }

            /// <summary>
            /// Mensagem gerada pela análise do serviço.
            /// </summary>
            public string Mensagem { get; set; }

            /// <summary>
            /// Status atual do serviço.
            /// </summary>
            public string Status { get; set; }

            /// <summary>
            /// Tempo de duração para análise do serviço em milissegundos.
            /// </summary>
            public string Duracao { get; set; }

            /// <summary>
            /// Informações complementares sobre os recursos dependentes associados ao serviço.
            /// </summary>
            public IDictionary<string, string> Informacoes { get; set; }
        }
    }
}