namespace nuget_graph;

public class Entrypoint : IEntrypoint
{
    public Task Run(string[] args)
    {
        var rootDirectory = Directory.GetCurrentDirectory();

        var graph = new Graph(rootDirectory, args);
        
        HtmlWriter.CreateHtmlOutput(graph.Edges, rootDirectory);
 
        return Task.CompletedTask;
    }
}