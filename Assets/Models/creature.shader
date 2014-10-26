Shader "Custom/creature" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Base (RGB)", 2D) = "white" {}
		_Color ("Main Color", Color) = (1,1,1,1)
		_Sides1234 ("Sides 1 2 3 4", Vector) = (1,1,1,1)
		_Sides5678 ("Sides 5 6", Vector) = (1,1,1,1)
		_Threshold ("Threshold",Float) = 0.01
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		Pass{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		float4 _MainTex_ST;
		sampler2D _MainTex;
		sampler2D _Ramp;
		fixed4 _Color;
		float4 _Sides1234;
		float4 _Sides5678;
		float _Threshold;

		struct appdata_t {
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
		};


		struct v2f {
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
		};
		
		v2f vert(appdata_t v){
			v2f o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.texcoord = v.texcoord.xy;			
			return o;
		}
		
		fixed4 frag(v2f i):SV_Target{
			fixed4 pos = tex2D(_MainTex, i.texcoord);
			fixed4 col = tex2D(_Ramp, pos.xy );
			col.a = 1;
			for(int i=0; i<8; i++){
				float f = 1.0*i/8;
				float r = 0;
				if( i < 4 ){
					r = _Sides1234[i];
				}else{
					r = _Sides5678[i-4];
				}
				
				if( abs(f-pos.x)<_Threshold ){
					col *= 2*r;
					
				}
				
			}
			col.a = 1;
			return col;
		}

		
		ENDCG
		}
	}
	
}
