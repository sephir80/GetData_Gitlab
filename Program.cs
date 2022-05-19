using RestSharp;
using RestSharp.Authenticators;
using GetData_Gitlab;



Get_Parameter parameter = new Get_Parameter("Parameter.cfg");

string token = parameter.token;
int nPages;
List<Project> projects = new List<Project>();


var client = new RestClient("http://192.168.1.33/");
client.Authenticator = new JwtAuthenticator(token);


//var response = await client.GetJsonAsync<List<Project>>("api/v4/projects");
//api/v4/projects?search_namespaces=true&search=Flame_Italy/CM001

var request = new RestRequest("api/v4/projects?simple=true", Method.Head);
var head = await client.ExecuteAsync(request);
nPages = int.Parse(head.Headers.ToList().Find(x => x.Name == "X-Total-Pages").Value.ToString());


for (int i = 1; i <= nPages; i++)
    projects.AddRange(await client.GetJsonAsync<List<Project>>("api/v4/projects?simple=true&page=" + i.ToString()));

List<Merge> listOfMergeRequestPerProject = new List<Merge>();


foreach (Project p in projects)
{
    request = new RestRequest("api/v4/projects/" + p.id + "/merge_requests?state=merged", Method.Head);
    head = await client.ExecuteAsync(request);
    nPages = int.Parse(head.Headers.ToList().Find(x => x.Name == "X-Total-Pages").Value.ToString());
    if (nPages > 0)
    {
        listOfMergeRequestPerProject.AddRange(await client.GetJsonAsync<List<Merge>>("api/v4/projects/" + p.id + "/merge_requests?state=merged"));
        if (listOfMergeRequestPerProject.Count > 0)
            projects.Find(x => x.id == p.id).rev_info = listOfMergeRequestPerProject.First();
        listOfMergeRequestPerProject.Clear();
    }
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

async void get_Revision_info()
{
}