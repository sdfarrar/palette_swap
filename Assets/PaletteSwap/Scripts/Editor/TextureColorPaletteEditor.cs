using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextureColorPalette))]
public class TextureColorPaletteEditor : AbstractPaletteEditor {

    TextureColorPalette palette;

    SerializedProperty paletteTextureProp;

    GUIContent paletteTextureLabel = new GUIContent("Palette Texture");
    bool isTextureReadable;

    void OnEnable() {
        Initialize();
        palette = (TextureColorPalette)target;
        paletteTextureProp = serializedObject.FindProperty("PaletteTexture");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        isTextureReadable = (palette.PaletteTexture!=null) ? palette.PaletteTexture.isReadable : false;

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
        DrawNotReadableWarning();

        GUI.enabled = isTextureReadable;
        if(GUILayout.Button("Generate From Texture")) {
            palette.GenerateColors();
        }
        GUI.enabled = true;

        if(isTextureReadable && paletteTextureProp.objectReferenceValue!=palette.PaletteTexture){
            palette.GenerateColors();
        }
    }

    private void DrawNotReadableWarning() {
        if(isTextureReadable){ return; }
        EditorGUILayout.HelpBox("Texture is not readable. Tick the \"Read/Write Enabled\" checkbox in the Texture Settings.", MessageType.Warning);
    }

}
