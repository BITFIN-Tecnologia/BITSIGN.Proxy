// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Collections.Generic;
using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Empresa ou Pessoa Física que possui contrato para assinaturas digitais.
    /// </summary>
    [DebuggerDisplay("{Entidade.Nome,nq}")]
    public class Contratante : Base
    {
        /// <summary>
        /// Entidade associada ao contratante.
        /// </summary>
        public Entidade Entidade { get; set; }

        /// <summary>
        /// Configurações do processo operacional das assinaturas e de integração.
        /// </summary>
        public Configuracao Configuracao { get; set; }

        /// <summary>
        /// Observadores globais adicionados ao contratante.
        /// </summary>
        public List<Observador> Observadores { get; set; }
    }
}