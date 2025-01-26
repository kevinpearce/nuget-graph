using System.Diagnostics;
using System.Text;

namespace nuget_graph;

public static class HtmlWriter
{
    public static void CreateHtmlOutput(List<string> edges, string rootPath)
    {
        var mermaidGraph = new StringBuilder();
        foreach (var edge in edges)
        {
            mermaidGraph.Append(edge);
        }
        
        var html = $$"""
                     <!DOCTYPE html>
                     <html lang='en'>
                     <head>
                         <meta charset='UTF-8'>
                         <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                         <title>nuget-graph</title>
                         <script src='https://cdn.jsdelivr.net/npm/mermaid/dist/mermaid.min.js'></script>
                         <script>
                             mermaid.initialize({ startOnLoad: true });
                         </script>
                     </head>
                     <body>
                         <h1>{{rootPath}}</h1>
                         <div class='mermaid'>
                             {{mermaidGraph.ToString().Trim()}}
                         </div>
                     </body>
                     </html>
                     """;
        
        var filePath = $"{rootPath}/output_{DateTime.Now:yyyyMMdd_HHmmss}.html";
        File.WriteAllText(filePath, html);
        
        OpenHtmlInBrowser(filePath);
    }

    private static void OpenHtmlInBrowser(string filePath)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            }
        };
        process.Start();
    }
}