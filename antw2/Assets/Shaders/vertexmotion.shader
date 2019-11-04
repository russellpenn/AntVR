Shader "Custom Shaders/vertexmotion" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
		_TimeScale("Time Scale", float)	= 1
		}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Opaque" "IgnoreProject"="True" }
		//Tags { "RenderType"="Opaque" }
		//LOD 200

		Pass{
			Tags{"Lightmode"="ForwardBase"}

			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
		
		    CGPROGRAM

		   #pragma vertex vert
		   #pragma fragment frag
		   #include "UnityCG.cginc"
		// Physically based Standard lighting model, and enable shadows on all light types
		//#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		//#pragma target 3.0

			sampler2D _MainTex;
			half _TimeScale;

			struct appdata{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				};

			struct v2f{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				}; 

			v2f vert(appdata v){
				v2f o;
				float4 offset = float4(0,0,0,0);
				offset.x = 0.01*sin(3.1416 * _Time.y * clamp(v.texcoord.y-0.8,0,1)) * _TimeScale;
				o.pos = UnityObjectToClipPos(v.vertex + offset);
				o.uv = v.texcoord.xy;
				return o;
				}

			fixed4 frag(v2f i) : SV_Target{
				return tex2D(_MainTex, i.uv);
				}		
			ENDCG
			}
			}
	FallBack Off
}
