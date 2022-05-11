using RestSharp;
using RestSharp.Authenticators;

//CPqiBXViBE4N87m5x6
//oKPs5b5UkBNTXAoxs5nK
string token = "oKPs5b5UkBNTXAoxs5nK";
string user = "root";
string password = "L0c@l4223";


var client = new RestClient("http://192.168.1.33/");
client.Authenticator = new JwtAuthenticator(token);

//var request = new RestRequest("api/v4/projects/223",Method.Get);
var response = await client.GetJsonAsync<List<Project>>("api/v4/projects");
Console.WriteLine(response);

public class Project
{
    public int id { get; set; }
    public string description { get; set; }
    public string name { get; set; }
}
