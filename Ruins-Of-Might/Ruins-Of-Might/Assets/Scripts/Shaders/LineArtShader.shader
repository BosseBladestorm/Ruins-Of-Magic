Shader "Custom/LineArtShader" {

	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Thickness("Thickness", Float) = 1
	}

		SubShader{

			Cull Off
			Blend One OneMinusSrcAlpha

			Pass {

			CGPROGRAM
			#pragma vertex vertexFunc
			#pragma fragment fragmentFunc
			#include "UnityCG.cginc"

			sampler2D _MainTex;

			struct v2f {
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;

			};

			v2f vertexFunc(appdata_base v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;

				return o;

			}

			fixed4 _Color;
			float4 _MainTex_TexelSize;
			float _Thickness;

			fixed4 fragmentFunc(v2f i) : COLOR{
				half4 c = tex2D(_MainTex, i.uv);
				c.rgb *= c.a;
				half4 outlineC = _Color;
				outlineC.a *= ceil(c.a);
				outlineC.rgb *= outlineC.a;

				fixed upAlpha = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y * _Thickness)).a;
				fixed downAlpha = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y * _Thickness)).a;
				fixed rightAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x * _Thickness, 0)).a;
				fixed leftAlpha = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x * _Thickness, 0)).a;

				fixed upLeftAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x * -_Thickness, _MainTex_TexelSize.y * _Thickness)).a;
				fixed downLeftAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x * -_Thickness, _MainTex_TexelSize.y * -_Thickness)).a;
				fixed upRightAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x * _Thickness, _MainTex_TexelSize.y * _Thickness)).a;
				fixed downRightAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x * _Thickness, _MainTex_TexelSize.y * -_Thickness)).a;

				return lerp(outlineC, c, ceil(upAlpha * downAlpha * rightAlpha * leftAlpha * upLeftAlpha * downLeftAlpha * upRightAlpha * downRightAlpha));

			}

			ENDCG

			}

		}


}
