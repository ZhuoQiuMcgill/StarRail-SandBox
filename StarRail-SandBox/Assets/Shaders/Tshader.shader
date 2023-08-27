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
                    return fixed4(0,0,0,1); // ������Ұ�ڣ�ֱ�ӷ��غ�ɫ��͸��
                }

                float2 currentPoint = i.uv; // ��ǰ���ص�UV����
                float minDistance = 1.0; // ��ʼ����С����Ϊ1�������룩
                float4 nearestColor = float4(1, 1, 1, 1); // ��ʼ���������ɫΪ��ɫ


                // ѭ�������������ӵ�
                for (int j = 0; j < 800; j++)
                {
                    float2 seed = tex2D(_PositionTex, float2((j + 0.5) / 800.0, 0.5)).xy; // �������л�ȡ���ӵ�����
                    float4 seedColor = tex2D(_ColorTex, float2((j + 0.5) / 800.0, 0.5)); // �������л�ȡ���ӵ���ɫ

                    float distToSeed = distance(seed, currentPoint); // ʹ�����ú����������

                    // �������������������������С����
                    if (distToSeed < minDistance)
                    {
                        minDistance = distToSeed;
                        nearestColor = seedColor;
                    }
                }

                return nearestColor; // ������������ɫ
            }

            ENDCG
        }
    }
}
