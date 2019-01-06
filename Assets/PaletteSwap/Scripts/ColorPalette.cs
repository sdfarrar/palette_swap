using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Color Palette", menuName="Palettes/Color Palette")]
public class ColorPalette : ScriptableObject {

    public const int MAX_COLORS = 16;

#if UNITY_EDITOR
    public SwatchTemplate SwatchTemplate;
#endif

    [SerializeField]
    protected Color[] colors = new Color[MAX_COLORS];

    [SerializeField]
    [Range(0, MAX_COLORS)]
    protected int activeSwatches = 0;

    public void ApplyColorPalette(Material mat) {
        SetMaterialMatrix(mat);
    }

    private void SetMaterialMatrix(Material mat) {
        if(!mat){ return; }
        mat.SetInt("_ColorArrayCount", activeSwatches);
        mat.SetColorArray("_ColorArray", colors);
    }

    void OnValidate() {
        if(colors.Length == 16) { return; }
        Color[] newColors = new Color[16];
        for(int i=0; i<colors.Length; ++i) {
            newColors[i] = colors[i];
        }
        colors = newColors;
    }

}
