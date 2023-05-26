Shader "Toon/OutlineShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _AngleThreshold("Angle Threshold", Range(0, 180)) = 30
        _OutlineWidth("Outline Width", Range(0, 0.1)) = 0.01
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

            Cull Off
            ZWrite Off
            ZTest Always

            CGPROGRAM
            #pragma surface surf Lambert

            #include "UnityCG.cginc"

            struct Input
            {
                float2 uv_MainTex;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _AngleThreshold;
            float _OutlineWidth;

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 mainTexColor = tex2D(_MainTex, IN.uv_MainTex);

                // Calculate the normal derivative
                float2 ddx_normal = ddx(IN.uv_MainTex);
                float2 ddy_normal = ddy(IN.uv_MainTex);
                float3 normal = UnpackNormal(mainTexColor);
                float2 normalDerivative = float2(dot(ddx_normal, ddx_normal), dot(ddy_normal, ddy_normal));
                float2 sobel = sqrt(normalDerivative);

                // Calculate the angle between the normal and the sobel values
                float angle = degrees(acos(dot(normal, float3(sobel, 0.0))));

                // Determine if the angle exceeds the threshold
                float outline = step(angle, _AngleThreshold);

                // Apply the outline color
                fixed4 outlineColor = outline * _OutlineColor;

                // Adjust the alpha channel to control outline width
                outlineColor.a *= outline * _OutlineWidth;

                // Mix the outline color with the main texture color
                fixed4 finalColor = lerp(mainTexColor, outlineColor, outlineColor.a);

                o.Albedo = finalColor.rgb;
                o.Alpha = finalColor.a;
            }
            ENDCG
        }
}
