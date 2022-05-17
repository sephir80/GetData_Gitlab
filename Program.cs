using RestSharp;
using RestSharp.Authenticators;

//CPqiBXViBE4N87m5x6
//oKPs5b5UkBNTXAoxs5nK
string token = "oKPs5b5UkBNTXAoxs5nK";
string user = "root";
string password = "L0c@l4223";

var client = new RestClient("http://192.168.1.33/");
client.Authenticator = new JwtAuthenticator(token);

//var request = new RestRequest("api/v4/projects/223",Method.Get)
List<Booth> booths = new List<Booth>();

//var response = await client.GetJsonAsync<List<Project>>("api/v4/projects");
//api/v4/projects?search_namespaces=true&search=Flame_Italy/CM001
var response = await client.GetJsonAsync<List<Project>>("api/v4/projects?search_namespaces=true&search=Flame_Italy/CM002");
List<Production> items_approved = new List<Production>(); 
foreach (var item in response)
{
    var merg_info = await client.GetJsonAsync<List<string>>("/api/v4/projects/" +item.id+ "/merge_requests?state=merged");
    if (merg_info.Count == 0)
        merg_info.Add("not approved");
    items_approved.Add(new Production { id = item.id, name = item.name, description = item.description, web_url = item.web_url, merged_by= merg_info.First()  });
}

//booths.Add(new Booth { id = 1, name = "CM001", productions = response });


Console.WriteLine(response);


public class Booth
{
    public int id { get; set; }
    public string name { get; set; }
    public List<Production> productions{ get; set; }
}
/*
"merged_by": {
    "id": 8,
            "name": "Giacomo Trecordi",
            "username": "G.Trecordi",
            "state": "active",
            "avatar_url": "https://www.gravatar.com/avatar/fb94948dafc53059619ae8d334bc1779?s=80&d=identicon",
            "web_url": "http://192.168.1.33/G.Trecordi"
*/

public class Merged_by
{
    public string name { get; set; }

}

public class Production : Project
{
    public string merged_by;

}

public class Project
{
    public int id { get; set; }
    public string description { get; set; }
    public string name { get; set; }
    public string web_url { get; set; }
}

