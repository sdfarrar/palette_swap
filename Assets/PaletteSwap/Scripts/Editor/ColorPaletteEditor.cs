using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorPalette))]
public class ColorPaletteEditor : Editor {

    ColorPalette palette;

    SerializedProperty colorArrayProp;

    GUIContent colorArrayLabel = new GUIContent("Colors");

    void OnEnable() {
        palette = (ColorPalette)target;
        colorArrayProp = serializedObject.FindProperty("colors");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        DrawColorArray();
        DrawAddButton();
        DrawRemoveButton();

        if(EditorGUI.EndChangeCheck()){
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(palette);
        }
    }

    private void DrawColorArray() {

        for(int i=0; i<colorArrayProp.arraySize; ++i){
            Color c = colorArrayProp.GetArrayElementAtIndex(i).colorValue;
            colorArrayProp.GetArrayElementAtIndex(i).colorValue = EditorGUILayout.ColorField($"Swatch {i}", c);
        }
        
    }

    private void DrawAddButton() {
        if(GUILayout.Button("Add Swatch")){
            ++colorArrayProp.arraySize;
        }
    }

    private void DrawRemoveButton() {
        if(GUILayout.Button("Remove Swatch")){
            --colorArrayProp.arraySize;
        }

    }

}
