// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

var repositoriesOne = await ProcessRepositoryOneAsync(client);

foreach (var repo in repositoriesOne)
{
    Console.WriteLine($"Name: {repo.Name}");
    Console.WriteLine($"Homepage: {repo.Homepage}");
    Console.WriteLine($"GitHub: {repo.GitHubHomeUrl}");
    Console.WriteLine($"Description: {repo.Description}");
    Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
    Console.WriteLine($"Last push: {repo.LastPush}");
    Console.WriteLine();
}


static async Task<List<RepositoryOne>> ProcessRepositoryOneAsync(HttpClient client)
{
    await using Stream stream =
        await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
    var repositories =
        await JsonSerializer.DeserializeAsync<List<RepositoryOne>>(stream);
    return repositories ?? new();
}

/*
    Test code that doesn't work 

*/

/*
var repositoriesTwo = await ProcessRepositoryTwoAsync(client);

static async Task<RepositoryTwo> ProcessRepositoryTwoAsync(HttpClient client)
{
    await using Stream stream =
        await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
    var repositories =
        await JsonSerializer.DeserializeAsync<RepositoryTwo>(stream);
    return repositories ?? new();
}

*/



