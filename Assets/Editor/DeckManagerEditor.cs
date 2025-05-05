using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(DrawpileManager))]
public class DrawPileManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DrawpileManager drawPileManager = (DrawpileManager)target;
        if(GUILayout.Button("Draw Next Card"))
        {
            HandManager handManager = FindFirstObjectByType<HandManager>();
            if(handManager != null)
            {
                drawPileManager.DrawCard(handManager);
            }
        }
    }
}
#endif
