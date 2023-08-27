Shader "Unlit/Tshader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _PositionTex("Position Texture", 2D) = "white" {}
        _ColorTex("Color Texture", 2D) = "white" {}
        _Radius("Radius", float) = 0.0025
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

            sampler2D _PositionTex;
            sampler2D _ColorTex;
            float _Radius;

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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 pixelPos = i.uv;
                float minDist = _Radius;
                float4 color = float4(0,0,0,1); // Default to black

                for (int j = 0; j < 800; j++) 
                {
                    float2 starPos = tex2D(_PositionTex, float2(j / 800.0, 0.5)).xy;
                    float dist = distance(starPos, pixelPos);
                    if (dist < minDist)
                    {
                        color = tex2D(_ColorTex, float2(j / 800.0, 0.5));
                        minDist = dist;
                    }
                }
                return color;
            }
            ENDCG
        }
    }
}
