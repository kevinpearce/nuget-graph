namespace nuget_graph;

public class Entrypoint : IEntrypoint
{
    public Task Run(string[] args)
    {
        // var rootDirectory = Directory.GetCurrentDirectory();
        var rootDirectory = "../../../example"; //local testing
        
        var graph = args.Length == 0 ? new Graph(rootDirectory) : new Graph(rootDirectory, args);
        
        HtmlWriter.CreateHtmlOutput(graph.Edges, rootDirectory);
 
        return Task.CompletedTask;
    }
}