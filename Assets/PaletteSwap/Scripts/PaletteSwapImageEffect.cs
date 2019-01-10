using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PaletteSwapImageEffect : MonoBehaviour
{
    public ColorPalette ColorPalette;
    private Material mat;

    void OnEnable()	{
		if(mat != null){ return; }
        Shader shader = Shader.Find("Hidden/PaletteSwap");
        mat = new Material(shader);
        // Disable default blending set in shader
        mat.SetInt("_BlendSrcMode", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_BlendDstMode", (int)UnityEngine.Rendering.BlendMode.Zero);
        if(ColorPalette==null){ return; }
        ColorPalette.ApplyColorPalette(mat);
	}

	void OnDisable() {
		if (mat == null){ return; }
        DestroyImmediate(mat);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst) {
        //if(ColorPalette == null) { return; }
        if(ColorPalette) {
            ColorPalette.ApplyColorPalette(mat);
        }
		Graphics.Blit(src, dst, mat);
	}

}
