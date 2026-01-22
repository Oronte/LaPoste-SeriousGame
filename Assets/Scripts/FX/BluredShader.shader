Shader "Custom/SimpleBlurURP"
{
    Properties 
    {
        _BlurSize ("Force du Flou", Range(0, 0.1)) = 0.005
    }

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" }
        ZTest Always ZWrite Off Cull Off

        Pass
        {
            Name "BlurPass"
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl" 

            float _BlurSize;
            TEXTURE2D(_CameraColorTexture);
            SAMPLER(sampler_CameraColorTexture);

            struct Attributes { uint vertexID : SV_VertexID; };
            struct Varyings { float4 positionCS : SV_POSITION; float2 uv : TEXCOORD0; };

            Varyings Vert(Attributes input)
            {
                Varyings output;
                output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
                output.uv = GetFullScreenTriangleTexCoord(input.vertexID);
                return output;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                float2 uv = input.uv;
                float2 off = float2(_BlurSize, _BlurSize); 

                half4 col = SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, uv);
                col += SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, uv + float2(off.x, 0));
                col += SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, uv - float2(off.x, 0));
                col += SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, uv + float2(0, off.y));
                col += SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, uv - float2(0, off.y));

                return col / 5.0;
            }
            ENDHLSL
        }
    }
}