// Assets/Editor/ProjectStructureExporter.cs
using UnityEditor;
using UnityEngine;
using System.IO;

public class ProjectStructureExporter
{
    [MenuItem("Tools/Export Project Structure")]
    public static void ExportStructure()
    {
        string rootPath = Application.dataPath;
        string outputPath = Path.Combine(Application.dataPath, "project_structure.txt");

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            WriteDirectory(writer, rootPath, "");
        }

        Debug.Log("Project structure exported to: " + outputPath);
        AssetDatabase.Refresh();
    }

    private static void WriteDirectory(StreamWriter writer, string path, string indent)
    {
        foreach (var dir in Directory.GetDirectories(path))
        {
            writer.WriteLine($"{indent}- {Path.GetFileName(dir)}/");
            WriteDirectory(writer, dir, indent + "  ");
        }

        foreach (var file in Directory.GetFiles(path))
        {
            if (file.EndsWith(".meta")) continue;
            writer.WriteLine($"{indent}- {Path.GetFileName(file)}");
        }
    }
}
