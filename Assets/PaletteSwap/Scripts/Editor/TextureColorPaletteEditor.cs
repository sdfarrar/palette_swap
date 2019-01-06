using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextureColorPalette))]
public class TextureColorPaletteEditor : AbstractPaletteEditor {

    TextureColorPalette palette;

    SerializedProperty paletteTextureProp;

    GUIContent paletteTextureLabel = new GUIContent("Palette Texture");

    void OnEnable() {
        Initialize();
        palette = (TextureColorPalette)target;
        paletteTextureProp = serializedObject.FindProperty("PaletteTexture");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        DrawTextureSection();
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(swatchTemplateProp);
        DrawColorArray(palette);

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

}
