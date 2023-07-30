Shader "Custom/Wall" {
 
    Properties{
        _Color("Main Color", Color) = (1,1,1,1)
    }
 
    SubShader {
        Tags { "RenderType"="Transparent+1" }
 
        //Stencil
        //{
       //     Ref 1
       //     Comp Always
       //     Pass Replace
       // }
         Pass
        {    
              // Disables writing to the depth buffer for this Pass
              ZWrite On
            
              // The rest of the code that defines the Pass goes here.
        }
        CGPROGRAM
        #pragma surface surf Lambert alpha
 
        fixed4 _Color;
 
        struct Input {
            fixed3 Albedo;
        };
 
        void surf (Input IN, inout SurfaceOutput o) {
            //o.Albedo = fixed3(1, 1, 1);
            //o.Alpha = 0;
            o.Albedo = _Color.rgb;
            o.Emission = _Color.rgb;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}