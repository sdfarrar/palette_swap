using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextureColorPalette))]
public class TextureColorPaletteEditor : Editor {

    TextureColorPalette palette;

    SerializedProperty paletteTextureProp;
    SerializedProperty colorArrayProp;

    GUIContent paletteTextureLabel = new GUIContent("Palette Texture");
    GUIContent colorArrayLabel = new GUIContent("Colors");

    void OnEnable() {
        palette = (TextureColorPalette)target;
        paletteTextureProp = serializedObject.FindProperty("PaletteTexture");
        colorArrayProp = serializedObject.FindProperty("colors");
    }

    public override void OnInspectorGUI() {

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        DrawTextureSection();
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
        //EditorGUILayout.PropertyField(colorArrayProp, colorArrayLabel, true);
        //EditorGUILayout.IntField("Colors", colorArrayProp.arraySize);
        //for(int i=0; i<colorArrayProp.arraySize; ++i){
        //    Color c = colorArrayProp.GetArrayElementAtIndex(i).colorValue;
        //    EditorGUILayout.ColorField($"Swatch {i}", c);
        //}
        
    }

}
