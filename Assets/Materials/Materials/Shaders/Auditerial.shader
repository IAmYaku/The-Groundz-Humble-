Shader "Unlit/Auditerial"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Color",Color) = (0,0,1,1)
		_VertexOffset("Offset", Float) = (0,0,0,0)
		_AnimationSpeed("Animation Speed", Range(0,3)) = 0
		_OffsetSize("Offset Size", Range(0,10)) = 0
		_SampleAve("SampleAve",Range(0,1)) = 0
		_Rand("_Rand", Float) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
				float3 normal: Normal;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			fixed4 _Color;
			fixed4 _VertexOffset;
			float _AnimationSpeed;
			float _OffsetSize;
			float _SampleAv;
			float _FreqBins[512];
			float _Rand;

            v2f vert (appdata v)
            {
				// v.vertex += _VertexOffset;
				v.vertex.x+= sin(_Time.y * _AnimationSpeed + v.vertex.y * _OffsetSize);
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);


                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col * _Color;
            }
            ENDCG
        }
    }
}
