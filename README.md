# BITSIGN.Proxy
###### Comunicação com as API's de Assinaturas Digitais e Eletrônicas
Biblioteca .NET para consumo dos serviços (API's) fornecidos pela BITSIGN, recepção de _callbacks_ e demais recursos para a integração entre sistemas. Essa biblioteca abstrai toda a complexidade de comunicação com o protocolo HTTP, simplificando o consumo pelos clientes, que acessarão as classes e métodos que refletem exatamente a documentação da API, não havendo a necessidade de lidar diretamente com código referente à infraestrutura.

Além da comunicação que já está embutida, a biblioteca também oferece recursos para _logging_, correlação de requisições e classes de DTOs (que também são definidas pelas API's). Isso garantirá uma experiência diferenciada para consumo dos serviços, já que o _proxy_ expõe em sua _interface_ pública, métodos e propriedades que refletem o negócio, que é o de "assinaturas digitais".

> Processo de Assinaturas (visão técnica e operacional): [https://bitsign.com.br/documentacao](https://bitsign.com.br/documentacao)

> Documentação das API's: [https://bitsign.com.br/documentacao/apis](https://bitsign.com.br/documentacao/apis)

> NUGET: [PM> Install-Package BITFIN.BITSIGN.Proxy](https://www.nuget.org/packages/BITFIN.BITSIGN.Proxy)

## Conexão e Autenticação
A classe que intermedia toda a comunicação é chamada de `ProxyDoServico`. Essa classe recebe em seu construtor os dados necessários para estabelecer a comunicação com os serviços. Todos os parâmetros necessários são informados através da classe `Conexao`, onde o **código do contratante**, **código da aplicação** e o **código de integração** são fornecidos no nomento da criação/contratação; além disso, é neste objeto que também deverá ser informado a versão da API a ser utilizada e para qual ambiente as requisições devem ser encaminhadas: **SANDBOX** ou **PRODUÇÃO**.

```csharp
var nome = "Aplicação Xpto";
var versao = "v1";
var codigoDoContratante = new Guid("985e0702-e94a-4954-b7a8-1f28c73c8122");
var codigoDaAplicacao = new Guid("98b4307e-51d4-4f2f-88da-cbb23b903db5");
var chaveDeIntegracao = "TWpZd00yTXpPVGN...zWkRVM01qTmhNR0Zq";

using (var proxy = new ProxyDoServico(
    new Conexao(
        nome,
        Ambiente.Sandbox,
        versao,
        codigoDoContratante,
        codigoDaAplicacao,
        chaveDeIntegracao,
        FormatoDeSerializacao.Json)))
{
    //consumo dos serviços (API's)
}
```

> A classe `Conexao` está associada, exclusivamente, à uma única aplicação; pelo fato do _proxy_ receber a conexão em seu construtor, ele só poderá manipular os serviços (API's) referente aquela aplicação. Serão necessárias múltiplas instâncias do _proxy_ caso necessite, simultaneamente, interagir com mais de uma aplicação.

O _proxy_ é também encarregado de configurar a autenticação da conexão, nomeando e anexando os _headers_ exigidos pelo serviço para identificar quem é o cliente que está consumindo. Por fim, ainda há o formato de serialização em que o _proxy_ irá operar. Por padrão, ele utilizará o formato **JSON**, mas através do enumerador `FormatoDeSerializacao` é possível alternar para o formato **XML**.

As configurações de conexão também estão expostas através da classe abstrata [Configuracao](https://github.com/BITFIN-Software/BITSIGN.Proxy/blob/master/BITSIGN.Proxy/Configuracoes/Configuracao.cs), possibilitando a extração destas informações de algum repositório, como por exemplo, dos arquivos de configurações ([App.config](https://github.com/BITFIN-Software/BITSIGN.Proxy/blob/master/BITSIGN.Proxy/Configuracoes/AppSettings.cs), Web.config ou [appSettings.json](https://github.com/BITFIN-Software/BITSIGN.Proxy/blob/master/BITSIGN.Proxy/Configuracoes/AppSettingsJson.cs)) e com isso, não deixar estes parâmetros em "hard-code", possibilitando a alteração sem a necessidade de recompilar o programa.

## Logging
O _logging_ é um item de extrema importância em ambientes distribuídos, já que invariavelmente, precisamos depurar eventuais problemas que ocorrem. Se o código não estiver bem instrumentado em relação à isso, pode-se perder muito tempo para descobrir o problema e corrigí-lo. Para auxiliar no desenvolvimento e consumo pelos clientes, foi espalhado por toda biblioteca, pontos de captura de informações que podem ser relevantes para a análise. 

Para indicar ao _proxy_ que deseja capturar estas informações, será necessário determinar o local onde estes _logs_ serão armazenados. Nativamente, o biblioteca fornece uma implementação chamada `LogEmTexto` recebe as mensagens e as armazena em um `TextWriter`, porém, se desejar algum repositório mais específico, como uma base de dados por exemplo, basta implementar a _interface_ [ILogger](https://github.com/BITFIN-Software/BITSIGN.Proxy/blob/master/BITSIGN.Proxy/Logging/ILogger.cs), customizando cada um dos métodos.

`ILogger` herda da _interface_ `IDisposable`, e quando o _logger_ for envolvido em um bloco `using`, o método `Dispose` é chamado e as mensagens, que estão no `buffer` são armazenadas fisicamente. Uma vez que _logger_ esteja criado, basta informá-lo no construtor do _proxy_, que internamente fará uso, quando necessário, para armazenar as informações mais relevantes, que podem ocorrer durante a preparação e o envio das requisições e para armazenar o resultado recebido dos serviços.

```csharp
using (var log = new LogEmTexto(new StreamWriter("Log.txt", true)))
{
    using (var proxy = new ProxyDoServico(this.Conexao, log))
    {
        //consumo dos serviços (API's)
    }
}

Console.Write(File.ReadAllText("Log.txt"));
```
### Rastreio de Requisições
Opcionalmente, é possível gerar um "código de rastreio", que identifica unicamente a requisição que será enviada ao serviço. Isso permitirá correlacionar a mensagem ao processamento remoto realizado pelo serviço e, consequentemente, facilitar a depuração de eventuais problemas. Da mesma forma que o _log_, o gerador de códigos de rastreios é extensível, e pode ser customizado simplesmente implementando a _interface_ [IRastreio](https://github.com/BITFIN-Software/BITSIGN.Proxy/blob/master/BITSIGN.Proxy/Logging/IRastreio.cs). De qualquer forma, já existe um gerador nativo, chamado `RastreioComGuid`, que como o próprio nome sugere, para cada requisição, gera um novo _Guid_.

E, para informar que desejar utilizar um rastreador de requisições, basta informar o gerador de códigos no construtor da classe `ProxyDoServico`, como se vê no trecho de código abaixo. E, na sequência, o conteúdo do _log_ armazenado no arquivo texto. Note, que o Guid que se repete por todas as linhas, relacionam todas as informações associadas aquela requisição e sua respectiva resposta.

```csharp
using (var log = new LogEmTexto(new StreamWriter("Log.txt", true)))
{
    using (var proxy = new ProxyDoServico(this.Conexao, log, new RastreioComGuid()))
    {
        //consumo dos serviços (API's)
    }
}
```

```
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - INÍCIO DO ESCOPO
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - POST /lotes
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Request.Type: ByteArrayContent
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Request.Content-Type: 
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Request.Headers: Authorization: TWpZd00yTXpPVGN...zWkRVM01qTmhNR0Zq;BS-Tracking: 2c83d520-138d-45d1-b29c-b9c4bf027ac2;Accept: application/json
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Response.Headers: Location=http://localhost:33664/api/lotes/06202cf4-281d-46a5-bd81-975c15f58d94;
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Response.StatusCode: Created
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Response.ReasonPhrase: Created
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Response.Type: HttpConnectionResponseContent
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Response.Content-Type: application/json; charset=utf-8
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - FIM DO ESCOPO
```

> **Correlação com a Resposta:** Por fim, quando este código estiver presente na requisição, a BITSIGN o devolverá, também, através do mesmo _header_ na resposta; com isso, a aplicação consumidora poderá realizar alguma consistência sobre ele, como correlacionar as mensagens, armazenamento de _logs_, etc.

## Callbacks
Os _callbacks_ servem para recepcionar algum evento relevante que foi gerado pelo processo de assinaturas. Uma vez que o contratante informa ao serviço que ele deseja receber estas notificações, será necessário informar uma `URL` para que o serviço envie o _callback_. Além da `URL`, você pode optar também em qual formato deseja receber este _callback_, onde o padrão é o formato **JSON** e pode ser alterado para **XML**. 

Abaixo temos a estrutura do objeto, que será serializado em JSON ou XML e entregue no local (`URL`) previamente informado, e o cliente então, pode utilizar a informação como desejar; para entender como o serviço entende que o _callback_ foi entregue e sobre a quantidade de tentativas de entrega, [consulte a documentação](https://bitsign.com.br/documentacao#integracaoCallbacks).

```csharp
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
    /// <remarks>Na maioria das vezes será um <see cref="Guid"/>; porém quando se tratar de um assinante, o <see cref="Id"/> será o número de seu documento (CNPJ/CPF).</remarks>
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
    public object Complemento { get; set; }

    /// <summary>
    /// Se a entidade que gerou o callback possuir tags associadas, elas serão informadas nesta propriedade.
    /// </summary>
    public string Tags { get; set; }
    
    /// <summary>
    /// Identificador da aplicação à qual o callback está relacionado.
    /// </summary>
    public Guid AplicacaoId { get; set; }
}
```
### Serviço de Dumps
Caso não tenha um ambiente definido para _callbacks_, e mesmo assim quer ter a chance de visualizar os eventos gerados pela plataforma, você pode optar por utilizar o **Serviço de Dumps** da BITSIGN. Este serviço permite você criar um ambiente totalmente "descartável" para receber as requisições de _callbacks_, que pode ser útil até que se tenha um ambiente real e de produção para recepcionar estas notificações. Para habilitar este serviço, que está disponível apenas em ambiente de **SANDBOX**, basta ir até as configurações da aplicação e utilizar a opção para gerar o repositório; um link randômico será gerado e você poderá utilizá-lo para visualizar todos os eventos que estão sendo gerados.

> **IMPORTANTE:** Apesar de ser um serviço gratuito, ele está limitado em, no máximo, **100 requisições**; a partir daí, as requisições mais antigas começam a ser sobrescritas. Este recurso deve ser utilizado exclusivamente em ambiente de testes e de validação. O visualizador de dumps está acessível para qualquer um que possua o link. É importante que você o mantenha protegido e, por questões de segurança, quando finalizar os testes, opte por remover todo o conteúdo gerado.

![Serviço de Dumps da BITSIGN](http://bitsign.com.br/Documentacao/Dumps.png)

> Este _proxy_ oferece suporte para interagir com este serviço através da API [Dumps](https://github.com/BITFIN-Software/BITSIGN.Proxy/blob/master/BITSIGN.Proxy/Comunicacao/APIs/Dumps.cs).

## Status dos Serviços
O _proxy_ também expõe uma propriedade para analisar o status dos serviços e seus recursos associados. Isso permitirá ao cliente que consome as API's possa criar alguma regra em torno disso, e desabilitar e reabilitar funcionalidades em seu sistema de acordo com a situação atual de cada serviço. A propriedade `Status` está acessível a partir do _proxy_ e através do método `Atualizar` é acessar o relatório com a situação de todos os serviços. Abaixo temos o exemplo de como chamar o método e exibir o relatório:

```csharp
using (var proxy = new ProxyDoServico(this.Conexao))
{
    var relatorio = await proxy.Status.Atualizar(cancellationToken);

    Console.WriteLine($"Status Geral: {relatorio.Status}");
    Console.WriteLine("--------- SERVIÇOS ---------");

    foreach (var s in relatorio.Servicos)
    {
        Console.WriteLine($"Serviço: {s.Nome}");
        Console.WriteLine($"Status: {s.Status}");
        Console.WriteLine($"Mensagem: {s.Mensagem}");
        Console.WriteLine();
    }
}
```
```
Status Geral: Online

--------- SERVIÇOS ---------
Serviço: Base de Dados
Status: Online
Mensagem: Base de Dados Online.

Serviço: NTP.br - Horário de Brasília
Status: Online
Mensagem: Horário de Brasília Validado.
...
```
> É importante dizer que o _proxy_, nesta versão, ignora o status atual do serviço, ou seja, mesmo que por algum motivo ele esteja indisponível, a requisição sempre será enviada.
---
## Exemplos
Para os principais cenários, foi incluído neste mesmo repositório, um projeto chamado de `Testes`, onde cada exemplo faz o consumo de um serviço, parametrizando de acordo com a necessidade de cada um, envia ao respectivo serviço e trata o retorno devolvido, que simplesmente exibe as informações em tela. Abaixo está um dos exemplos que compõem o projeto de `Testes`. Consulte o projeto para os demais exemplos.

```csharp
public class ConsultaDeDocumentos : Exemplo
{
    public override async Task Executar(CancellationToken cancellationToken = default)
    {
        using (var proxy = new ProxyDoServico(this.Conexao))
        {
            //Retorna todas as informações de um determinado documento, exceto seus arquivos (bytes[]).
            var documento = await proxy.Documentos.Detalhes(new("aa9076b3-a058-44e2-b776-dca0a1743ce7"), cancellationToken);

            if (documento != null)
            {
                Console.WriteLine($"Status: {documento.Status}");

                //Individualmente, cada método a seguir retorna: arquivo origninal, assinado e o manifesto.
                File.WriteAllBytes(documento.NomeDoArquivo, await proxy.Documentos.Original(documento.Id));

                if (documento.Status == "Assinado")
                {
                    File.WriteAllBytes(documento.NomeDoArquivoAssinado, await proxy.Documentos.Assinado(documento.Id));
                    File.WriteAllBytes(documento.NomeDoArquivoDeManifesto, await proxy.Documentos.Manifesto(documento.Id));
                }
            }
        }
    }
}
```
O projeto de `Testes` é do tipo `Console`, o que quer dizer que você poderá executá-lo. Ao fazer isso, uma relação dos exemplos disponíveis será listado. Basta você digitar o número que corresponde ao serviço para que ele seja executado. Só é importante configurar os parâmetros de acordo com o que deseja testar.

```
EXEMPLOS DISPONIVEIS
  01 - UploadDePacote
  02 - UploadDeLote
  03 - LogsComDepuracao
  04 - ConsultaDeLotes
  05 - ConsultaDeDocumentos
  06 - DownloadDePacote
  07 - DadosFinanceiros
  08 - AtualizacaoDeConfiguracoes
  09 - RenovacaoDeChave
  10 - NotificacoesDoLote
  11 - UsoDoAppSettings
  12 - DadosDoContratante
  13 - StatusDosServicos
  14 - BuscadorDeRecursos
  15 - TratamentoDeErros
  16 - DocumentosComTemplate
  17 - PadraoPAdES
  18 - PadraoXAdES
  19 - VisualizacaoDeDumps
  20 - ValidandoCertificado
  21 - EmissaoDeCertificados
  22 - UploadDeArquivo
  
INFORME O NUMERO DO EXEMPLO:
```
---
> #### CONTATOS
>
> - Site: <https://bitsign.com.br>
> - E-mail Técnico: <dev@bitfin.com.br>
> - E-mail Comercial: <comercial@bitfin.com.br>
> - Telefone (+WhatsApp): +55 (19) 9.9901-1065
