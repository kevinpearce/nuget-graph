namespace nuget_graph;

public readonly struct Vertex(string id)
{
    public string Id { get; } = id;
}