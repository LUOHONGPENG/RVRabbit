using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(SoundMgr))]
public class SoundMgrEditor : Editor
{
    float time = 0;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();

        SoundMgr myScript = (SoundMgr)target;

        time = EditorGUILayout.FloatField("Time", time);

        if (GUILayout.Button("PlayVoice"))
        {
            myScript.PlaySoundTime(myScript.testSoundType, time);
        }
    }
}