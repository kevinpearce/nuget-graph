namespace nuget_graph;

public class Node(string name)
{
    public string Name { get; } = name;
    public List<Node> Dependencies { get; } = [];

    public void AddDependency(Node package)
    {
        Dependencies.Add(package);
    }
}