using UnityEngine;
using UnityEditor;

public abstract class AbstractPaletteEditor : Editor {

    protected SerializedProperty colorArrayProp;
    protected SerializedProperty activeSwatchesProp;
    protected SerializedProperty swatchTemplateProp;

    GUILayoutOption[] buttonOptions = new GUILayoutOption[] { GUILayout.Width(125), GUILayout.Height(50) };

    void OnEnable() {
    }

    protected void Initialize() {
        colorArrayProp = serializedObject.FindProperty("colors");
        activeSwatchesProp = serializedObject.FindProperty("activeSwatches");
        swatchTemplateProp = serializedObject.FindProperty("SwatchTemplate");
    }

    protected void DrawColorArray(ColorPalette palette) {
        for(int i = 0; i < activeSwatchesProp.intValue; ++i) {
            Color c = colorArrayProp.GetArrayElementAtIndex(i).colorValue;
            string name = GetSwatchName(palette, i);
            colorArrayProp.GetArrayElementAtIndex(i).colorValue = EditorGUILayout.ColorField(name, c);
        }
    }

    protected string GetSwatchName(ColorPalette palette, int index) {
        if(palette.SwatchTemplate == null || palette.SwatchTemplate.ActiveSwatches <= index) { return $"Swatch {index}"; }
        return palette.SwatchTemplate.SwatchNames[index];
    }

}
