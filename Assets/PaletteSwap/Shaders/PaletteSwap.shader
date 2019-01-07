Shader "Hidden/PaletteSwap"
{
    Properties
    {
        _Color ("Tint", Color) = (1,1,1,1)
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}
        [HideInInspector]_BlendSrcMode ("BlendSrcMode", Float) = 1  // One
        [HideInInspector]_BlendDstMode ("BlendDstMode", Float) = 10 // OneMinusSrcAlpha
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
        ZTest Always
        Blend [_BlendSrcMode] [_BlendDstMode]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                half4 color : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            int _ColorArrayCount = 0;
            half4 _ColorArray[16];
            half4 _Color;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = _Color; // comment out for neato effect
                return o;
            }

            fixed4 MatrixPaletteSwap(float2 uv) {
				fixed4 c = tex2D(_MainTex, uv);
                float x = c.r;
                float index = clamp(x*_ColorArrayCount, 0, _ColorArrayCount-1);
				return _ColorArray[index] * c.a;
            }

			fixed4 frag(v2f IN) : SV_Target {
                fixed4 c = MatrixPaletteSwap(IN.uv);
				return c * IN.color;
			}
            ENDCG
        }
    }
}
