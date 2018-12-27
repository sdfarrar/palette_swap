using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Color Palette", menuName="Palettes/Color Palette")]
public class ColorPalette : ScriptableObject {

    [SerializeField]
    protected Color[] colors = new Color[16];

    [SerializeField]
    [Range(0, 16)]
    protected int activeSwatches = 0;

    public void ApplyColorPalette(Material mat) {
        SetMaterialMatrix(mat);
    }

    private void SetMaterialMatrix(Material mat) {
        if(!mat){ return; }
        mat.SetInt("_ColorArrayCount", activeSwatches);
        mat.SetColorArray("_ColorArray", colors);
    }

}
