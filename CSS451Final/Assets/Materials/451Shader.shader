﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/451Shader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float3 vertexWC : TEXCOORD3;
            };

            sampler2D _MainTex;

            float4 LightPosition;
            fixed4 LightColor;
            float  LightNear;
            float  LightFar;
            float4x4 MyXformMat;
            v2f vert(appdata v)
            {
                v2f o;
                
                o.vertex = mul(MyXformMat, v.vertex);
                o.vertexWC = o.vertex;
                o.vertex = mul(UNITY_MATRIX_VP, o.vertex);

                o.uv = v.uv;

                float3 p = v.vertex + 10 * v.normal;
                p = mul(UNITY_MATRIX_M, p);
                o.normal = normalize(p - o.vertexWC); 
                
                return o;
            }

            // our own function
            float ComputeDiffuse(v2f i) {
                float3 l = LightPosition - i.vertexWC;
                float d = length(l);
                l = l / d;
                float strength = 1;

                float ndotl = clamp(dot(i.normal, l), 0, 1);
                if (d > LightNear) {
                    if (d < LightFar) {
                        float range = LightFar - LightNear;
                        float n = d - LightNear;
                        strength = smoothstep(0, 1, 1.0 - (n*n) / (range*range));
                    }
                    else {
                        strength = 0;
                    }
                }
                return ndotl * strength;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // return fixed4(i.normal, 1.0);
                    // This is to verify the value of the normal vector

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                float diff = ComputeDiffuse(i);
                return col * diff * LightColor;
            }

            ENDCG
        }
    }
}