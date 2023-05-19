Shader "Custom/FogVolume" {
    Properties{
        _FogDensity("Fog Density", Range(0.0, 1.0)) = 0.1
        _FogColor("Fog Color", Color) = (0.5, 0.5, 0.5, 1)
        _MainTex("Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
    }

        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float4 vertex : SV_POSITION;
                    float4 worldPos : TEXCOORD0;
                    float3 worldNormal : TEXCOORD1;
                    float2 uv : TEXCOORD2;
                };

                float _FogDensity;
                float4 _FogColor;
                float4 _Color;
                sampler2D _MainTex;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    // Compute the distance from the camera to the current pixel
                    float3 viewDir = normalize(i.worldPos - _WorldSpaceCameraPos);
                    float distance = length(i.worldPos - _WorldSpaceCameraPos);

                    // Compute the fog factor based on the distance and the fog density
                    float fogFactor = exp(-_FogDensity * distance);

                    // Sample the texture and apply the input color
                    fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;

                    // Compute the final fog color by blending the fog color and the scene color
                    fixed4 fogColor = lerp(_FogColor, texColor, fogFactor);

                    return fogColor;
                }
                ENDCG
            }
        }

            FallBack "Diffuse"
}