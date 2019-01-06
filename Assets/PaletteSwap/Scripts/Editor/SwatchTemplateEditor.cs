using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SwatchTemplate))]
public class SwatchTemplateEditor : Editor {

    SwatchTemplate template;

    SerializedProperty activeSwatchesProp;
    SerializedProperty swatchNamesProp;

    GUILayoutOption[] options = new GUILayoutOption[] { };

    void OnEnable() {
        template = (SwatchTemplate)target;
        swatchNamesProp = serializedObject.FindProperty("_swatchNames");
        activeSwatchesProp = serializedObject.FindProperty("_activeSwatches");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(activeSwatchesProp);
        EditorGUILayout.Space();
        DrawNamesArray();

        if(EditorGUI.EndChangeCheck()) {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(template);
        }
    }

    private void DrawNamesArray() {
        for(int i = 0; i < activeSwatchesProp.intValue; ++i) {
            string st = swatchNamesProp.GetArrayElementAtIndex(i).stringValue;
            swatchNamesProp.GetArrayElementAtIndex(i).stringValue = EditorGUILayout.TextField($"Swatch {i} Name", st, options);
        }
    }

}