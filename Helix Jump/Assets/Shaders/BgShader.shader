// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BgShader" {
	Properties
    {
         _Color1 ("Color1", Color) = (1.0, 1.0, 1.0, 1.0)
         _Color2 ("Color2", Color) = (0.0, 0.0, 0.0, 1.0)
         _Color3 ("Color3", Color) = (0.0, 0.0, 0.0, 1.0)
        [PowerSlider(1)] _Top("Top", Range(0.0, 1.0)) = 0.75
        [PowerSlider(1)] _Bottom("Bottom", Range(0.0, 1.0)) = 0.25
    }
     
    SubShader
    {
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
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
             
            fixed4 _Color1;
            fixed4 _Color2;
            fixed4 _Color3;
            float _Weights;
            float _Top;
            float _Bottom;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            //注意，uv的最大值是(1, 1)
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                float lp = 0.0;
                 
                if (i.uv.y >= _Top)
                {
                    lp = (1 - i.uv.y) / (1 - _Top);
                    col = lerp(_Color1, _Color2, lp);
                }
                else if (i.uv.y <= _Bottom)
                {
                    lp = i.uv.y / _Bottom;
                    col = lerp(_Color3, _Color2, lp);
                }
                else
                {
                    col = _Color2;
                }
                 
 
                return col;
            }
             
            ENDCG
        }
    }
}
