Shader "Custom/SpiraleBillboarding"
{
    Properties
    {
         _MainTex("Sprite Texture", 2D) = "white" {}
         _Color("Color", Color) = (1,1,1,1)
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "RenderType" = "Transparent"
                "RenderPipeline" = "UniversalPipeline"
                "DisableBatching" = "True"
            }

            Pass
            {
                Blend SrcAlpha OneMinusSrcAlpha
                ZWrite Off
                Cull Off

                HLSLPROGRAM

                #pragma vertex vert
                #pragma fragment frag

                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                struct Attributes
                {
                    float3 positionOS : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct Varyings
                {
                    float4 positionCS : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);

                CBUFFER_START(UnityPerMaterial)
                    float4 _MainTex_ST;
                    float4 _Color;
                CBUFFER_END

                Varyings vert(Attributes IN)
                {
                    Varyings OUT;

                    float3 center = TransformObjectToWorld(float3(0, 0, 0));

                    float3 cameraRight = UNITY_MATRIX_I_V._m00_m10_m20;
                    float3 cameraUp = UNITY_MATRIX_I_V._m01_m11_m21;

                    float3 objectScale = float3(
                        length(unity_ObjectToWorld._m00_m10_m20),
                        length(unity_ObjectToWorld._m01_m11_m21),
                        length(unity_ObjectToWorld._m02_m12_m22)
                    );

                    float3 worldPos =
                        center
                        + cameraRight * IN.positionOS.x * objectScale.x
                        + cameraUp * IN.positionOS.y * objectScale.y;

                    OUT.positionCS = TransformWorldToHClip(worldPos);
                    OUT.uv = IN.uv;

                    return OUT;
                }

                half4 frag(Varyings IN) : SV_Target
                {
                    half4 sprite = SAMPLE_TEXTURE2D(
                        _MainTex,
                        sampler_MainTex,
                        IN.uv
                    );

                    return sprite * _Color;
                }

                ENDHLSL
            }
        }
}