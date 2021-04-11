// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

namespace BITSIGN.Proxy
{
    /// <summary>
    /// Recursos e parâmetros utilizados para a comunicação com os serviços.
    /// </summary>
    public class Protocolo
    {
        /// <summary>
        /// Identifica o código do contratante.
        /// </summary>
        public const string CodigoDoContratante = "BS-Contratante";

        /// <summary>
        /// Identifica a chave de integração (token) gerado para o contrantante.
        /// </summary>
        public const string ChaveDeIntegracao = "BS-Token";

        /// <summary>
        /// Identifica o código utilizado para rastrear as requisições.
        /// </summary>
        public const string CodigoDeRastreio = "BS-CodigoDeRastreio";
    }
}