using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextureColorPalette))]
public class TextureColorPaletteEditor : Editor {

    TextureColorPalette palette;

    SerializedProperty paletteTextureProp;
    SerializedProperty colorArrayProp;
    SerializedProperty activeSwatchesProp;

    GUIContent paletteTextureLabel = new GUIContent("Palette Texture");
    GUIContent colorArrayLabel = new GUIContent("Swatches");

    void OnEnable() {
        palette = (TextureColorPalette)target;
        paletteTextureProp = serializedObject.FindProperty("PaletteTexture");
        colorArrayProp = serializedObject.FindProperty("colors");
        activeSwatchesProp = serializedObject.FindProperty("activeSwatches");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        DrawTextureSection();
        EditorGUILayout.Space();
        DrawColorArray();

        if(EditorGUI.EndChangeCheck()){
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(palette);
        }
    }

    private void DrawTextureSection() {
        EditorGUILayout.PropertyField(paletteTextureProp, paletteTextureLabel);
        if(GUILayout.Button("Generate From Texture")) {
            palette.GenerateColors();
        }

        if(paletteTextureProp.objectReferenceValue!=palette.PaletteTexture){
            palette.GenerateColors();
        }
    }

    private void DrawColorArray() {
        for(int i=0; i<activeSwatchesProp.intValue; ++i){
            Color c = colorArrayProp.GetArrayElementAtIndex(i).colorValue;
            colorArrayProp.GetArrayElementAtIndex(i).colorValue = EditorGUILayout.ColorField($"Swatch {i}", c);
        }
    }

}
