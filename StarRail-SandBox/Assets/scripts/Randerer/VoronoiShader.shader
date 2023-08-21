Shader "Custom/VoronoiShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }

        SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            #define MAX_VERTEX_COUNT 800 // 根据你的需要调整

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

            float4 _Positions[MAX_VERTEX_COUNT];
            float4 _Colors[MAX_VERTEX_COUNT];
            float4x4 _WorldToCameraMatrix;
            float4x4 _ProjectionMatrix;

            sampler2D _MainTex;

            v2f vert(appdata v)
            {
                v2f o;
                float4 worldPos = float4(v.vertex.xyz, 1);
                float4 viewPos = mul(_WorldToCameraMatrix, worldPos);
                o.vertex = mul(_ProjectionMatrix, viewPos);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 screenPos = i.uv;
                float4 chosenColor = float4(1,1,1,1);
                float minDistance = 1e10;

                for (int j = 0; j < MAX_VERTEX_COUNT; j++)
                {
                    float2 vertexPos = _Positions[j].xy;
                    float distToVertex = distance(screenPos, vertexPos);

                    if (distToVertex < minDistance)
                    {
                        minDistance = distToVertex;
                        chosenColor = _Colors[j];
                    }
                }

                fixed4 col = tex2D(_MainTex, i.uv);
                return col * chosenColor;
            }
            ENDCG
        }
    }
}
