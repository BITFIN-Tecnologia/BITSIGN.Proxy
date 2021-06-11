// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Classe utilizada para recepcionar os callbacks gerados pela plataforma.
    /// </summary>
    [DebuggerDisplay("{Evento} - Status: {Status}")]
    public class Callback
    {
        /// <summary>
        /// Nome do evento indicando de qual entidade se refere o callback.
        /// </summary>
        public string Evento { get; set; }

        /// <summary>
        /// Identificador da entidade que gerou o callback.
        /// </summary>
        /// <remarks>Na maioria das vezes será um <see cref="Guid"/>; porém quando a propriedade <see cref="Evento"/> se tratar de um "Assinante", o <see cref="Id"/> será o número de seu documento (CNPJ/CPF).</remarks>
        public string Id { get; set; }

        /// <summary>
        /// Status que foi atribuído à entidade.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Data em que o callback ocorreu.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Alguma informação complementar relevante para este evento.
        /// </summary>
        /// <remarks>Dependendo da complexidade do callback, esta propriedade pode retornar um objeto serializado para complementar a informação.</remarks>
        public string Complemento { get; set; }

        /// <summary>
        /// Se a entidade que gerou o callback possuir tags associadas, elas serão informadas nesta propriedade.
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Identificador da aplicação à qual o callback está relacionado.
        /// </summary>
        public Guid AplicacaoId { get; set; }
    }
}