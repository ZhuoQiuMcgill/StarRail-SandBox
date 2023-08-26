Shader "Custom/VoronoiShaderWithTexture"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _PositionTex("Position Texture", 2D) = "white" {}
        _ColorTex("Color Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "Queue" = "Transparent" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _PositionTex;
            sampler2D _ColorTex;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float2 pixelPos = i.uv;
                float minDist = 10000.0;
                float4 color = float4(0,0,0,1);

                // 寻找最近的点并取得其颜色
                for (int j = 0; j < 800; j++) {
                    float2 starPos = tex2D(_PositionTex, float2(j / 800.0, 0.5)).xy;
                    float dist = distance(starPos, pixelPos);

                    if (dist < minDist) {
                        minDist = dist;
                        color = tex2D(_ColorTex, float2(j / 800.0, 0.5));
                    }
                }

                return color;
            }
            ENDCG
        }
    }
}
