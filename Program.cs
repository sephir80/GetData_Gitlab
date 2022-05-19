using RestSharp;
using RestSharp.Authenticators;
using GetData_Gitlab;



Get_Parameter parameter = new Get_Parameter("Parameter.cfg");

string token = parameter.token;
int nPages;



var client = new RestClient("http://192.168.1.33/");
client.Authenticator = new JwtAuthenticator(token);


// get project info *****************************************************

var request = new RestRequest("api/v4/projects?simple=true", Method.Head);
var head = await client.ExecuteAsync(request);

nPages = int.Parse(head.Headers.ToList().Find(x => x.Name == "X-Total-Pages").Value.ToString());

List<Project> projects = new List<Project>();

for (int i = 1; i <= nPages; i++)
{
    Console.WriteLine("pagine progetti = " + i.ToString());
    projects.AddRange(await client.GetJsonAsync<List<Project>>("api/v4/projects?simple=true&page=" + i.ToString()));
    Console.WriteLine(i.ToString());
}
Console.WriteLine("Progetti fininiti \n");
// get merge info ***********************************************************

request = new RestRequest("api/v4/merge_requests?scope=all", Method.Head);
head = await client.ExecuteAsync(request);

nPages = int.Parse(head.Headers.ToList().Find(x => x.Name == "X-Total-Pages").Value.ToString());

List<Merge> merges = new List<Merge>();

for (int i = 1; i <= nPages; i++)
{
    Console.WriteLine("pagine progetti = " + i.ToString());
    merges.AddRange(await client.GetJsonAsync<List<Merge>>("api/v4/merge_requests?scope=all&page =" + i.ToString()));
    Console.WriteLine(i.ToString());
}
Console.WriteLine("revisioni fininite \n");

// update merge info per project*********************************************


foreach (Merge merge in merges)
{
    if (merge.merged_by == null)
        merge.merged_by = new User();
    if (merge.state != "closed")
    {
        projects.Find(x => x.id==merge.project_id).rev_info = merge;

    }
}

foreach (Project p in projects)
{
    Console.WriteLine(p.ToString());
}


/*
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
*/
