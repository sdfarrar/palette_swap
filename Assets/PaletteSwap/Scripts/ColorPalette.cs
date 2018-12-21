using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Color Palette", menuName="Palettes/Color Palette")]
public class ColorPalette : ScriptableObject {

    [SerializeField]
    protected List<Color> colors;

    public void ApplyColorPalette(Material mat) {
        SetMaterialMatrix(mat);
    }

    private void SetMaterialMatrix(Material mat) {
        if(!mat){ return; }
        mat.SetInt("_ColorArrayCount", colors.Count);
        mat.SetColorArray("_ColorArray", colors.ToArray());
    }

}
