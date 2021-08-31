// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Relação de constantes utilizadas pela biblioteca.
    /// </summary>
    public class Constantes
    {
        /// <summary>
        /// Padrões de assinaturas digitais suportadas.
        /// </summary>
        public class PadroesDeAssinatura
        {
            /// <summary>
            /// CMS Advanced Electronic Signatures.
            /// </summary>
            public const string CAdES = nameof(CAdES);

            /// <summary>
            /// PDF Advanced Electronic Signatures.
            /// </summary>
            public const string PAdES = nameof(PAdES);

            /// <summary>
            /// XML Advanced Electronic Signatures.
            /// </summary>
            public const string XAdES = nameof(XAdES);
        }
    }
}