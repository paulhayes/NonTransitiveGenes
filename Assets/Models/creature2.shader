Shader "Custom/creature2" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Base (RGB)", 2D) = "white" {}
		_Sides1234 ("Sides 1 2 3 4", Vector) = (1,1,1,1)
		_Sides5678 ("Sides 5 6 7 8", Vector) = (1,1,1,1)
		_Color ("Main Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		Pass{
		CGPROGRAM
		#pragma target 3.0
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		sampler2D _Ramp;
		float4 _MainTex_ST;
		float4 _Sides1234;
		float4 _Sides5678;
		fixed4 _Color;

		
		static float sqrt2=1.4142135623730951;

		struct appdata_t {
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
		};


		struct v2f {
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
		};
		
		float getSideValue(int i){
			float r = 0;
			if( i < 4 ){
				r = _Sides1234[i];
			}
			else {
				r = _Sides5678[i-4];
			}
			//not working for some reason
			//r = dot(_Sides1234,maskA) + dot(_Sides5678,maskB);
			return r;
		}
		
		float2 sidePosition(int side){
			int c = 3;
			int r =	2;
			int i = side;
			float2 pos = float2( (1.0*(i%c)+0.5)*(1.0/c), (1.0*(i/c)+0.5)*(1.0/r) );
			//pos+=float2(_SinTime.y, _CosTime.y);
			//if( pos.x < 0 ) pos.x = -fmod(-pos.x,1); 
			//if( pos.y < 0 ) pos.x = -fmod(-pos.y,1); 
			//pos = fmod(pos,float2(1,1));
			//return float2(1.0*i/6,0.5);
			return pos;
		}
		
		fixed4 getSideColor(int i){
			return tex2D( _Ramp, float2((0.8*i+0)*(1.0/6),0.5));
		}

		
		v2f vert(appdata_t v){
			v2f o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
			return o;
		}
		
		float4 frag(v2f pix):SV_Target{
			
			int closest = 0;
			int secondClosest = 0;
			float sqrClosest = sqrt2;
			float sqrSecondClosest = sqrt2;
			float closestTint;
			float secondClosestTint;
			int size = 6;
			for(int i=0; i<size; i++){
				float2 marker = pix.uv - sidePosition(i);
				float dSqr = sqrt( dot(marker,marker) ) + max(1.0-getSideValue(i),0.0);
				if( dSqr <= sqrClosest ){
					secondClosest = closest;
					closest = i;	
					secondClosestTint= closestTint;
					sqrSecondClosest = sqrClosest;
					sqrClosest = dSqr;
					closestTint = getSideValue(i);
					
				}else if(dSqr<=sqrSecondClosest){
					secondClosest = i;
					sqrSecondClosest = dSqr;
					secondClosestTint = getSideValue(i);
				}
			}
			float a = sqrClosest; //+(1f-getSideValue(closest));
			float b = sqrSecondClosest; //+(1f-getSideValue(secondClosest));
			float4 colA = 2.0*closestTint*_Color;//getSideColor(closest);
			float4 colB = 2.0*secondClosestTint*_Color;//getSideColor(secondClosest);
			if(abs(a-b)<0.02){
				return float4(0,0,0,0);
			}
			return (a<=b) ? colA : colB ;
		}
		
		
		ENDCG
		}
	} 
	
}
