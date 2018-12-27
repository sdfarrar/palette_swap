using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class ApplyColorPalette : MonoBehaviour {

    private readonly string AppendShaderName = "Hidden/PaletteSwap";
    private readonly string SpriteShaderName = "Sprite/PaletteSwap";

    public ColorPalette ColorPalette;
    public bool AppendToMaterialList;
    private new SpriteRenderer renderer;

    private Material[] materials {
        get {
            return new Material[]{
                new Material(Shader.Find("Sprites/Default")),
                new Material(Shader.Find(AppendShaderName)),
            };
        }
    }

    private Material _mat;
    [SerializeField]
    private Material mat {
        get{
            if(!_mat){
                _mat = (AppendToMaterialList) 
                    ? new Material(Shader.Find(AppendShaderName))
                    : new Material(Shader.Find(SpriteShaderName));
            }
            return _mat;
        }
        set{_mat = value;}
    }

    private int materialIndex = 0;

    void Awake() {
        renderer = GetComponent<SpriteRenderer>();
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
        Apply();
    }
#endif

    private void AppendMaterials() {
        if(!renderer || !AppendToMaterialList){ return; }
#if UNITY_EDITOR
        Material[] materials = (Material[])renderer.sharedMaterials.Clone();
        renderer.sharedMaterials = CreateNewMaterialList(materials);
#else
        Material[] materials = renderer.materials;
        renderer.materials = CreateNewMaterialList(materials);
#endif
    }

    private Material[] CreateNewMaterialList(Material[] list) {
        bool contains = list.Contains(mat);
        if(list.Contains(mat)){ return list; }

        materialIndex = list.Length;
        Material[] newList = new Material[list.Length+1];
        for(int i=0; i<list.Length; ++i){
            newList[i] = list[i];
        }
        newList[materialIndex] = mat;
        return newList;
    }

    private void Apply() {
        if(!this.enabled || !this.renderer || !ColorPalette){ return; } 
        if(AppendToMaterialList){
            AppendMaterials();
#if UNITY_EDITOR
            ColorPalette.ApplyColorPalette(renderer.sharedMaterials[materialIndex]);
#else
            ColorPalette.ApplyColorPalette(renderer.materials[materialIndex]);
#endif

        }else{
#if UNITY_EDITOR
            ColorPalette.ApplyColorPalette(renderer.sharedMaterials[materialIndex]);
#else
            ColorPalette.ApplyColorPalette(renderer.materials[materialIndex]);
#endif
        }
    }

}
