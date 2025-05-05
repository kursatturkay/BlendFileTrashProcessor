using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class BlendFileTrashProcessor : AssetPostprocessor
{
    
    private static readonly System.Text.RegularExpressions.Regex BlendBackupFileRegex = 
        new System.Text.RegularExpressions.Regex(@"\.blend\d+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    
    
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        
        
        foreach (string assetPath in importedAssets)
        {
            CheckAndTrashBlendFile(assetPath);
        }
    }

    
    private static void CheckAndTrashBlendFile(string assetPath)
    {
        if (IsBlendFile(assetPath))
        {
            try 
            {
                
                bool result = AssetDatabase.MoveAssetToTrash(assetPath);
                
                if (result)
                {
                    Debug.Log($"Blend backup file moved to trash immediately upon adding to project: {assetPath}");
                }
                else
                {
                    Debug.LogWarning($"Blend backup file could not be moved to trash: {assetPath}");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error occurred while moving blend file to trash: {assetPath}\nError: {e.Message}");
            }
        }
    }

    
    public static bool IsBlendFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return false;

        return BlendBackupFileRegex.IsMatch(filePath);
    }
}


[InitializeOnLoad]
public class BlendFileStartupCleaner
{
    static BlendFileStartupCleaner()
    {
        
        EditorApplication.update += CheckExistingBlendFiles;
    }

    private static void CheckExistingBlendFiles()
    {
        EditorApplication.update -= CheckExistingBlendFiles;
        CleanExistingBlendFiles();
    }

    private static void CleanExistingBlendFiles()
    {
        string[] assets = AssetDatabase.FindAssets("", new[] { "Assets" });
        int cleanedCount = 0;
        
        foreach (var guid in assets)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string extension = Path.GetExtension(path);
            
            if (BlendFileTrashProcessor.IsBlendFile(path))
            {
                bool result = AssetDatabase.MoveAssetToTrash(path);
                if (result)
                {
                    cleanedCount++;
                }
            }
        }

        if (cleanedCount > 0)
        {
            Debug.Log($"Cleaned {cleanedCount} .blend files on Unity startup.");
        }
    }
}