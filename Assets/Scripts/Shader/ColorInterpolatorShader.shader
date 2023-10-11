Shader "Custom/ColorInterpolatorShader"
{
    Properties
    {
        _Color1 ("Color 1", Color) = (1, 0, 0, 1) // Default color for object 1
        _Color2 ("Color 2", Color) = (0, 0, 1, 1) // Default color for object 2
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : TEXCOORD0;
            };

            float4 _Color1;
            float4 _Color2;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Calculate the position of the third object in world space
                float3 thirdObjectPosition = _WorldSpaceCameraPos - i.pos.xyz;
                
                // Calculate the interpolation factor based on the position of the third object
                float interpolationFactor = saturate((length(thirdObjectPosition) - 0) / (1 - 0));
                
                // Interpolate between the two colors based on the factor
                half4 interpolatedColor = lerp(_Color1, _Color2, interpolationFactor);
                
                return interpolatedColor;
            }
            ENDCG
        }
    }
}
