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

Console.Write("Pagina Progetto caricata =  0 / " + nPages.ToString());
ValueTuple<Int32, Int32> Point = Console.GetCursorPosition();
Point.Item1 = Point.Item1 - 7;

for (int i = 1; i <= nPages; i++)
{

    projects.AddRange(await client.GetJsonAsync<List<Project>>("api/v4/projects?simple=true&page=" + i.ToString()));
    Console.SetCursorPosition(Point.Item1,Point.Item2);
    Console.Write(String.Format("{0,2}",i));

}

Point.Item1 = 0;
Point.Item2 = 1;
Console.SetCursorPosition(Point.Item1,Point.Item2);
Console.WriteLine("Progetti fininiti \n");
// get merge info ***********************************************************

request = new RestRequest("api/v4/merge_requests?scope=all", Method.Head);
head = await client.ExecuteAsync(request);

nPages = int.Parse(head.Headers.ToList().Find(x => x.Name == "X-Total-Pages").Value.ToString());

List<Merge> merges = new List<Merge>();
Console.Write("Pagina Revisioni caricata =  0 / " + nPages.ToString());
Point = Console.GetCursorPosition();
Point.Item1 = Point.Item1 - 7;

for (int i = 1; i <= nPages; i++)
{
    merges.AddRange(await client.GetJsonAsync<List<Merge>>("api/v4/merge_requests?scope=all&page=" + i.ToString()));
    Console.SetCursorPosition(Point.Item1, Point.Item2);
    Console.Write(String.Format("{0,2}", i));
}
Point.Item1 = 0;
Point.Item2 = 3;
Console.SetCursorPosition(Point.Item1, Point.Item2);
Console.WriteLine("revisioni fininite \n");


// update merge info per project*********************************************


foreach (Merge merge in merges)
{
    if (merge.merged_by == null)
        merge.merged_by = new User();


    if (merge.state != "closed")
    {
        Merge m = new Merge();
        m=projects.Find(x => x.id == merge.project_id).rev_info;
        if ((m.state == merge.state) & (m.id > merge.id))
            projects.Find(x => x.id == merge.project_id).rev_info=m;
        else
            projects.Find(x => x.id == merge.project_id).rev_info = merge;
    }
}

SaveOnFile mergesToSave = new SaveOnFile(merges,"pluto.csv");
SaveOnFile projectstoSave = new SaveOnFile(projects, "pippo.csv");



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
