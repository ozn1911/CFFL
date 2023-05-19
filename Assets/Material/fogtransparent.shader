Shader "Custom/FogVolumeTransparency" {
    Properties{
        _FogColor("Fog Color", Color) = (0.5,0.5,0.5,0.5)
        _FogDensity("Fog Density", Range(0.0, 1.0)) = 0.1
        _TransparencyStartDistance("Transparency Start Distance", Range(0.0, 100.0)) = 0.5
        _TransparencyEndDistance("Transparency End Distance", Range(0.0, 200.0)) = 1.0
        _MaxTransparency("Max Transparency", Range(0.0, 1.0)) = 1
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}
            LOD 100

            Pass {
                Blend SrcAlpha OneMinusSrcAlpha
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                };

                struct v2f {
                    float4 pos : SV_POSITION;
                    float3 worldPos : TEXCOORD0;
                    float3 worldNormal : TEXCOORD1;
                };

                float4x4 _ObjectToWorld;

                v2f vert(appdata v) {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.worldPos = mul(_ObjectToWorld, v.vertex).xyz;
                    o.worldNormal = mul(float4(v.vertex.xyz, 0), _ObjectToWorld).xyz;
                    return o;
                }

                float4 _FogColor;
                float _FogDensity;
                float _TransparencyStartDistance;
                float _TransparencyEndDistance;
                float _MaxTransparency;

                float4 frag(v2f i) : SV_Target {
                    float distToCamera = distance(i.worldPos, _WorldSpaceCameraPos);
                    float fogFactor = 1.0 - exp(-_FogDensity * distToCamera * distToCamera);
                    float transparencyFactor = (_TransparencyEndDistance - distToCamera) / (_TransparencyEndDistance - _TransparencyStartDistance);
                    transparencyFactor = clamp(transparencyFactor, 0.0, _MaxTransparency);
                    float4 finalColor = _FogColor * fogFactor;
                    finalColor.a *= transparencyFactor;
                    return finalColor;
                }
                ENDCG
            }
    }
        FallBack "Diffuse"
}
