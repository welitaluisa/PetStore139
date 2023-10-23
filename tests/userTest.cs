// 1- Bibliotecas 
using Newtonsoft.Json; // dependencia para o JsonConvert
using RestSharp;

// 2 - NameSpace
namespace User;

// 3 - Classe
public class userTest
{

    // 3.1 - Atributos
    // Endereço da API
    private const string BASE_URL = "https://petstore.swagger.io/v2/";
    // 3.2 - Funções e Métodos
    [Test, Order(1)]
    public void PostUserTest()
    {
        // Configura
        // Instancia o objeto do tipo RestClient com o endereço da API
        var client = new RestClient(BASE_URL);

        // Instancia o objeto do tipo RestRequest com o complemento de endereço 
        // como " user" e configurando o método para ser um post ( Inclusão)
        var request = new RestRequest("user", Method.Post);

        // armazena o conteúdo do arquivo user1.json na memória
        String jsonBody = File.ReadAllText(@"C:\Iterasys\PetStore139\fixtures\user1.json");

        // Adiciona na requisição o conteúdo do arquivo user1.json
        request.AddBody(jsonBody);
        // Executa
        //Executa a requisição conforme a configuração realizada 
        // guarda o json retornado no objeto response
        var response = client.Execute(request);


        // Valida
        var responseBody = JsonConvert.DeserializeObject<dynamic>(response.Content);
        // Exibe o responseBody no console
        Console.WriteLine(responseBody);
        // Valide que na resposta, o staus code é igual ao resultado esperado (200)
        Assert.That((int)response.StatusCode, Is.EqualTo(200));
        // Valide a estrutura da resposta 
        Assert.That((int)responseBody.code, Is.EqualTo(200));
        Assert.That(responseBody.type.ToString(), Is.EqualTo("unknown"));
        Assert.That(responseBody.message.ToString(), Is.EqualTo("894951"));
    }

    [Test, Order(2)]
    public void GetUserTest()
    {
        var client = new RestClient(BASE_URL);
        var request = new RestRequest("user/Lele", Method.Get); // 

        var response = client.Execute(request);

        var responseBody = JsonConvert.DeserializeObject<dynamic>(response.Content);
        Console.WriteLine(responseBody);

        // Valide que na resposta, o staus code é igual ao resultado esperado (200)
        Assert.That((int)response.StatusCode, Is.EqualTo(200));

         // Valide a estrutura da resposta 
        Assert.That((int)responseBody.id, Is.EqualTo(894951));
        Assert.That(responseBody.username.ToString(), Is.EqualTo("Lele"));
        Assert.That(responseBody.userStatus.ToString(), Is.EqualTo("1"));
    }

    [Test, Order(3)]
    public void UpdateUserTest()
    {
        var client = new RestClient(BASE_URL);
        var request = new RestRequest("user/Lele", Method.Put);

        string jsonBody = File.ReadAllText(@"C:\Iterasys\PetStore139\fixtures\updated_user.json");

        request.AddBody(jsonBody);

        var response = client.Execute(request);
        var responseBody = JsonConvert.DeserializeObject<dynamic>(response.Content);
        Console.WriteLine(responseBody);

        // Valide que na resposta, o staus code é igual ao resultado esperado (200)
        Assert.That((int)response.StatusCode, Is.EqualTo(200));

         // Valide a estrutura da resposta 
        Assert.That((int)responseBody.code, Is.EqualTo(200));
        Assert.That(responseBody.type.ToString(), Is.EqualTo("unknown"));
        Assert.That(responseBody.message.ToString(), Is.EqualTo("8594951"));
    }

        [Test, Order(4)]
    public void DeleteUserTest()
    {
        var client = new RestClient(BASE_URL);
        var request = new RestRequest("user/Lele", Method.Delete); 

        var response = client.Execute(request);
        var responseBody = JsonConvert.DeserializeObject<dynamic>(response.Content);
        Console.WriteLine(responseBody);

        // Valide que na resposta, o staus code é igual ao resultado esperado (200)
        Assert.That((int)response.StatusCode, Is.EqualTo(200));

         // Valide a estrutura da resposta 
        Assert.That((int)responseBody.code, Is.EqualTo(200));
        Assert.That(responseBody.type.ToString(), Is.EqualTo("unknown"));
        Assert.That(responseBody.message.ToString(), Is.EqualTo("Lele"));
    }


}