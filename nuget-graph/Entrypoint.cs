using System.Xml.Linq;

namespace nuget_graph;

public class Entrypoint : IEntrypoint
{
    public Task RunAsync()
    {
        // var rootDirectory = Directory.GetCurrentDirectory();
        var rootDirectory = "/Users/kpearce/Developer/jot";

        var graph = BuildProjectGraph(rootDirectory);

        foreach (var project in graph)
        {
            Console.WriteLine($"Project : {project.Key}");

            foreach (var dependency in project.Value.Dependencies)
            {
                Console.WriteLine($"Dependency : {dependency.Name}");
            }
        }
        
        return Task.CompletedTask;
    }
    
    private static Dictionary<string, Node> BuildProjectGraph(string rootDirectory)
    {
        var projectGraph = new Dictionary<string, Node>();

        foreach (var projectFile in Directory.GetFiles(rootDirectory, "*.csproj", SearchOption.AllDirectories))
        {
            if (!projectGraph.TryGetValue(projectFile, out var value))
            {
                value = new Node(projectFile);
                projectGraph[projectFile] = value;
            }
            
            var node = value;
            var referencedProjects = GetProjectReferencesAsync(projectFile).Result;
            foreach (var referencedProject in referencedProjects)
                node.AddDependency(new Node(referencedProject!));
        }
        
        return projectGraph;
    }
    
    private static async Task<IEnumerable<string?>> GetProjectReferencesAsync(string projectFile)
    {
        await using var stream = File.OpenRead(projectFile);
        var doc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);

        return doc.Descendants("PackageReference")
            .Select(e => e.Attribute("Include")?.Value)
            .Where(name => !string.IsNullOrEmpty(name))
            .ToList();
    }
}