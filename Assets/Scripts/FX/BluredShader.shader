Shader "Custom/BluredShader"
{
    Properties 
    {
        _BlurSize ("Blur Size", Float) = 0.005
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalRenderPipeline" }

        Pass
        {
            Name "BlurPass"

            HLSLPROGRAM
            float _BlurSize;

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"


            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);


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
                float2 offset = float2(_BlurSize, 0);

                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * 0.4;
                col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + offset) * 0.3;
                col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv - offset) * 0.3;

                return col;
            }
            ENDHLSL
        }
    }
}
