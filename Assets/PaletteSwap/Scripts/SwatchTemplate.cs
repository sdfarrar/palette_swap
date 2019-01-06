using UnityEngine;

#if UNITY_EDITOR
[CreateAssetMenu(fileName = "SwatchTemplate", menuName = "Palettes/Swatch Template", order = 99)]
public class SwatchTemplate : ScriptableObject {

    [SerializeField]
    private string[] _swatchNames = new string[ColorPalette.MAX_COLORS];
    public string[] SwatchNames { get => _swatchNames; }

    [SerializeField]
    [Range(0, ColorPalette.MAX_COLORS)]
    private int _activeSwatches = 0;
    public int ActiveSwatches { get => _activeSwatches; }
}
#endif
