Shader "Custom Shaders/frontcolor" {  
     
    Properties {  
    	_MainTint("Global Color Tint", Color) = (1,1,1,1)
        _windSource("Wind Direction", Float) = (1,0,0,0)
        _windSpeed ("Wind Speed", Range(0.0, 5.0)) = 1
        _windStrength ("Wind Strength", Range(0.0, 3.0)) = 1
        _mainBendFalloff ("Main Bending Falloff", Range(0.0, 1.0)) = .25
        _mainBendSpeed ("Main Bending Speed", Range(0.0, 1.0)) = .25
        _mainBendStrength ("Main Bending Strength", Range(0.0, 1.0)) = .25
        //_pivot("Pivot", Float) = 0.01
    }  

    SubShader {  
        Tags { "RenderType"="Opaque" }  
        LOD 200  

        //cull back faces
        Cull back
        CGPROGRAM  
        #pragma surface surf Standard vertex:vert addshadow
        #pragma target 3.0
  
        float4 _MainTint;  
  
        struct Input {  
            float4 vertColor;
        };  

        fixed3 _windSource;
        half _windSpeed;
        half _windStrength;
        half _mainBendFalloff;
        half _mainBendSpeed;
        half _mainBendStrength;
        //half _pivot;

        //approximate sine waves using triangle waves
	    fixed3 SmoothWave(fixed3 input) {
	     	//smooths the curve via cubic interpolation
	    	return input * input * (3.0 - 2.0 * input);
	    }

	    fixed3 TriangleWave(fixed3 input) {
			//get the decimals of input + 0.5
			//multiply by 2 and subtract 1
			//so we go 0 up to 1 down to 0 instead of jumping between 1 and 0
			return abs( frac( input + 0.5 ) * 2.0 - 1.0);
	    }

	    fixed3 SmoothTriangleWave(fixed3 input) {
			return SmoothWave( TriangleWave(input) );
	    }

	    //vertex function
        void vert(inout appdata_full v, out Input o) {
        	UNITY_INITIALIZE_OUTPUT(Input, o);
        	//assign vertex color  
        	o.vertColor = v.color; 
        	//only displace vertices above the specified height
            if ((v.vertex.z + 0.01) > 0) {

            	half3 windDir = normalize( _windSource.xyz );
                float Time = _Time.y * _windSpeed;
                //float flength = length(v.vertex.xyz);

                //float3 binormal = cross(v.normal.xyz, v.tangent.xyz);

                //float3 tangentpos = v.vertex.xyz - (v.tangent.xyz*0.01);
                //float3 binormalpos = v.vertex.xyz + (binormal*0.01);

                half3 mainDeformation = saturate( SmoothTriangleWave(windDir * Time * _mainBendSpeed ) )  * _windStrength;
                //half3 mainDeformation = saturate( sin(windDir * Time * _mainBendSpeed ) )  * _windStrength;
          	    mainDeformation.y = 0;

          	    //scale the amount of deformation by the height
          		half mainDeformationScale = length(v.vertex.z + 0.01) * _mainBendFalloff;
          		//half mainDeformationScale = length(v.vertex.z + _pivot) * _mainBendFalloff;
          		mainDeformationScale += 1.0;
          		mainDeformationScale *= mainDeformationScale;
          		mainDeformationScale = mainDeformationScale * mainDeformationScale - mainDeformationScale;
          		v.vertex.xzy += mainDeformation * mainDeformationScale * _mainBendStrength; 

          		//restrict vertex movement
          		//rescale the new displaced position
          		//v.vertex.xyz = normalize(v.vertex.xyz)*flength;

          		//update the normals using tangents and binormals
          		//float tangentlength = length(tangentpos.xyz);
          		//mainDeformationScale = length(tangentpos.z + 0.01) * _mainBendFalloff;
          		//mainDeformationScale += 1.0;
          		//mainDeformationScale *= mainDeformationScale;
          		//mainDeformationScale = mainDeformationScale * mainDeformationScale - mainDeformationScale;
          		//tangentpos.xzy += mainDeformation * mainDeformationScale * _mainBendStrength; 

          		//restrict vertex movement
          		//rescalet the new displaced position
          		//tangentpos.xyz = normalize(tangentpos.xyz)*tangentlength;

          		//float binormallength = length(binormalpos.xyz);
          		//mainDeformationScale = length(binormalpos.z + 0.01) * _mainBendFalloff;
          		//mainDeformationScale += 1.0;
          		//mainDeformationScale *= mainDeformationScale;
          		//mainDeformationScale = mainDeformationScale * mainDeformationScale - mainDeformationScale;
          		//binormalpos.xzy += mainDeformation * mainDeformationScale * _mainBendStrength; 

          		//restrict vertex movement
          		//rescalet the new displaced position
          		//binormalpos.xyz = normalize(binormalpos.xyz)*binormallength;

          		//binormal = normalize(binormalpos - v.vertex.xyz);
          		//float3 newtangent = normalize(v.vertex.xyz - tangentpos.xyz);

          		//v.normal = cross(binormal, -newtangent);

          	}       
        }  
  
        void surf (Input IN, inout SurfaceOutputStandard o) {  
            o.Albedo = IN.vertColor.rgb * _MainTint.rgb;
        }  
        ENDCG

    }   
    FallBack "Diffuse"  
}  