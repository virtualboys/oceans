Shader "Unlit/LineShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_rowCoord ("Row", Float) = 0
		_normal ("normal", Vector) = (0,1,0)
		_keyColor ("key color", Color) = (0,1,0)
	}
	SubShader
	{
		Tags { "Queue"="Transparent" 
		"RenderType"="Transparent" }
		Cull Back
		ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha
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
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float _rowCoord;
			float4 _MainTex_ST;
			float3 _normal;
			uniform float4 _keyColor;
			
			v2f vert (appdata v)
			{
				v2f o;
				//o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
				float4 vert= v.vertex;
				vert=mul(unity_ObjectToWorld,vert);
				float4 uv;
				uv.x=o.uv.x;
				uv.y=_rowCoord;
				uv.z=0;
				uv.w=0;
				int t = (int)_Time.x;
				float time= _Time.x-t; 
				if(time>.5)
					time=1-time;
				vert.xyz+= length(tex2Dlod(_MainTex, uv))*(_normal*(3*(1+time)));
				vert= mul(UNITY_MATRIX_VP,vert);
				o.vertex=vert;
				//o.vertex = mul(
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float2 uv;
				uv.x=i.uv.x;
				uv.y=_rowCoord;
				fixed4 col = tex2D(_MainTex, uv);
				if(length(col - fixed4(.207,.627,.188,1)) < .3f) {
					col.a = 0;
				}
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
