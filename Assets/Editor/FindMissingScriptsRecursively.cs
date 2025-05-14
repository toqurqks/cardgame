#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class FindMissingScriptsRecursively : MonoBehaviour
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    public static void FindMissingScripts()
    {
        GameObject[] gos = GameObject.FindObjectsOfType<GameObject>();
        int go_count = 0, components_count = 0, missing_count = 0;

        foreach (GameObject go in gos)
        {
            go_count++;
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                components_count++;
                if (components[i] == null)
                {
                    missing_count++;
                    string path = GetHierarchyPath(go);
                    Debug.LogWarning($"Missing script in: {path}", go);
                }
            }
        }

        Debug.Log($"Searched {go_count} GameObjects, {components_count} components, found {missing_count} missing scripts.");
    }

    private static string GetHierarchyPath(GameObject obj)
    {
        string path = obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = obj.name + "/" + path;
        }
        return path;
    }
}
#endif