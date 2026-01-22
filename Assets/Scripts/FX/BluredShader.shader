Shader "Custom/SimpleBlurURP"
{
    Properties 
    {
        _BlurSize ("Force du Flou", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalRenderPipeline" "RenderType"="Opaque" }
        ZWrite Off Cull Off

        Pass
        {
            Name "BlurPass"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl" 

            float _BlurSize;

            TEXTURE2D(_BlitTexture);
            SAMPLER(sampler_BlitTexture);

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                float2 offX = float2(_BlurSize, 0);
                float2 offY = float2(0, _BlurSize * _ScreenParams.x / _ScreenParams.y); 

                half4 col = SAMPLE_TEXTURE2D(_BlitTexture, sampler_BlitTexture, IN.uv);

                col += SAMPLE_TEXTURE2D(_BlitTexture, sampler_BlitTexture, IN.uv + offX);
                col += SAMPLE_TEXTURE2D(_BlitTexture, sampler_BlitTexture, IN.uv - offX);
                col += SAMPLE_TEXTURE2D(_BlitTexture, sampler_BlitTexture, IN.uv + offY);
                col += SAMPLE_TEXTURE2D(_BlitTexture, sampler_BlitTexture, IN.uv - offY);

                return col / 5.0;
            }
            ENDHLSL
        }
    }
}