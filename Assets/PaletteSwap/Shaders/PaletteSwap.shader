Shader "Hidden/PaletteSwap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            int _ColorArrayCount = 0;
            half4 _ColorArray[16];

            fixed4 MatrixPaletteSwap(float2 uv) {
				fixed4 c = tex2D(_MainTex, uv);
                float x = c.r;
                float index = clamp(x*_ColorArrayCount, 0, _ColorArrayCount-1);
				return _ColorArray[index] * c.a;
            }

			fixed4 frag(v2f IN) : SV_Target {
                fixed4 c = MatrixPaletteSwap(IN.uv);// * IN.color;
				return c;
			}
            ENDCG
        }
    }
}
