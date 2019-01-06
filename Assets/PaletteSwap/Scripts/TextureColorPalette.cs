using UnityEngine;

[CreateAssetMenu(fileName="New Texture Palette", menuName="Palettes/Texture Palette")]
public class TextureColorPalette : ColorPalette {

    [Tooltip("Palette texture. Assumes texture is 1 pixel tall.")]
    public Texture2D PaletteTexture;

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
        int length = Mathf.Min(pixels.Length, MAX_COLORS);
        for(int i=0; i<length; ++i){
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
