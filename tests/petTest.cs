// 1- Bibliotecas 
using Models;
using Newtonsoft.Json; // dependencia para o JsonConvert
using RestSharp;

// 2 - NameSpace
namespace Pet;

// 3 - Classe
public class PetTest
{

    // 3.1 - Atributos
    // Endereço da API
    private const string BASE_URL = "https://petstore.swagger.io/v2/";
    // 3.2 - Funções e Métodos
    [Test, Order(1)]
    public void PostPetTest()
    {
        // Configura
        // Instancia o objeto do tipo RestClient com o endereço da API
        var client = new RestClient(BASE_URL);

        // Instancia o objeto do tipo RestRequest com o complemento de endereço 
        // como " PEt" e configurando o método para ser um post ( Inclusão)
        var request = new RestRequest("pet", Method.Post);

        // armazena o conteúdo do arquivo pet1.json na memória
        String jsonBody = File.ReadAllText(@"C:\Iterasys\PetStore139\fixtures\pet1.json");

        // Adiciona na requisição o conteúdo do arquivo pet1.json
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
        // Valida o nome do animal na resposta 
        String name = responseBody.name;
        Assert.That(name, Is.EqualTo("Leticia"));

        // Valida o status do animal na resposta
        string status = responseBody.status.ToString();
        Assert.That(responseBody.status.ToString(), Is.EqualTo("available"));

    }

    [Test, Order(2)]
    public void GetPetTest()
    {
        // Configura
        int petId = 3899405;
        String petName = "Leticia";
        String categoryName = "cat";
        String tagsName = "vacinada";

        var client = new RestClient(BASE_URL);
        var request = new RestRequest($"pet/{petId}", Method.Get);
        // Executa
        var response = client.Execute(request);
        // Valide
        var responseBody = JsonConvert.DeserializeObject<dynamic>(response.Content);
        Console.WriteLine(responseBody);

        Assert.That((int)response.StatusCode, Is.EqualTo(200));
        Assert.That((int)responseBody.id, Is.EqualTo(petId));
        Assert.That((String)responseBody.name, Is.EqualTo(petName));
        Assert.That((String)responseBody.category.name, Is.EqualTo(categoryName));
        Assert.That((String)responseBody.tags[0].name, Is.EqualTo(tagsName));
    }

    [Test, Order(3)]
    public void UpdatePetTest()
    
    {   // Configura
        // os dados de entrada vão formar o body da alteração
        // Vamos ussar uma classe de modelo
        PetModel petModel = new PetModel();
        petModel.id = 1732181;
        petModel.category = new Category(1, "dog");
        petModel.name = "Athena";
        petModel.photoUrls = new String[]{""};
        petModel.tags = new Tag[]{new Tag(1, "vacinado"), 
                                    new Tag(2, "castrado")};
        petModel.status = "pending";

        // Transformar o modelo acima em um arquivo json
        var jsonBody = JsonConvert.SerializeObject(petModel, Formatting.Indented);
        Console.WriteLine(jsonBody);

        var client = new RestClient(BASE_URL);
        var request = new RestRequest("pet", Method.Put);
        request.AddBody(jsonBody);

        // Executa

        var response = client.Execute(request);
        // Valida
        var responseBody = JsonConvert.DeserializeObject<dynamic>(response.Content);

        Assert.That((int)response.StatusCode, Is.EqualTo(200));
        Assert.That((int)responseBody.id, Is.EqualTo(petModel.id));
        Assert.That((String)responseBody.tags[1].name, Is.EqualTo(petModel.tags[1].name));
        Assert.That((String)responseBody.status, Is.EqualTo(petModel.status));
    }

    [Test, Order(4)]
    public void DeletePetTest1()
    {
        var client = new RestClient(BASE_URL);
        var request = new RestRequest("pet/3899405", Method.Delete);

        var response = client.Execute(request);
        var responseBody = JsonConvert.DeserializeObject<dynamic>(response.Content);
        Console.WriteLine(responseBody);

        Assert.That((int)response.StatusCode, Is.EqualTo(200));
    }

    [Tet, Order(5)]
    public void DeletePetTest2()
    {
        // Configura
        int

        // Executa 

        // Valida 
    }
}

