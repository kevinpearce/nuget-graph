namespace nuget_graph;

public class Graph
{
    private static string[] _allowedPrefixes = [];
    private Dictionary<string, Vertex> Vertices { get; } = new();
    public List<string> Edges { get; } = ["graph TD\n"];

    public Graph(string rootDirectory, string[] allowedPrefixes)
    {
        _allowedPrefixes = allowedPrefixes;
        CreateGraph(rootDirectory);
    }
    
    private void CreateGraph(string rootDirectory)
    {
        foreach (var projectFile in Directory.GetFiles(rootDirectory, "*.csproj", SearchOption.AllDirectories))
        {
            var id = Path.GetFileNameWithoutExtension(projectFile);
            
            if (!Vertices.TryGetValue(id, out var value))
            {
                value = new Vertex(id);
                Vertices[id] = value;
            }
            
            var node = value;
            
            //ensure vertex is added to output regardless of dependencies
            Edges.Add($"        {node.Id}\n");
            
            var referencedProjects = _allowedPrefixes.Length == 0
                ? XmlParser.ParseProject(projectFile).Result
                : XmlParser.ParseProject(projectFile, _allowedPrefixes).Result;
            
            foreach (var referencedProject in referencedProjects)
                Edges.Add($"        {node.Id} --> {referencedProject}\n");
        }
    }
}