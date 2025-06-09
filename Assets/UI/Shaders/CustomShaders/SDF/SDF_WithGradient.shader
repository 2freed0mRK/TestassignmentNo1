Shader "Custom/SDFWithGradient"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _SDFCenter ("SDF Center Value", Range(0,1)) = 0.5
        _TopColor ("Bottom Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color    : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color    : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _SDFCenter;
            fixed4 _TopColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float distance = tex2D(_MainTex, i.uv).r; 
                float pixelWidth = fwidth(distance);
                float scaledDistance = (distance - _SDFCenter) / pixelWidth;
                float alpha = smoothstep(-0.5, 0.5, scaledDistance);
                float gradientUV_Y = smoothstep (_TopColor.a,1 - _SDFCenter,i.uv.y);
                fixed4 col = lerp(i.color, _TopColor, gradientUV_Y);
                col.a = alpha;

                return col;
            }
            ENDCG
        }
    }
}