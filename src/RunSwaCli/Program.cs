// See https://aka.ms/new-console-template for more information
using RunSwaCli;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

var client = new HttpClient();

var batchFileName = Path.Combine(Environment.CurrentDirectory, "runcli.bat");
//if (!File.Exists(batchFileName))
{
    Console.WriteLine("Create Batch!");

    var builder = new BatchBuilder();

    // https://azure.github.io/static-web-apps-cli/docs/cli/swa-start/
    builder.ReactUrl = "http://localhost:3000";
    if (!(await ExistsUrlAsync(builder.ReactUrl)))
        throw new Exception($"{builder.ReactUrl} not exists.");
    Console.WriteLine($"ReactUrl : {builder.ReactUrl}.");

    builder.SolutionPath = GetSlhPath();
    Console.WriteLine($"SolutionPath : {builder.SolutionPath}.");
    builder.ApiUrl = await GetApiUrlAsync(builder.SolutionPath);
    Console.WriteLine($"ApiUrl : {builder.ApiUrl}.");

    var batchText = builder.Build();
    Console.WriteLine($"BatchFileName : {batchFileName}.");
    File.WriteAllText(batchFileName, batchText);
}

var process = Process.Start(batchFileName);
await process.WaitForExitAsync();

Console.WriteLine("ok");
Console.ReadLine();

async Task<bool> ExistsUrlAsync(string url)
{
    try
    {
        var res = await client.GetAsync(url);
        return res.StatusCode == HttpStatusCode.OK;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        return false;
    }
}

string GetSlhPath()
{
    var result = Environment.CurrentDirectory;
    while (!Directory.GetFileSystemEntries(result).Any(q => Path.GetExtension(q) == ".sln"))
    {
        result = Path.GetDirectoryName(result);
        if (result is null)
            throw new Exception($"{result} not exists.");
    }
    return result;
}

async Task<string?> GetApiUrlAsync(string slhPath)
{
    foreach (var path in Directory.GetDirectories(slhPath))
    {
        var sercher = new LaunchSettingsSearcher(path);
        var launchSettings = sercher.Get();
        var profiles = launchSettings?.Profiles;
        if (profiles is null)
            continue;

        var urls = new List<string>();
        if (profiles.Api?.ApplicationUrl?.Split(";") is { } appUrls)
        {
            urls.AddRange(appUrls.Where(q => q.StartsWith("http://")));
        }
        if (profiles.FunctionApp1?.CommandLineArgs is { } funcArgs)
        {
            var split = funcArgs.Split(" ");
            var index = Array.IndexOf(split, "--port");
            var port = index >= 0 ? split.ElementAtOrDefault(index + 1) : null;
            if (!string.IsNullOrEmpty(port))
                urls.Add($"http://localhost:{port}");
        }

        foreach (var url in urls)
        {
            if (await ExistsUrlAsync(url))
            {
                return url;
            }
        }
    }
    return null;
}