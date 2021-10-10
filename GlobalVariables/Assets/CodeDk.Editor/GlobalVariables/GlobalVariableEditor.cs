using UnityEditor;
using UnityEngine;

namespace CodeDk
{
    [CustomEditor(typeof(GlobalVariable), true)]
    public class GlobalVariableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GlobalVariable targetVariable = (GlobalVariable)target;

            EditorGUI.BeginChangeCheck();

            DrawDefaultInspector();

            if (EditorGUI.EndChangeCheck() && Application.isPlaying)
            {
                EditorApplication.delayCall += targetVariable.RaiseChangedEvent;
            }
        }

    }
}
