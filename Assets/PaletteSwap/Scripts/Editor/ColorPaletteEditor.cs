using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(ColorPalette))]
public class ColorPaletteEditor : Editor {

    ColorPalette palette;

    SerializedProperty colorArrayProp;
    SerializedProperty activeSwatchesProp;

    GUILayoutOption[] buttonOptions = new GUILayoutOption[]{GUILayout.Width(125), GUILayout.Height(50)};

    int minActiveSwatches;
    int maxActiveSwatches;

    void OnEnable() {
        palette = (ColorPalette)target;
        colorArrayProp = serializedObject.FindProperty("colors");
        activeSwatchesProp = serializedObject.FindProperty("activeSwatches");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(activeSwatchesProp);
        EditorGUILayout.Space();
        DrawColorArray();

        if(EditorGUI.EndChangeCheck()){
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(palette);
        }
    }

    private void DrawColorArray() {

        for(int i=0; i<activeSwatchesProp.intValue; ++i){
            Color c = colorArrayProp.GetArrayElementAtIndex(i).colorValue;
            colorArrayProp.GetArrayElementAtIndex(i).colorValue = EditorGUILayout.ColorField($"Swatch {i}", c);
        }
        
    }

    private void DrawAddButton() {
        Debug.Log(colorArrayProp.arraySize);
        if(GUILayout.Button("Add Swatch", buttonOptions)){
            ++activeSwatchesProp.intValue;
        }
    }

    private void DrawRemoveButton() {
        if(GUILayout.Button("Remove Swatch", buttonOptions) && activeSwatchesProp.intValue>0){
            --activeSwatchesProp.intValue;
        }

    }

}
