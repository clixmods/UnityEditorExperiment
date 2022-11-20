
Shader "Example/LitEmissiveScroll"
{
    // The properties block of the Unity shader. In this example this block is empty
    // because the output color is predefined in the fragment shader code.
    Properties
    { 
        [HDR] _EmissiveColor("Emissive 1 Color", Color) = (1, 1, 1, 1) 
        [HDR] _EmissiveSecondaryColor("Emissive 2 Color", Color) = (1, 1, 1, 1)
        [HDR] _EmissiveEdgeColor("Emissive 3 Color", Color) = (1, 1, 1, 1)

        [MainTexture] _NoiseMap("NoiseMap", 2D) = "white"
        _SpeedScroll("Speed Scroll", Float) = 0.0
    }

    // The SubShader block containing the Shader code.
    SubShader
    {
        // SubShader Tags define when and under which conditions a SubShader block or
        // a pass is executed.
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            // The HLSL code block. Unity SRP uses the HLSL language.
            HLSLPROGRAM
            // This line defines the name of the vertex shader.
            #pragma vertex vert
            // This line defines the name of the fragment shader.
            #pragma fragment frag

            // The Core.hlsl file contains definitions of frequently used HLSL
            // macros and functions, and also contains #include references to other
            // HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_NoiseMap);
            SAMPLER(sampler_NoiseMap);
            
            // The structure definition defines which variables it contains.
            // This example uses the Attributes structure as an input structure in
            // the vertex shader.
            struct Attributes
            {
                // The positionOS variable contains the vertex positions in object
                // space.
                float4 positionOS   : POSITION;
                float2 uv : TEXCOORD0;
                
            };

            struct Varyings
            {
                // The positions in this struct must have the SV_POSITION semantic.
                float4 positionHCS  : SV_POSITION;
                float2 uv : TEXCOORD0;
                VertexNormalInputs tamere : TEXCOORD1;
          half3 tamerea : TEXCOORD8;
                
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _NoiseMap_ST;
                 half4 _EmissiveColor;
                half4 _EmissiveSecondaryColor;
           half4 _EmissiveEdgeColor;
                float _SpeedScroll;
            
               
            CBUFFER_END
            // The vertex shader definition with properties defined in the Varyings
            // structure. The type of the vert function must match the type (struct)
            // that it returns.
            Varyings vert(Attributes IN)
            {
                // Declaring the output object (OUT) with the Varyings struct.
                Varyings OUT;
                
                OUT.uv = TRANSFORM_TEX(IN.uv,_NoiseMap);
                OUT.uv.xy += _Time * _SpeedScroll;
       
                
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.tamere = GetVertexNormalInputs(IN.positionOS);
                OUT.tamerea = TransformWorldToViewDir(IN.positionOS);
               
              
                return OUT;
            }

            // The fragment shader definition.
            half4 frag(Varyings IN) : SV_Target
            {
                // Defining the color variable and returning it.
                half4 noise = SAMPLE_TEXTURE2D(_NoiseMap, sampler_NoiseMap,IN.uv);
                float fresnel = dot(IN.tamere.normalWS, normalize(-GetViewForwardDir()));
                fresnel = saturate(1-fresnel);
                
                half4 newfresnel =   fresnel ;
                newfresnel = newfresnel.gggg.rrrr.aa.y * _EmissiveEdgeColor;
                //return newfresnel;
                noise *= 1-fresnel;
                half4 noiseInvert = (1 - noise ) ;
                noiseInvert -= fresnel;
                noise *= _EmissiveColor;
                noiseInvert *= _EmissiveSecondaryColor ;
                half4 result = (noise + noiseInvert) ;
               result += newfresnel;
               
                return result;
            }
            ENDHLSL
        }
    }
}