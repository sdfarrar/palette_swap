using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Texture Palette", menuName="Palettes/Texture Palette")]
public class TextureColorPalette : ColorPalette {

    public Texture2D PaletteTexture;
    [Range(1, 8)]
    public int PixelsPerSwatch = 1;

    private void OnEnable() {
        GenerateColors();
    }

    public void GenerateColors() {
        if(!PaletteTexture) { return; }

        activeSwatches = UpdateColors();
        ClearColors();

    }

    private int UpdateColors() {
        int index = 0;
        Color[] pixels = PaletteTexture.GetPixels();
        for(int i=0; i<pixels.Length; i+=PixelsPerSwatch){
            colors[index++] = pixels[i];
        }
        return index;

    }

    private void ClearColors() {
        for(int i=activeSwatches; i<colors.Length; ++i){
            colors[i] = Color.clear;
        }
    }

}
