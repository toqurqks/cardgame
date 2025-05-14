#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class DeepMissingScriptScanner
{
    [MenuItem("Tools/Deep Scan for Missing Scripts")]
    public static void ScanAllAssetsAndScene()
    {
        int totalMissing = 0;

        // 1. 현재 열린 씬의 모든 GameObject
        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>(true))
        {
            Component[] components = go.GetComponents<Component>();
            foreach (Component c in components)
            {
                if (c == null)
                {
                    Debug.LogWarning($"[Scene] Missing script in: {GetPath(go)}", go);
                    totalMissing++;
                }
            }
        }

        // 2. 프로젝트 내 프리팹들
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            Component[] components = prefab.GetComponentsInChildren<Component>(true);
            foreach (Component c in components)
            {
                if (c == null)
                {
                    Debug.LogWarning($"[Prefab] Missing script in: {path}", prefab);
                    totalMissing++;
                    break;
                }
            }
        }

        if (totalMissing == 0)
            Debug.Log(" No missing scripts found.");
        else
            Debug.Log($" Found {totalMissing} missing scripts.");
    }

    private static string GetPath(GameObject obj)
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
