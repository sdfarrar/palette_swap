using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorPalette))]
public class ColorPaletteEditor : AbstractPaletteEditor {

    ColorPalette palette;

    GUILayoutOption[] buttonOptions = new GUILayoutOption[] { GUILayout.Width(125), GUILayout.Height(50) };

    void OnEnable() {
        Initialize();
        palette = (ColorPalette)target;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(activeSwatchesProp);
        EditorGUILayout.PropertyField(swatchTemplateProp);
        EditorGUILayout.Space();
        DrawColorArray(palette);

        if(EditorGUI.EndChangeCheck()) {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(palette);
        }
    }

}
