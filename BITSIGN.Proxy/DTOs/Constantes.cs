﻿// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Relação de constantes utilizadas pela biblioteca.
    /// </summary>
    public class Constantes
    {
        /// <summary>
        /// Idioma em que o assinador (Web ou Móvel) será apresentado.
        /// </summary>
        public class Idiomas
        {
            /// <summary>
            /// Português do Brasil.
            /// </summary>
            internal const string PortuguesDoBrasil = "pt-br";
        }

        /// <summary>
        /// Modelos de identificação e coleta de assinaturas.
        /// </summary>
        public class ModelosDeAssinatura
        {
            /// <summary>
            /// O assinante deverá apresentar o certificado digital para identificação e posterior assinatura.
            /// </summary>
            public const string Digital = "D";
            /// <summary>
            /// O assinante deverá confirmar seus dados para identificação e posterior assinatura.
            /// </summary>
            public const string Eletronica = "E";
        }

        /// <summary>
        /// Tipos de autenticação do assinante.
        /// </summary>
        public class TiposDeAutenticacao
        {
            /// <summary>
            /// Necessário a utilização de um certificado digital válido para assinatura.
            /// </summary>
            public const string CertificadoDigital = "Certificado";
            /// <summary>
            /// Token gerado e enviado por E-mail e que deverá ser informado para assinatura.
            /// </summary>
            public const string Email = "Token por E-mail";
            /// <summary>
            /// Token gerado e enviado por SMS e que deverá ser informado para assinatura.
            /// </summary>
            public const string SMS = "Token por SMS";
            /// <summary>
            /// Token gerado e enviado por WhatsApp e que deverá ser informado para assinatura.
            /// </summary>
            public const string WhatsApp = "Token por WhatsApp";
        }

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

                /// <summary>
                /// Política ICP-Brasil de acordo com o padrão de assinatura.
                /// </summary>
                /// <param name="padraoDeAssinatura">CAdES, PAdES ou XAdES.</param>
                /// <returns>Política mais recente implementada pela Plataforma.</returns>
                public static string Padrao(string padraoDeAssinatura) =>
                    padraoDeAssinatura switch
                    {
                        PadroesDeAssinatura.CAdES => CAdES_PA_AD_RB_v2_3,
                        PadroesDeAssinatura.PAdES => PAdES_PA_AD_RB_v1_1,
                        PadroesDeAssinatura.XAdES => XAdES_PA_AD_RB_v2_4,
                        _ => throw new ArgumentException($"O padrão de assinatura informado ({padraoDeAssinatura}) não representa um valor válido. Escolhe entre: {PadroesDeAssinatura.CAdES}, {PadroesDeAssinatura.PAdES} ou {PadroesDeAssinatura.XAdES}.", nameof(padraoDeAssinatura))
                    };
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