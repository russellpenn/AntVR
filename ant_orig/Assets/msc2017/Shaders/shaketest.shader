Shader "Custom/shaketest" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_ShakeDisplacement("Displacement",Range(0,1.0))=1.0;
		_ShakeTime("Shake Time",Range(0,1.0))=1.0;
		_ShakeWindspeed("Shake Windspeed",Range(0,1.0))=1.0;
		_ShakeBending("Shake Bending",Range(0,1.0))=1.0;
	}
	SubShader {
		Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert addshadow

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		fixed _Color;
		float _ShakeDisplacement;
		float _ShakeTime;
		float _ShakeWindSpeed;
		float _ShakeBending;

		struct Input {
			float2 uv_MainTex;
		};

		void vert (inout appdata_full v){
			float factor=(1-_ShakeDisplacement-v.color.r)*0.5;

			const float _WindSpeed=(_ShakeWindspeed+v.color.g);
			const float _WaveScale=_ShakeDisplacement;

			const float4 _waveXSize=float4(0.048,0.06,0.24,0.096);
			const float4 _waveZSize=float4(0.024,0.08,0.08,0.02);
			const float4 waveSpeed=float4(1.2,2,1.6,4.8);

			float4 _waveXmove=float4(0.024,0.04,-0.12,0.096);
			float4 _waveZmove=float4(0.006,0.02,-0.02,0.1);

			float4 waves;
			waves=v.vertex.x*_waveXSize;
			waves+=v.vertex.z*_waveZSize;

			waves=_Time.x *(1-_ShakeTime*2-v.color.b)*waveSpeed*_WindSpeed;


		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
