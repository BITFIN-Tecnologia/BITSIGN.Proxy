﻿// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Configurações referente ao processo operacional e integração.
    /// </summary>
    [DebuggerDisplay("{UrlDeCallback}")]
    public class Integracao
    {
        /// <summary>
        /// Url para notificar o contratante dos eventos ocorridos.
        /// </summary>
        public string UrlDeCallback { get; set; }

        /// <summary>
        /// Headers que serão embutidos na requisição quando o callback for enviado ao contratante. Exemplo: AppKey=123;Servico=Assinaturas
        /// </summary>
        public string HeadersDeCallback { get; set; }

        /// <summary>
        /// Determina em qual formato será serializado o objeto de callback, pode ser JSON ou XML.
        /// </summary>
        public string FormatoDeCallback { get; set; }

        /// <summary>
        /// Relação de IPs que estão autorizados à consumir a API. Múltiplos IPs são suportados desde que separados por ponto-e-vírgula (;).
        /// </summary>
        public string IPsAutorizados { get; set; }

        /// <summary>
        /// Eventos disponíveis para notificação (Financeiro, Pacote, Lote, Documento, Assinatura e Assinante).
        /// </summary>
        public string EventosReportados { get; set; }

        /// <summary>
        /// Indica quais documentos (Original, Assinado, Manifesto e Original + Manifesto) devem compor o pacote gerado quando um lote é concluído (assinado).
        /// </summary>
        public string DocumentosDoPacote { get; set; }

        /// <summary>
        /// Indica o local onde será definido o carimbo da assinatura no documento original (Topo, Esquerda, Direita ou Rodapé).)
        /// </summary>
        public string PosicaoDoCarimbo { get; set; }

        /// <summary>
        /// Mensagens predefinidas com os possíveis motivos de rejeição que o assinante poderá selecionar.
        /// </summary>
        public string RejeicoesPadroes { get; set; }

        /// <summary>
        /// Permite ou não a geração de links para acompanhamento ao vivo do processo de assinaturas dos documentos.
        /// </summary>
        public bool AcompanhamentoAoVivo { get; set; }
    }
}