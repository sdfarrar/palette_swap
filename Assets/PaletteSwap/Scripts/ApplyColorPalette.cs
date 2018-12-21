using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class ApplyColorPalette : MonoBehaviour {

    public ColorPalette ColorPalette;
    public bool AppendToMaterialList;
    private new SpriteRenderer renderer;

    private Material[] materials {
        get {
            return new Material[]{
                new Material(Shader.Find("Sprites/Default")),
                new Material(Shader.Find("Hidden/PaletteSwap")),
            };
        }
    }

    private Material _mat;
    private Material mat {
        get{
            if(!_mat){ _mat = new Material(Shader.Find("Sprite/PaletteSwap")); }
            return _mat;
        }
    }

    void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        AppendMaterials();
        Apply();
    }

    void OnEnable() {
        Apply();
    }

    void Update() {
#if UNITY_EDITOR
        Apply();
#endif
    }

#if UNITY_EDITOR
    void OnValidate() {
        AppendMaterials();
        Apply();
    }
#endif

    private void AppendMaterials() {
        if(!renderer || !AppendToMaterialList){ return; }
#if UNITY_EDITOR
        renderer.sharedMaterials = materials;
#else
        renderer.materials = materials;
#endif
    }

    private void Apply() {
        if(!this.enabled || !this.renderer || !ColorPalette){ return; } 
        if(AppendToMaterialList){
#if UNITY_EDITOR
            ColorPalette.ApplyColorPalette(renderer.sharedMaterials[1]);
#else
            ColorPalette.ApplyColorPalette(renderer.materials[1]);
#endif

        }else{
            ColorPalette.ApplyColorPalette(renderer.sharedMaterials[0]);
        }
    }

}
