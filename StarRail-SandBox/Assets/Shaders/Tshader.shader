Shader "Unlit/Tshader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _PositionTex("Position Texture", 2D) = "white" {}
        _ColorTex("Color Texture", 2D) = "white" {}
        _Radius("Radius", float) = 0.0025

        _TopLeft("TopLeft", Vector) = (0,0,0,0)
        _TopRight("TopRight", Vector) = (0,0,0,0)
        _BottomLeft("BottomLeft", Vector) = (0,0,0,0)
        _BottomRight("BottomRight", Vector) = (0,0,0,0)
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
            sampler2D _PositionTex;
            sampler2D _ColorTex;
            float _Radius;
            float4 _MainTex_ST;

            float4 _TopLeft;
            float4 _TopRight;
            float4 _BottomLeft;
            float4 _BottomRight;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                if (i.uv.x < _TopLeft.x || i.uv.x > _TopRight.x || i.uv.y > _TopLeft.y || i.uv.y < _BottomRight.y)
                {
                    return fixed4(0,0,0,1); // 不在视野内，直接返回黑色或透明
                }

                float2 currentPoint = i.uv; // 当前像素的UV坐标
                float minDistance = 1.0; // 初始化最小距离为1（最大距离）
                float4 nearestColor = float4(1, 1, 1, 1); // 初始化最近点颜色为白色


                // 循环遍历所有种子点
                for (int j = 0; j < 800; j++)
                {
                    float2 seed = tex2D(_PositionTex, float2((j + 0.5) / 800.0, 0.5)).xy; // 从纹理中获取种子点坐标
                    float4 seedColor = tex2D(_ColorTex, float2((j + 0.5) / 800.0, 0.5)); // 从纹理中获取种子点颜色

                    float distToSeed = distance(seed, currentPoint); // 使用内置函数计算距离

                    // 如果这个点更近，更新最近点和最小距离
                    if (distToSeed < minDistance)
                    {
                        minDistance = distToSeed;
                        nearestColor = seedColor;
                    }
                }

                return nearestColor; // 返回最近点的颜色
            }

            ENDCG
        }
    }
}
