
Shader "Example/UnlitBillboard"
{
    // The properties block of the Unity shader.
    Properties
    { 
        [HDR] _BaseColor("BaseColor", Color) = (1, 1, 1, 1) 
        [MainTexture] _BaseMap("BaseMap", 2D) = "white"
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
            
            // The structure definition defines which variables it contains.
            // This example uses the Attributes structure as an input structure in
            // the vertex shader.
            struct Attributes
            {
                // The positionOS variable contains the vertex positions in object
                // space.
                float4 positionOS   : POSITION;
                float2 uv : TEXCOORD0;
                float3 normals : NORMAL;
            };

            struct Varyings
            {
                // The positions in this struct must have the SV_POSITION semantic.
                float4 positionHCS  : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normals : TEXCOORD1;
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
                half4 _BaseColor;
            CBUFFER_END
            // The vertex shader definition with properties defined in the Varyings
            // structure. The type of the vert function must match the type (struct)
            // that it returns.
            Varyings vert(Attributes IN)
            {
                // Declaring the output object (OUT) with the Varyings struct.
                Varyings OUT;
                //IN.positionOS.xyz = mul(IN.uv, IN.positionOS.xyz );
                //OUT.positionHCS =  IN.positionOS;
                //OUT.positionHCS =  TransformObjectToHClip(IN.positionOS);
                //OUT.positionHCS = TransformObjectToTangent(TransformWorldToViewDir(OUT.positionHCS),GetViewToHClipMatrix());
                 
                OUT.positionHCS = unity_CameraProjection * TransformObjectToHClip(IN.positionOS);
              //  OUT.positionHCS = unity_CameraProjection*float4(OUT.positionHCS.xyz,0);
                OUT.uv = IN.uv;
                //
                // float3 ObjectPositionWorldSpace;
                // float3 CameraPositionWorldSpace;
                // float3 one = mul(float3(1,1,1),CameraPositionWorldSpace);
                // float3 subtract = normalize(ObjectPositionWorldSpace - one);
                // float3 Output = atan2(subtract.y, subtract.x);
                // Output /= 3.14;
                // Output /= 2;
                // float3 RotationAngle = Output + 0.25;
                //     

                
                //OUT.uv.xy =  dot(IN.positionOS,normalize(OUT.positionHCS.xy-GetCurrentViewPosition() ) )  ;

                
                return OUT;
            }

            // The fragment shader definition.
            half4 frag(Varyings IN) : SV_Target
            {
                half4 baseMap = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap,IN.uv);
                return baseMap;
            }
            ENDHLSL
        }
    }
}