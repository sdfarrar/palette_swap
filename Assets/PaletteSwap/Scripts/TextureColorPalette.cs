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
    
        Color[] pixels = PaletteTexture.GetPixels();
        colors = new List<Color>();
        for(int i=0; i<pixels.Length; i+=PixelsPerSwatch){
            colors.Add(pixels[i]);
        }
    }

}
