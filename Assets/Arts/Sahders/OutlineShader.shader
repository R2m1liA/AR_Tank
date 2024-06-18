Shader "Unlit/OutlineShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineWidth ("Outline Width", Range (0.0, 2)) = 0.01
        _OutlineColor ("Outline Color", Color) = (0.5,0.5,0.5,1)
        
        [Toggle(_USE_TANGENT)]_UseSmoothNormal ("Use Smooth Normal", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
        LOD 100
        Cull Front

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma shader_feature _USE_TANGENT

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Atributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };

            CBUFFER_START(UnityPerMaterial)
            real4 _OutlineColor;
            real _OutlineWidth;
            CBUFFER_END

            Varyings vert(Atributes v)
            {
                Varyings o = (Varyings)0;
                o.positionCS = mul(UNITY_MATRIX_MV, v.positionOS);
                #if _USE_TANGENT
                float3 normal = TransformObjectToWorldNormal(v.tangentOS.xyz);
                #else
                float3 normal = TransformObjectToWorldNormal(v.normalOS);
                #endif

                normal = normalize(normal);
                float fov = 1 / (unity_CameraProjection[1].y);
                float depth = lerp(1, abs(o.positionCS.z), - unity_CameraProjection[3].z);
                float width = _OutlineWidth * (depth * fov);
                o.positionCS.xy += normalize(normal.xy) * width;
                o.positionCS = mul(UNITY_MATRIX_P, o.positionCS);
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                return _OutlineColor;
            }

            ENDHLSL
        }
    }
}
