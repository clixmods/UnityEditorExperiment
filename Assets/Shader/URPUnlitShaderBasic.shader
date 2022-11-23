// This shader fills the mesh shape with a color predefined in the code.
Shader "Example/URPUnlitShaderBasic"
{
    // The properties block of the Unity shader. In this example this block is empty
    // because the output color is predefined in the fragment shader code.
    Properties
    { 
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)   
        [HDR] _EmissiveColor("Emissive Color", Color) = (1, 1, 1, 1)    
        [MainTexture] _BaseMap("Base Map", 2D) = "white"
        [MainTexture] _BaseEmissive("EmissiveMap", 2D) = "black"
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

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            TEXTURE2D(_BaseEmissive);
            SAMPLER(sampler_BaseEmissive);
            
            // The structure definition defines which variables it contains.
            // This example uses the Attributes structure as an input structure in
            // the vertex shader.
            struct Attributes
            {
                // The positionOS variable contains the vertex positions in object
                // space.
                float4 positionOS   : POSITION;
                float2 uv : TEXCOORD0;
                float2 uvEmissive : TEXCOORD1;
            };

            struct Varyings
            {
                // The positions in this struct must have the SV_POSITION semantic.
                float4 positionHCS  : SV_POSITION;
                  float2 uv : TEXCOORD0;
                float2 uvEmissive : TEXCOORD1;
                
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                half4 _EmissiveColor;
                float4 _BaseMap_ST;
               
                float4 _BaseEmissive_ST;
            CBUFFER_END
            // The vertex shader definition with properties defined in the Varyings
            // structure. The type of the vert function must match the type (struct)
            // that it returns.
            Varyings vert(Attributes IN)
            {
                // Declaring the output object (OUT) with the Varyings struct.
                Varyings OUT;
      
                // The TransformObjectToHClip function transforms vertex positions
                // from object space to homogenous clip space.
              
                OUT.uv = TRANSFORM_TEX(IN.uv,_BaseMap);
                OUT.uvEmissive = TRANSFORM_TEX(IN.uvEmissive,_BaseEmissive);
              //  float4 pixelColorNoise = tex2Dlod(_BaseEmissive, float4(TRANSFORM_TEX( IN.uvEmissive, _BaseEmissive),0,0)) ;

                //IN.positionOS.y = (( pixelColorNoise.r))* 10 ;
                
               // IN.positionOS.y *=  _CosTime;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                
            
    
                 
                // Returning the output.
                return OUT;
            }

            // The fragment shader definition.
            half4 frag(Varyings IN) : SV_Target
            {
                // Defining the color variable and returning it.
                half4 baseMap = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap,IN.uv);
                half4 emissive = SAMPLE_TEXTURE2D(_BaseEmissive,sampler_BaseEmissive, IN.uvEmissive);
                
                half4 emissiveMap = emissive * _EmissiveColor;
                half4 mix = _BaseColor*baseMap;
                
                half4 customColor = mix+emissiveMap;
                
                return customColor;
            }
            ENDHLSL
        }
    }
}