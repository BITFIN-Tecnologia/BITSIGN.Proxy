# BITSIGN.Proxy
###### Comunicação com as APIs de Assinaturas Digitais
Biblioteca .NET para consumo dos serviços (APIs) fornecidos pela BITSIGN, recepção de _callbacks_ e demais recursos para a integração entre sistemas. Essa biblioteca abstrai toda a complexidade de comunicação com o protocolo HTTP, simplificando o consumo pelos clientes, que acessarão as classes e métodos que refletem exatamente a documentação da API, não havendo a necessidade de lidar diretamente com código referente à infraestrutura.

Além da comunicação que já está embutida, a biblioteca também oferece recursos para _logging_, correlação de requisições e classes de DTOs (que também são definidas pelas APIs). Isso garantirá uma experiência diferenciada para consumo dos serviços, já que o _proxy_ expõe em sua _interface_ pública, métodos e propriedades que refletem o negócio, que é o de "assinaturas digitais".

> Processo de Assinaturas (visão técnica e operacional): [https://www.bitsign.com.br/documentacao](https://www.bitsign.com.br/documentacao).

> Documentação das APIs: [https://www.bitsign.com.br/apis](https://www.bitsign.com.br/apis).

## Conexão e Autenticação
A classe que intermedia toda a comunicação é chamada de `ProxyDoServico`. Essa classe recebe em seu construtor os dados necessários para estabelecer a comunicação com os serviços. Todos os parâmetros necessários são informados através da classe `Conexao`, onde o **código do contratante** e o **código de integração** são fornecidos no nomento da criação/contratação; além disso, é neste objeto que também deverá ser informado para qual ambiente as requisições devem ser encaminhadas: **SANDBOX** ou **PRODUÇÃO**.

```csharp
var codigoDoContratante = new Guid("985e0702-e94a-4954-b7a8-1f28c73c8122");
var chaveDeIntegracao = "TWpZd00yTXpPVGN0TmpFMk9TMDBaRGRqTFdFMk1XTXROR1kzWkRVM01qTmhNR0Zq";

using (var proxy = new ProxyDoServico(
    new Conexao(
        Ambiente.Sandbox,
        codigoDoContratante,
        chaveDeIntegracao,
        FormatoDeSerializacao.Json)))
{
    //consumo dos serviços (APIs)
}
```

O _proxy_ é também encarregado de configurar a autenticação da conexão, nomeando e anexando os _headers_ exigidos pelo serviço para identificar quem é o cliente que está consumindo. Por fim, ainda há o formato de serialização em que o _proxy_ irá operar. Por padrão, ele utilizará o formato **JSON**, mas através do enumerador `FormatoDeSerializacao` é possível alternar para o formato **XML**.

## Logging
O _logging_ é um item de extrema importância em ambientes distribuídos, já que invariavelmente, precisamos depurar eventuais problemas que ocorrem. Se o código não estiver bem instrumentado em relação à isso, pode-se perder muito tempo para descobrir o problema e corrigí-lo. Para auxiliar no desenvolvimento e consumo pelos clientes, foi espalhado por toda biblioteca, pontos de captura de informações que podem ser relevantes para a análise. 

Para indicar ao _proxy_ que deseja capturar estas informações, será necessário determinar o local onde estes _logs_ serão armazenados. Nativamente, o biblioteca fornece uma implementação chamada `LogEmTexto` recebe as mensagens e as armazena em um `TextWriter`, porém, se desejar algum repositório mais específico, como uma base de dados por exemplo, basta implementar a _interface_ [ILogger](https://github.com/BITFIN-Software/BITSIGN.Proxy/blob/master/BITSIGN.Proxy/Logging/ILogger.cs), customizando cada um dos métodos.

`ILogger` herda da _interface_ `IDisposable`, e quando o _logger_ for envolvido em um bloco `using`, o método `Dispose` é chamado e as mensagens, que estão no `buffer` são armazenadas fisicamente. Uma vez que _logger_ esteja criado, basta informá-lo no construtor do _proxy_, que internamente fará uso, quando necessário, para armazenar as informações mais relevantes, que podem ocorrer durante a preparação e o envio das requisições e para armazenar o resultado recebido dos serviços.

```csharp
using (var log = new LogEmTexto(new StreamWriter("Log.txt", true)))
{
    using (var proxy = new ProxyDoServico(this.Conexao, log))
    {
        //consumo dos serviços (APIs)
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
        //consumo dos serviços (APIs)
    }
}
```

```
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - INÍCIO DO ESCOPO
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - POST /lotes
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Request.Type: ByteArrayContent
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Request.Content-Type: 
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Request.Headers: BS-Contratante=985e0702-e94a-4954-b7a8-1f28c73c8122;BS-Token=TWpZd00yTXpPVGN0TmpFMk9TMDBaRGRqTFdFMk1XTXROR1kzWkRVM01qTmhNR0Zq;BS-CodigoDeRastreio: 2c83d520-138d-45d1-b29c-b9c4bf027ac2;Accept=application/json
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Response.Headers: Location=http://localhost:33664/api/lotes/06202cf4-281d-46a5-bd81-975c15f58d94;
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Response.StatusCode: Created
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Response.ReasonPhrase: Created
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Response.Type: HttpConnectionResponseContent
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - Response.Content-Type: application/json; charset=utf-8
11/02/2021 20:52:57 - Info - 2c83d520-138d-45d1-b29c-b9c4bf027ac2 - FIM DO ESCOPO
```
## Exemplos
Para os principais cenários, foi incluído neste mesmo repositório, um projeto chamado de `Testes`, onde cada exemplo faz o consumo de um serviço, parametrizando de acordo com a necessidade de cada um, envia ao respectivo serviço e trata o retorno devolvido, que simplesmente exibe as informações em tela. Abaixo está um dos exemplos que compõem o projeto de `Testes`. Consulte o projeto para os demais exemplos.

```csharp
public class ConsultaDeDocumentos : Exemplo
{
    public override async Task Executar(params string[] parametros)
    {
        using (var proxy = new ProxyDoServico(this.Conexao))
        {
            //Retorna todas as informações de um determinado documento, exceto seus arquivos (bytes[]).
            var documento = await proxy.Documentos.Detalhes(new Guid("aa9076b3-a058-44e2-b776-dca0a1743ce7"));

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
    01 - UploadDeLote
    02 - LogsComDepuracao
    03 - ConsultaDeLotes
    04 - ConsultaDeDocumentos
    05 - DownloadDePacote
    06 - DadosFinanceiros
INFORME O NUMERO DO EXEMPLO:
```

## Callbacks
Os _callbacks_ servem para recepcionar algum evento relevante que foi gerado pelo processo de assinaturas. Uma vez que o contratante informa ao serviço que ele deseja receber estas notificações, será necessário informar uma `URL` para que o serviço envie o _callback_. Além da `URL`, você pode optar também em qual formato deseja receber este _callback_, onde o padrão é o formato **JSON** e pode ser alterado para **XML**. 

Abaixo temos a estrutura do objeto, que será serializado em JSON ou XML e entregue no local (`URL`) previamente informado, e o cliente então, pode utilizar a informação como desejar; para entender como o serviço entende que o _callback_ foi entregue e sobre a quantidade de tentativas de entrega, [consulte a documentação](https://www.bitsing.com.br/documentacao#integracaoCallbacks).

```csharp
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
    public string Complemento { get; set; }

    /// <summary>
    /// Se a entidade que gerou o callback possuir tags associadas, elas serão informadas nesta propriedade.
    /// </summary>
    public string Tags { get; set; }
}
```
---

> #### CONTATOS
>
> - Site: <https://www.bitsign.com.br>
> - E-mail: <contato@bitsign.com.br>
>
