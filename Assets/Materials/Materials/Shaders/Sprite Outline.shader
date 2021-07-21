Shader "Unlit/Sprite Outline"
{
    Properties
    {
        _MainTex("Texture",2D) = "white" {}
		_NoiseTex("Noise_Tex",2D) = "black" {}
		_Color("Color", Color) = (0,0,0,0)
		_OffsetSize("Offset Size", Range(0,10)) = 0
		_SampleAve("SampleAve",Range(0,1)) = 0
		_CutThresh("CutThresh",Range(0,1)) = 0
		_TintMain("TintMain",Range(0,1)) = 0
		_TintOut("TintOut",Range(0,2)) = 0


    }
    SubShader
    {
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100
		Cull Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
			sampler2D _NoiseTex;
            float4 _MainTex_ST;
			fixed4 _Color;
			fixed4 _MainTex_TexelSize;

			float _SampleAv;
			float _FreqBins[512];
			float _Rand;

			float _CutThresh = 1;
			float _TintMain =1;
			float _TintOut = 1;
			float _AnimationSpeed = 10;

            v2f vert (appdata v)
            {
				//v.vertex.x = sin(_Time.y * _SampleAv + v.vertex.y * 10);

				v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                half4 col = tex2D(_MainTex, i.uv);
				half4 noise = tex2D(_NoiseTex, i.uv);
                // apply fog
               // UNITY_APPLY_FOG(i.fogCoord, col);

				col.rgb *= col.a;
				col += noise * _Color * _TintMain;
				half4 outlineC = noise * _Color * _TintOut;
				outlineC.a = noise.rgb + _SampleAv;
				outlineC.rgb *= outlineC.a + _SampleAv;

				fixed upAlpha = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y)).a;
				fixed downAlpha = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y)).a;
				fixed leftAlpha = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x,0 )).a;
				fixed rightAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0)).a;

				clip(col.b - _CutThresh);


                return lerp(outlineC,col, ceil(upAlpha*downAlpha*leftAlpha*rightAlpha));
            }
            ENDCG
        }
    }
}
