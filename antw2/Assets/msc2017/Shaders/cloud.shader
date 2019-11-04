// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom Shaders/cloudshadow" {

	Properties {		 
		_CloudColor ("Color", Color) = (1, 1, 1, 0.5)
		_Density ("Density", Range(0.0, 2.0)) = 0.2
		_Speed ("Speed", float) = 0.1
		//_Direction("Direction", vector) = (1, 0, 0, 0)
		_ScatterMap0 ("Scatter Map 1", 2D) = "white" {}
		_ScatterMap1 ("Scatter Map 2", 2D) = "white" {}
		_DensityMap ("Density Map", 2D) = "white" {}
		_TextureMap ("Texture Map", 2D) = "white" {}
	    _AlphaCutoff("Alpha Cutoff", Range(0.0,1.0))=0.35
	}

	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		cull off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			//Add lighting
		    Tags{"Lightmode"="ForwardBase"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"

			fixed4 _CloudColor;
			sampler2D _ScatterMap0;
			sampler2D _ScatterMap1;
			sampler2D _DensityMap;
			sampler2D _TextureMap;
			fixed4 _ScatterMap0_ST;
			fixed4 _ScatterMap1_ST;
			fixed4 _DensityMap_ST;
			fixed4 _TextureMap_ST;
			fixed _Speed;
			fixed _Density;
			//fixed4 _Direction;
		

			struct v2f {
				fixed4 pos : SV_POSITION;
				fixed4 uv0 : TEXCOORD0;
				fixed4 uv1 : TEXCOORD1;
				fixed4 diff : COLOR0;
			};

			v2f vert (appdata_base v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				//Set UV coordinates for scatter maps
				o.uv0.xy = TRANSFORM_TEX(v.texcoord, _ScatterMap0)* fixed2(2, 2) + _Time.x * _Speed * (1.5,1.0);
				o.uv0.zw = TRANSFORM_TEX(v.texcoord, _ScatterMap1)* fixed2(2, 2) + _Time.x * _Speed *  (1.0, 1.2);
				o.uv1.xy = TRANSFORM_TEX(v.texcoord, _DensityMap) * fixed2(10, 10.5) + _Time.x * _Speed *  (0.75, 0.5);
				o.uv1.zw = TRANSFORM_TEX(v.texcoord, _TextureMap) * fixed2(10.6, 10) + _Time.x * _Speed *  (0.5, 0.6);

				//Add diffuse lighting
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				half diff = max(0, dot(normalize(worldNormal), normalize(_WorldSpaceLightPos0.xyz)));
				o.diff = diff;
				return o;
			}
			
			half4 frag (v2f i) : SV_Target {
				fixed4 col = 0;
				fixed4 n0 = tex2D(_ScatterMap0, i.uv0.xy);
				fixed4 n1 = tex2D(_ScatterMap1, i.uv0.zw);
				fixed4 n2 = tex2D(_DensityMap, i.uv1.xy);
				fixed4 n3 = tex2D(_TextureMap, i.uv1.zw);
				fixed4 fbm = saturate(n0 + n1 + n2 + n3 - _Density);
				//Set alpha value
				col.a = saturate(fbm.xyz * _CloudColor.a * 2);

				//Set cloud color white
				col.rgb = _CloudColor.rgb;
				//Change the transparency according to the lighting
				col.a *= i.diff;
				return col;
			}
			ENDCG
		}

		//Add shadow
		Pass{
			Tags{"Lightmode"="ShadowCaster"}
			CGPROGRAM

			#pragma target 3.0
			#pragma vertex MyShadowVertexProgram
			#pragma fragment MyShadowFragmentProgram

			#include "UnityCG.cginc"

            fixed4 _CloudColor;
			sampler2D _ScatterMap0;
			sampler2D _ScatterMap1;
			sampler2D _DensityMap;
			sampler2D _TextureMap;
			fixed4 _ScatterMap0_ST;
			fixed4 _ScatterMap1_ST;
			fixed4 _DensityMap_ST;
			fixed4 _TextureMap_ST;
			fixed _Speed;
			fixed _Density;
            float _AlphaCutoff;

			struct vertdata {
				float4 position : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f{
				float4 position : SV_POSITION;
				fixed4 uv0 : TEXCOORD0;
		        fixed4 uv1 : TEXCOORD1;
			};

			v2f MyShadowVertexProgram (vertdata v) {
				v2f i;
				i.position = UnityClipSpaceShadowCasterPos(v.position.xyz, v.normal);
				i.position = UnityApplyLinearShadowBias(i.position);

				i.uv0.xy = TRANSFORM_TEX(v.uv, _ScatterMap0)* fixed2(2, 2) + _Time.x * _Speed * fixed2(1.5, 1.0);
				i.uv0.zw = TRANSFORM_TEX(v.uv, _ScatterMap1)* fixed2(2, 2) + _Time.x * _Speed * fixed2(1.0, 1.2);
				i.uv1.xy = TRANSFORM_TEX(v.uv, _DensityMap) * fixed2(10, 10.5) + _Time.x * _Speed * fixed2(0.75, 0.5);
				i.uv1.zw = TRANSFORM_TEX(v.uv, _TextureMap) * fixed2(10.6, 10) + _Time.x * _Speed * fixed2(0.5, 0.6);

				return i;
				}


			float4 MyShadowFragmentProgram (v2f i) : SV_TARGET {
				fixed4 n0 = tex2D(_ScatterMap0, i.uv0.xy);
				fixed4 n1 = tex2D(_ScatterMap1, i.uv0.zw);
				fixed4 n2 = tex2D(_DensityMap, i.uv1.xy);
				fixed4 n3 = tex2D(_TextureMap, i.uv1.zw);
				fixed4 fbm = saturate(n0 + n1 + n2 + n3 - _Density);
				float alpha = saturate(fbm.xyz * _CloudColor.a * 2);
				//Discard the pixel where alpha less than the threshold
				//Only cast shadow when alpha greater than the threshold
				clip(alpha-_AlphaCutoff-0.001);
				
				return 0;
			}
			ENDCG
		}
	
	}
}