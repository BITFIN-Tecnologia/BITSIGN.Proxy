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

        /// <summary>
        /// Relação de políticas suportadas pela BITSIGN.
        /// </summary>
        public class Politicas
        {
            /// <summary>
            /// Relação de políticas vigentes da ICP-Brasil.
            /// </summary>
            /// <remarks>Para maiores informações, consulte a área exclusiva no <see href="https://www.gov.br/iti/pt-br/assuntos/repositorio/artefatos-de-assinatura-digital">ITI</see>.</remarks>
            public class ICPBrasil
            {
                /// <summary>
                /// <see href="http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_2.der">http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_2.der</see>
                /// </summary>
                public const string CAdES_PA_AD_RB_v2_2 = "PA_AD_RB_v2_2";
                /// <summary>
                /// <see href="http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_3.der">http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_3.der</see>
                /// </summary>
                public const string CAdES_PA_AD_RB_v2_3 = "PA_AD_RB_v2_3";
                /// <summary>
                /// <see href="http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_2.der">http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_2.der</see>
                /// </summary>
                public const string CAdES_PA_AD_RT_v2_2 = "PA_AD_RT_v2_2";
                /// <summary>
                /// <see href="http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_3.der">http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_3.der</see>
                /// </summary>
                public const string CAdES_PA_AD_RT_v2_3 = "PA_AD_RT_v2_3";
                /// <summary>
                /// <see href="http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RB_v1_0.der">http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RB_v1_0.der</see>
                /// </summary>
                public const string PAdES_PA_AD_RB_v1_0 = "PA_PAdES_AD_RB_v1_0";
                /// <summary>
                /// <see href="http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RB_v1_1.der">http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RB_v1_1.der</see>
                /// </summary>
                public const string PAdES_PA_AD_RB_v1_1 = "PA_PAdES_AD_RB_v1_1";
                /// <summary>
                /// <see href="http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RT_v1_0.der">http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RT_v1_0.der</see>
                /// </summary>
                public const string PAdES_PA_AD_RT_v1_0 = "PA_PAdES_AD_RT_v1_0";
                /// <summary>
                /// <see href="http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RT_v1_1.der">http://politicas.icpbrasil.gov.br/PA_PAdES_AD_RT_v1_1.der</see>
                /// </summary>
                public const string PAdES_PA_AD_RT_v1_1 = "PA_PAdES_AD_RT_v1_1";
                /// <summary>
                /// <see href="http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_3.xml">http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_3.xml</see>
                /// </summary>
                public const string XAdES_PA_AD_RB_v2_3 = "PA_AD_RB_v2_3";
                /// <summary>
                /// <see href="http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_4.xml">http://politicas.icpbrasil.gov.br/PA_AD_RB_v2_4.xml</see>
                /// </summary>
                public const string XAdES_PA_AD_RB_v2_4 = "PA_AD_RB_v2_4";
            }
        }

        /// <summary>
        /// Templates suportados para expansão de documentos.
        /// </summary>
        /// <remarks>
        /// Para maiores detalhes sobre esta funcionalidade, consulte: <see href="https://bitsign.com.br/documentacao#integracaoTemplates">https://bitsign.com.br/documentacao#integracaoTemplates</see>.
        /// </remarks>
        public class Templates
        {
            /// <summary>
            /// Modelo para emissão de duplicatas mercantis ou de serviço.
            /// </summary>
            public const string Duplicata = "BITSIGN.Duplicata";

            /// <summary>
            /// Extrai e assina os documentos do arquivo compactado.
            /// </summary>
            public const string Zip = "BITSIGN.Zip";
        }

        /// <summary>
        /// Estilos suportados para assinatura em arquivos XML utilizando o Padrão <see cref="PadroesDeAssinatura.XAdES"/>.
        /// </summary>
        public class EstilosXml
        {
            /// <summary>
            /// A assinatura é incluída como um subelemento do elemento que está sendo assinado.
            /// </summary>
            public const string Enveloped = "Enveloped";

            /// <summary>
            /// O elemento assinado é envolvido pela assinatura.
            /// </summary>
            public const string Enveloping = "Enveloping";
        }
    }
}