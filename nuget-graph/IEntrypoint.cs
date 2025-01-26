namespace nuget_graph;

public interface IEntrypoint
{
    Task Run(string[] args);
}