Shader "Custom Shaders/backcolorstatic" {  

    Properties {  
        _MainTint("Global Color Tint", Color) = (1,1,1,1)  
   
    }  
      
    SubShader {  
        Tags { "RenderType"="Opaque" }  
        LOD 200  

        //cull front faces
        Cull front
        CGPROGRAM  
        #pragma surface surf Lambert vertex:vert addshadow
  		#pragma target 3.0

        float4 _MainTint;  
  
        struct Input   
        {  
            float4 vertColor;
        };  

      	
	  	//vertex function          
      	void vert(inout appdata_full v, out Input o) {  
            UNITY_INITIALIZE_OUTPUT(Input, o);
            //assign vertex color
            o.vertColor = v.color;

          	v.normal=-v.normal;
        }  

   
  
        void surf (Input IN, inout SurfaceOutput o)   
        {  
            o.Albedo = IN.vertColor.rgb * _MainTint.rgb;
        }  
        ENDCG 

    }   
    FallBack "Diffuse"  
}  
