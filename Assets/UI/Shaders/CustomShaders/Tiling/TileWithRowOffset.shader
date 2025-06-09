// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "UI/TileWithRowOffset"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _RowOffset ("Row Offset", Float) = 0.5 // 0 for no offset, 0.5 for brick-like
        _RotationAngle ("Rotation Angle (Degrees)", Range(0, 360)) = 0
        _TilingX ("Tiling X", Float) = 1
        _TilingY ("Tiling Y", Float) = 1
        _SDFCenter ("_SDFCenter Y", Float) = 1

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _RowOffset;
            float _RotationAngle;
            float _TilingX;
            float _TilingY;
            float _SDFCenter;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                
                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                                
                //uv.x = frac (v.texcoord.x * _TilingX);
                //uv.y = frac (v.texcoord.y * _TilingY);
                //if (_TilingY > 0 && _RowOffset != 0) {
                //   uv.x += _RowOffset * floor(uv.y);
                //}


                OUT.color = v.color * _Color;
                return OUT;
            }
                
            float2 RotateUV(float2 uv, float angleDegrees)
            {
                float angleRad = angleDegrees * UNITY_PI / 180.0;
                float s = sin(angleRad);
                float c = cos(angleRad);

                // Translate UVs so the center (0.5, 0.5) becomes (0, 0)
                uv -= float2(0.5, 0.5);

                // Apply rotation matrix
                float2 rotatedUV;
                rotatedUV.x = uv.x * c - uv.y * s;
                rotatedUV.y = uv.x * s + uv.y * c;

                // Translate UVs back
                rotatedUV += float2(0.5, 0.5);

                return rotatedUV;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float2 uv = IN.texcoord;
                uv = RotateUV(uv, _RotationAngle);
                uv.y = frac (uv.y + _Time.x * 0.2);
                uv.x = (uv.x * _TilingY) + (_RowOffset * floor(uv.y * _TilingY));
                uv.y = frac(uv.y * _TilingY);
                uv.x = frac (uv.x);

                
                float angleRad = _RotationAngle * (3.1415926535 / 180.0);
                //if (fmod(uv.y, 2.0) > 0.5) // Если ряд нечетный (или почти нечетный, для избежания артефактов)
                //{
                //    uv.x += _RowOffset; // Сдвигаем на полкирпича
                //}
                float4 color = tex2D(_MainTex, uv);
                #ifdef UNITY_UI_CLIP_RECT
                
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif


                
                float distance = color.r; 
                float pixelWidth = fwidth(distance);
                float scaledDistance = (distance - _SDFCenter) / pixelWidth;
                float alpha = smoothstep(-0.5, 0.5, scaledDistance);


                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif
                //return float4 (0,0,0,uv.x);
                color.a = alpha;
                return IN.color * color.a;
            }
        ENDCG
        }
    }
}