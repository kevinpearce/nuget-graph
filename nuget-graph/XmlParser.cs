using System.Xml.Linq;

namespace nuget_graph;

public static class XmlParser
{
    public static async Task<IEnumerable<string?>> ParseProject(string projectFile)
    {
        return await ParseProjectInternal(projectFile, name => !string.IsNullOrEmpty(name));
    }

    public static async Task<IEnumerable<string?>> ParseProject(string projectFile, string[] allowedPrefixes)
    {
        return await ParseProjectInternal(projectFile, name => 
            !string.IsNullOrEmpty(name) && allowedPrefixes.Any(name.StartsWith));
    }

    private static async Task<IEnumerable<string?>> ParseProjectInternal(string projectFile, Func<string?, bool> filter)
    {
        await using var stream = File.OpenRead(projectFile);
        var doc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);

        return doc.Descendants("PackageReference")
            .Select(e => e.Attribute("Include")?.Value)
            .Where(filter)
            .ToList();
    }
}