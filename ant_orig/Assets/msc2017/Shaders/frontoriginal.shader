// simple "dissolving" shader by genericuser (radware.wordpress.com)
// clips materials, using an image as guidance.
// use clouds or random noise as the slice guide for best results.
  Shader "Custom Shaders/Frontsideoriginal" {
    Properties {
      _MainTex ("Texture (RGB)", 2D) = "white" {}
      _SliceGuide ("Slice Guide (RGB)", 2D) = "white" {}
      _SliceAmount ("Slice Amount", Range(0.0, 1.0)) = 0.5
      _windSource("Wind Direction", Float) = (1,0,0,0)
      _windSpeed ("Wind Speed", Range(0.0, 5.0)) = 1
      _windStrength ("Wind Strength", Range(0.0, 3.0)) = 1
      _mainBendFalloff ("Main Bending Falloff", Range(0.0, 1.0)) = .25
      _mainBendSpeed ("Main Bending Speed", Range(0.0, 1.0)) = .25
      _mainBendStrength ("Main Bending Strength", Range(0.0, 1.0)) = .25
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      Cull back
      CGPROGRAM
      //if you're not planning on using shadows, remove "addshadow" for better performance
      #pragma surface surf Standard addshadow vertex:vert
      struct Input {
          float2 uv_MainTex;
          float2 uv_SliceGuide;
          float _SliceAmount;
      };

      fixed3 _windSource;
      half _windSpeed;
      half _windStrength;
      half _mainBendFalloff;
      half _mainBendSpeed;
      half _mainBendStrength;

      //some wave functions
	  fixed3 SmoothWave(fixed3 input)
	  {
	  //smooths the curve via cubic interpolation
	  return input * input * (3.0 - 2.0 * input);
	  }
	  fixed3 TriangleWave(fixed3 input)
	  {
			//get the decimals of input + 0.5
			//multiply by 2 and subtract 1
			//so we go 0 up to 1 down to 0 instead of jumping between 1 and 0
			return abs( frac( input + 0.5 ) * 2.0 - 1.0);
	  }
	  fixed3 SmoothTriangleWave(fixed3 input)
	  {
			return SmoothWave( TriangleWave(input) );
	  }

      void vert (inout appdata_full v, out Input o){
      	UNITY_INITIALIZE_OUTPUT(Input, o);
      	if ((v.vertex.z+0.01)>0){
          half3 windDir = normalize( _windSource.xyz );
          float Time = _Time.y*_windSpeed;
          float flength = length(v.vertex.xyz);
          fixed variation=v.color.y;
          half3 mainDeformation = saturate( SmoothTriangleWave(windDir * Time * _mainBendSpeed+variation) )  * _windStrength;
          mainDeformation.y = 0;
          half mainDeformationMask = length(v.vertex.z+0.01) * _mainBendFalloff;
          mainDeformationMask += 1.0;
          mainDeformationMask *= mainDeformationMask;
          mainDeformationMask = mainDeformationMask * mainDeformationMask - mainDeformationMask;
          v.vertex.xzy += mainDeformation * mainDeformationMask*  _mainBendStrength; 
          }
          //v.vertex.xyz = normalize(v.vertex.xyz) * flength;
          //v.vertex.x += 0.1*sin(_Time.y)*(v.vertex.z+0.01);
      }

      sampler2D _MainTex;
      sampler2D _SliceGuide;
      float _SliceAmount;
      void surf (Input IN, inout SurfaceOutputStandard o) {
          clip(tex2D (_SliceGuide, IN.uv_SliceGuide).rgb - _SliceAmount);
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }
     
